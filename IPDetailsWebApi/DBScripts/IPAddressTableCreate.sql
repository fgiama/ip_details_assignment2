USE [IPDetailsDB]
GO

/****** Object:  Table [dbo].[IPAddress]    Script Date: 9/15/2019 5:06:53 PM ******/
DROP TABLE [dbo].[IPAddress]
GO

/****** Object:  Table [dbo].[IPAddress]    Script Date: 9/15/2019 5:06:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IPAddress](
	[IP] [varchar](50) NOT NULL,
 CONSTRAINT [PK_IPAddress] PRIMARY KEY CLUSTERED 
(
	[IP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


