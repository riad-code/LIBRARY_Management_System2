USE [LibraryDatabase]
GO
/****** Object:  Table [dbo].[BorrowRecords]    Script Date: 7/11/2025 9:39:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BorrowRecords](
	[BorrowID] [int] IDENTITY(1,1) NOT NULL,
	[BookID] [int] NOT NULL,
	[UserID] [nvarchar](450) NOT NULL,
	[BorrowDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[ReturnDate] [datetime] NULL,
	[FineAmount] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[BorrowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BorrowRecords]  WITH CHECK ADD  CONSTRAINT [FK_BorrowRecords_Book] FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([BookID])
GO
ALTER TABLE [dbo].[BorrowRecords] CHECK CONSTRAINT [FK_BorrowRecords_Book]
GO
ALTER TABLE [dbo].[BorrowRecords]  WITH CHECK ADD  CONSTRAINT [FK_BorrowRecords_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[BorrowRecords] CHECK CONSTRAINT [FK_BorrowRecords_User]
GO
