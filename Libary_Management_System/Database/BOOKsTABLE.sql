USE [LibraryDatabase]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 7/11/2025 9:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Author] [nvarchar](150) NULL,
	[ISBN] [nvarchar](20) NOT NULL,
	[Publisher] [nvarchar](100) NULL,
	[PublishedDate] [date] NULL,
	[CategoryID] [int] NOT NULL,
	[CoverImagePath] [nvarchar](255) NULL,
	[TotalCopies] [int] NOT NULL,
	[AvailableCopies] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 
GO
INSERT [dbo].[Books] ([BookID], [Title], [Author], [ISBN], [Publisher], [PublishedDate], [CategoryID], [CoverImagePath], [TotalCopies], [AvailableCopies]) VALUES (2, N'Silver Fate', N'Steven Lee', N'978-3-16-148410-1', N'Steven Lees Moc', CAST(N'2010-03-05' AS Date), 2, N'/uploads/2191d225-a4d4-40e1-8bbf-929e3f33d41a.jpg', 2, 1)
GO
INSERT [dbo].[Books] ([BookID], [Title], [Author], [ISBN], [Publisher], [PublishedDate], [CategoryID], [CoverImagePath], [TotalCopies], [AvailableCopies]) VALUES (3, N'Love in the Rain', N'zeeven Leonardo', N'978-3-16-148410-0', N'Zeeven Studio', CAST(N'2023-03-05' AS Date), 5, N'/uploads/6eb69a87-a0fb-4ff8-9db6-642688317a9f.jpg', 20, 19)
GO
INSERT [dbo].[Books] ([BookID], [Title], [Author], [ISBN], [Publisher], [PublishedDate], [CategoryID], [CoverImagePath], [TotalCopies], [AvailableCopies]) VALUES (4, N'A Part of You', N'Lyden Wick', N'978-3-16-138410-2', N'Wick Publisher', CAST(N'2013-02-04' AS Date), 7, N'/uploads/d78b15f5-ddc3-473d-b36c-1c1e4e4542d2.jpg', 12, 10)
GO
INSERT [dbo].[Books] ([BookID], [Title], [Author], [ISBN], [Publisher], [PublishedDate], [CategoryID], [CoverImagePath], [TotalCopies], [AvailableCopies]) VALUES (5, N'rruhivj', N'Riad', N'reg5htrn', N'Zeeven Studio', CAST(N'2000-03-04' AS Date), 8, N'/uploads/594d7e07-621d-471d-9231-0d77f7bbd46c.jpg', 2, 1)
GO
INSERT [dbo].[Books] ([BookID], [Title], [Author], [ISBN], [Publisher], [PublishedDate], [CategoryID], [CoverImagePath], [TotalCopies], [AvailableCopies]) VALUES (7, N'ygujk', N'Steven Lee', N'ijikl', N'Zeeven Studio', CAST(N'2050-03-06' AS Date), 8, N'/uploads/3dd7d951-709a-4afc-ae91-6bfb06961188.jpg', 2, 2)
GO
INSERT [dbo].[Books] ([BookID], [Title], [Author], [ISBN], [Publisher], [PublishedDate], [CategoryID], [CoverImagePath], [TotalCopies], [AvailableCopies]) VALUES (8, N'tghjmk,', N'bjk', N'978-3-16-148410-8', N'Zeeven Studio', CAST(N'2034-04-06' AS Date), 7, N'/uploads/eca0589a-63b5-4f67-a633-56d1d6bf1967.jpg', 34, 33)
GO
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookCategories_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[BookCategories] ([CategoryID])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_BookCategories_CategoryID]
GO
