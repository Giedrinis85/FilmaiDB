USE [FilmaiDB]
GO

/****** Object:  Table [dbo].[Filmai]    Script Date: 2019.08.17 23:00:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Filmai](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Pavadinimas] [nvarchar](max) NOT NULL,
	[IsleidimoData] [int] NOT NULL,
	[ZanraiId] [int] NOT NULL,
 CONSTRAINT [PK_Filmai] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Filmai] ADD  DEFAULT ((0)) FOR [ZanraiId]
GO

ALTER TABLE [dbo].[Filmai]  WITH CHECK ADD  CONSTRAINT [FK_Filmai_Zanrai_ZanraiId] FOREIGN KEY([ZanraiId])
REFERENCES [dbo].[Zanrai] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Filmai] CHECK CONSTRAINT [FK_Filmai_Zanrai_ZanraiId]
GO

