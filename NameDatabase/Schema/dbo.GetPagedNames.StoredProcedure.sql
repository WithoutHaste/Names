USE [NameDatabase]
GO
/****** Object:  StoredProcedure [dbo].[GetPagedNames]    Script Date: 9/14/2019 2:50:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetPagedNames]
	@pageIndex as int,
	@rowsPerPage as int
as
begin
	select
		NameId,
		Name,
		FirstLetter,
		IsFamiliar,
		NameDetailId,
		Origin,
		Meaning
	from (
		select
			ROW_NUMBER() OVER(ORDER BY Name, NameDetail.Id) as RowNumber,
			Name.Id as NameId,
			Name.Name,
			Name.FirstLetter,
			Name.IsFamiliar,
			NameDetail.Id as NameDetailId,
			NameDetail.Origin,
			NameDetail.Meaning
		from dbo.Name
		inner join dbo.NameDetail on NameDetail.NameId = Name.Id
	) as AllNamesWithDetails
	where RowNumber > @pageIndex * @rowsPerPage
		AND RowNumber <= (@pageIndex + 1) * @rowsPerPage
	order by RowNumber
end;
GO
