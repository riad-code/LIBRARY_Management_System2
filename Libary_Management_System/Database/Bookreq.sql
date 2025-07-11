USE [LibraryDatabase]
GO
/****** Object:  Table [dbo].[BookRequests]    Script Date: 7/11/2025 9:41:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookRequests](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](450) NOT NULL,
	[BookID] [int] NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[IsApproved] [bit] NULL,
	[ApprovalDate] [datetime] NULL,
	[Status] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BookRequests]  WITH CHECK ADD  CONSTRAINT [FK_BookRequests_Book] FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([BookID])
GO
ALTER TABLE [dbo].[BookRequests] CHECK CONSTRAINT [FK_BookRequests_Book]
GO
ALTER TABLE [dbo].[BookRequests]  WITH CHECK ADD  CONSTRAINT [FK_BookRequests_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[BookRequests] CHECK CONSTRAINT [FK_BookRequests_User]
GO
