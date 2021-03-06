USE [NameDatabase]
GO
/****** Object:  Table [dbo].[NameDetail]    Script Date: 9/14/2019 10:51:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NameDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameId] [int] NOT NULL,
	[SourceId] [int] NOT NULL,
	[IsBoy] [bit] NULL,
	[IsGirl] [bit] NULL,
	[IsFirstName] [bit] NULL,
	[IsLastName] [bit] NULL,
	[Origin] [nvarchar](128) NULL,
	[Meaning] [nvarchar](256) NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.NameDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_NameId]    Script Date: 9/14/2019 10:51:22 AM ******/
CREATE NONCLUSTERED INDEX [IX_NameId] ON [dbo].[NameDetail]
(
	[NameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SourceId]    Script Date: 9/14/2019 10:51:22 AM ******/
CREATE NONCLUSTERED INDEX [IX_SourceId] ON [dbo].[NameDetail]
(
	[SourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NameDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NameDetail_dbo.Name_NameId] FOREIGN KEY([NameId])
REFERENCES [dbo].[Name] ([Id])
GO
ALTER TABLE [dbo].[NameDetail] CHECK CONSTRAINT [FK_dbo.NameDetail_dbo.Name_NameId]
GO
ALTER TABLE [dbo].[NameDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NameDetail_dbo.Source_SourceId] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Source] ([Id])
GO
ALTER TABLE [dbo].[NameDetail] CHECK CONSTRAINT [FK_dbo.NameDetail_dbo.Source_SourceId]
GO
