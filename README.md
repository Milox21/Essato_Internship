# Essato_Internship

Here i will list things to setup my app

1. I was using Visual Studio with NuGet Packages:
  a) CommunityToolKit.Mvvm (by Microsoft)
  b) Microsoft.EntityFrameworkCore
  c) Microsoft.EntityFrameworkCore.SqlServer
  d) Microsoft.EntityFrameworkCore.Tools

2. To setup connection to database, a connection string should be changed in DatabaseContext in method OnConfiguring

3. I was using Microsoft SQL Management Studio, and sended it with the other files, if there were any problems, here is script of database:
   
USE [PatientDatabase]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 04.04.2024 00:55:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PESEL] [nvarchar](11) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[Zipcode] [nvarchar](10) NOT NULL,
	[isActive] [bit] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[LastEditedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
