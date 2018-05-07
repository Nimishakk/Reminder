USE [information]
GO
/****** Object:  Table [dbo].[Save_Info]    Script Date: 5/7/2018 4:01:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Save_Info](
	[Information_id] [int] IDENTITY(1,1) NOT NULL,
	[Information] [varchar](500) NULL,
	[Start_date] [datetime] NULL,
	[End_date] [datetime] NULL
) ON [PRIMARY]
GO
