USE [NameDatabase]
GO
/****** Object:  Table [dbo].[NickName]    Script Date: 9/14/2019 10:51:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NickName](
	[NickNameId] [int] NOT NULL,
	[FullNameId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.NickName] PRIMARY KEY CLUSTERED 
(
	[NickNameId] ASC,
	[FullNameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_FullNameId]    Script Date: 9/14/2019 10:51:22 AM ******/
CREATE NONCLUSTERED INDEX [IX_FullNameId] ON [dbo].[NickName]
(
	[FullNameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NickNameId]    Script Date: 9/14/2019 10:51:22 AM ******/
CREATE NONCLUSTERED INDEX [IX_NickNameId] ON [dbo].[NickName]
(
	[NickNameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NickName]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NickName_dbo.Name_FullNameId] FOREIGN KEY([FullNameId])
REFERENCES [dbo].[Name] ([Id])
GO
ALTER TABLE [dbo].[NickName] CHECK CONSTRAINT [FK_dbo.NickName_dbo.Name_FullNameId]
GO
ALTER TABLE [dbo].[NickName]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NickName_dbo.Name_NickNameId] FOREIGN KEY([NickNameId])
REFERENCES [dbo].[Name] ([Id])
GO
ALTER TABLE [dbo].[NickName] CHECK CONSTRAINT [FK_dbo.NickName_dbo.Name_NickNameId]
GO
