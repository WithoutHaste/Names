USE [NameDatabase]
GO
/****** Object:  StoredProcedure [dbo].[GetNamesByOrigin]    Script Date: 9/14/2019 10:51:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetNamesByOrigin]
	@origin as nvarchar(128)
as
begin
	with categoryCTE (Category)
	as
	(
		select 
			Category
		from dbo.Category 
		where Category.Category = @origin OR Category.SuperCategory = @origin

		UNION ALL

		select 
			Category.Category 
		from Category 
		inner join categoryCTE on Category.SuperCategory = categoryCTE.Category
	)
	select
		Name.Id,
		Name.Name,
		Name.FirstLetter,
		NameDetail.Origin
	from dbo.Name
	inner join dbo.NameDetail on NameDetail.NameId = Name.Id
	where @origin is NULL or @origin = '' or NameDetail.Origin IN (SELECT Category from categoryCTE)
end;
GO
