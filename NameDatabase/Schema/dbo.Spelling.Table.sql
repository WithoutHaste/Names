USE [NameDatabase]
GO
/****** Object:  Table [dbo].[Spelling]    Script Date: 9/14/2019 10:51:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spelling](
	[CommonNameId] [int] NOT NULL,
	[VariationNameId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Spelling] PRIMARY KEY CLUSTERED 
(
	[CommonNameId] ASC,
	[VariationNameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_CommonNameId]    Script Date: 9/14/2019 10:51:22 AM ******/
CREATE NONCLUSTERED INDEX [IX_CommonNameId] ON [dbo].[Spelling]
(
	[CommonNameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_VariationNameId]    Script Date: 9/14/2019 10:51:22 AM ******/
CREATE NONCLUSTERED INDEX [IX_VariationNameId] ON [dbo].[Spelling]
(
	[VariationNameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Spelling]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Spelling_dbo.Name_CommonNameId] FOREIGN KEY([CommonNameId])
REFERENCES [dbo].[Name] ([Id])
GO
ALTER TABLE [dbo].[Spelling] CHECK CONSTRAINT [FK_dbo.Spelling_dbo.Name_CommonNameId]
GO
ALTER TABLE [dbo].[Spelling]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Spelling_dbo.Name_VariationNameId] FOREIGN KEY([VariationNameId])
REFERENCES [dbo].[Name] ([Id])
GO
ALTER TABLE [dbo].[Spelling] CHECK CONSTRAINT [FK_dbo.Spelling_dbo.Name_VariationNameId]
GO
