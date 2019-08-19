USE [FilmaiDB]
GO

/****** Object:  Table [dbo].[AktoriaiFilmai]    Script Date: 2019.08.17 23:00:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AktoriaiFilmai](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AktoriusId] [int] NOT NULL,
	[FilmasId] [int] NOT NULL,
 CONSTRAINT [PK_AktoriaiFilmai] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AktoriaiFilmai]  WITH CHECK ADD  CONSTRAINT [FK_AktoriaiFilmai_Aktoriai_AktoriusId] FOREIGN KEY([AktoriusId])
REFERENCES [dbo].[Aktoriai] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AktoriaiFilmai] CHECK CONSTRAINT [FK_AktoriaiFilmai_Aktoriai_AktoriusId]
GO

ALTER TABLE [dbo].[AktoriaiFilmai]  WITH CHECK ADD  CONSTRAINT [FK_AktoriaiFilmai_Filmai_FilmasId] FOREIGN KEY([FilmasId])
REFERENCES [dbo].[Filmai] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AktoriaiFilmai] CHECK CONSTRAINT [FK_AktoriaiFilmai_Filmai_FilmasId]
GO

