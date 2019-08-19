USE [FilmaiDB]
GO

/****** Object:  Table [dbo].[Zanrai]    Script Date: 2019.08.17 23:00:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Zanrai](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Pavadinimas] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Zanrai] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
