USE [IPDetailsDB]
GO

/****** Object:  Table [dbo].[IPDetails]    Script Date: 9/15/2019 5:06:59 PM ******/
DROP TABLE [dbo].[IPDetails]
GO

/****** Object:  Table [dbo].[IPDetails]    Script Date: 9/15/2019 5:06:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IPDetails](
	[Ip] [varchar](50) NOT NULL,
	[City] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Latitude] [varchar](50) NULL,
	[Longitude] [varchar](50) NULL,
	[Continent] [varchar](50) NULL,
 CONSTRAINT [PK_IPDetails] PRIMARY KEY CLUSTERED 
(
	[Ip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


