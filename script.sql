USE [master]
GO
/****** Object:  Database [project]    Script Date: 11/21/2019 9:15:21 AM ******/
CREATE DATABASE [project]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'project_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\project.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'project_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\project.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [project] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [project].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [project] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [project] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [project] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [project] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [project] SET ARITHABORT OFF 
GO
ALTER DATABASE [project] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [project] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [project] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [project] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [project] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [project] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [project] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [project] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [project] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [project] SET  DISABLE_BROKER 
GO
ALTER DATABASE [project] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [project] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [project] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [project] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [project] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [project] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [project] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [project] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [project] SET  MULTI_USER 
GO
ALTER DATABASE [project] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [project] SET DB_CHAINING OFF 
GO
ALTER DATABASE [project] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [project] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [project] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'project', N'ON'
GO
ALTER DATABASE [project] SET QUERY_STORE = OFF
GO
USE [project]
GO
/****** Object:  Table [dbo].[author]    Script Date: 11/21/2019 9:15:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[author](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_author] PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[book]    Script Date: 11/21/2019 9:15:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[book](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[ISBN] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[YearPublished] [date] NOT NULL,
	[QOH] [int] NOT NULL,
	[AuthorID] [int] NOT NULL,
	[PublishID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
 CONSTRAINT [PK_book_id] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bookOrder]    Script Date: 11/21/2019 9:15:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bookOrder](
	[CustomerOrderID] [int] NOT NULL,
	[BookID] [int] NOT NULL,
	[OrderQuantity] [int] NOT NULL,
 CONSTRAINT [PK_bookOrder] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderID] ASC,
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 11/21/2019 9:15:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nchar](50) NOT NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 11/21/2019 9:15:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[PostalCode] [nvarchar](10) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[FaxNumber] [nvarchar](20) NOT NULL,
	[CreditLimit] [float] NOT NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customerOrder]    Script Date: 11/21/2019 9:15:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customerOrder](
	[CustomerOrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[OrderDate] [date] NOT NULL,
 CONSTRAINT [PK_customerOrder] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[invoice]    Script Date: 11/21/2019 9:15:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoice](
	[InvoiceID] [int] NOT NULL,
	[TotalPrice] [float] NOT NULL,
	[TypeOfPayment] [varchar](50) NOT NULL,
	[PaymentDate] [date] NULL,
	[IsPaid] [nchar](1) NOT NULL,
 CONSTRAINT [PK_invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[publisher]    Script Date: 11/21/2019 9:15:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[publisher](
	[PublisherID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_publisher] PRIMARY KEY CLUSTERED 
(
	[PublisherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 11/21/2019 9:15:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 11/21/2019 9:15:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[author] ON 

INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (1, N'Fern', N'Michaels', N'FernMichaels@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (2, N'Sinead', N'Moriarty', N'SineadMoriarty@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (8, N'Sarah', N'Dunn', N'SarahDunn@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (9, N'Cassandra', N'Dunn', N'CassandraDunn@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (10, N'Elise', N'Juska', N'EliseJuska@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (11, N'Herbert', N'Schildt', N'HerbertSchildt@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (12, N'Joyce', N'Farrel', N'JoyceFarrel@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (13, N'Daniel', N' Bell', N'DanielBell@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (14, N' Andrew', N'Troelsen', N' AndrewTroelsen@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (15, N' Nathan', N'Metzler', N' NathanMetzler@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (16, N' Matt ', N'Neuburg', N' MattNeuburg@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (17, N'Robin', N'Nixon', N'RobinNixon@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (18, N' Chris', N' Colfer', N'ChrisColfer@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (19, N'Josh', N' Crute', N'JoshCrute@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (20, N'Agnes', N'Green', N'AgnesGreen@gmail.com')
INSERT [dbo].[author] ([AuthorID], [FirstName], [LastName], [Email]) VALUES (21, N'Amanda', N'Jones', N'AmandaJones@gmail.com')
SET IDENTITY_INSERT [dbo].[author] OFF
SET IDENTITY_INSERT [dbo].[book] ON 

INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (18, N'0-13-896598-6', N'This Child Of Mine', 12.5, CAST(N'2013-01-10' AS Date), 120, 2, 1, 1)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (19, N'1-11-826598-4', N'Crash And Burn', 9.8, CAST(N'2016-03-06' AS Date), 190, 1, 2, 1)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (21, N'1-12-566231-2', N'Perfect Match', 8.6, CAST(N'2015-07-08' AS Date), 86, 1, 2, 1)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (22, N'1-16-574231-4', N'Secrets  Of Happiness', 11.36, CAST(N'2017-05-19' AS Date), 260, 8, 3, 1)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (23, N'0-12-830598-6', N'The Art Of Adapting', 12.65, CAST(N'2015-10-20' AS Date), 135, 9, 4, 1)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (24, N'0-12-856598-9', N'The Blessings', 15.65, CAST(N'2013-11-11' AS Date), 235, 10, 3, 1)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (25, N'5-09-726528-9', N'Java: The Complete Reference', 56.59, CAST(N'2019-12-20' AS Date), 260, 11, 9, 2)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (26, N'5-08-726628-7', N'Java Programming ', 154.95, CAST(N'2018-01-15' AS Date), 320, 12, 10, 2)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (27, N'5-08-777728-2', N'Java: The Book For The Absolute Beginner', 18.86, CAST(N'2019-03-15' AS Date), 300, 13, 11, 2)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (28, N'5-07-566628-7', N'Pro C# 7: With .NET and .NET Core', 59.56, CAST(N'2017-06-09' AS Date), 265, 14, 12, 2)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (29, N'5-07-666666-7', N'C# for Beginners', 7.9, CAST(N'2018-09-17' AS Date), 220, 15, 12, 2)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (30, N'5-01-555555-8', N'IOS 12 Programming Fundamentals', 63.68, CAST(N'2018-02-06' AS Date), 315, 16, 13, 2)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (31, N'5-02-724528-9', N'Learning PHP, MySQL & JavaScript', 38.99, CAST(N'2018-04-08' AS Date), 0, 17, 13, 2)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (32, N'6-06-763528-9', N'The Land of Stories', 12.99, CAST(N'2017-05-06' AS Date), 256, 18, 3, 3)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (33, N'6-05-123524-1', N'Toy Story 4 Little Golden Book ', 5.45, CAST(N'2019-11-18' AS Date), 285, 19, 14, 3)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (34, N'6-05-567890-1', N'Today I''m a Monster', 19.11, CAST(N'2017-07-15' AS Date), 420, 20, 15, 3)
INSERT [dbo].[book] ([BookID], [ISBN], [Title], [UnitPrice], [YearPublished], [QOH], [AuthorID], [PublishID], [CategoryID]) VALUES (35, N'6-06-123456-1', N'Children It''s Time to Meet Your Teeth', 15.3, CAST(N'2017-05-07' AS Date), 415, 21, 16, 3)
SET IDENTITY_INSERT [dbo].[book] OFF
INSERT [dbo].[bookOrder] ([CustomerOrderID], [BookID], [OrderQuantity]) VALUES (1, 19, 10)
INSERT [dbo].[bookOrder] ([CustomerOrderID], [BookID], [OrderQuantity]) VALUES (1, 21, 50)
INSERT [dbo].[bookOrder] ([CustomerOrderID], [BookID], [OrderQuantity]) VALUES (2, 21, 14)
INSERT [dbo].[bookOrder] ([CustomerOrderID], [BookID], [OrderQuantity]) VALUES (2, 26, 120)
INSERT [dbo].[bookOrder] ([CustomerOrderID], [BookID], [OrderQuantity]) VALUES (2, 28, 10)
INSERT [dbo].[bookOrder] ([CustomerOrderID], [BookID], [OrderQuantity]) VALUES (4, 28, 100)
INSERT [dbo].[bookOrder] ([CustomerOrderID], [BookID], [OrderQuantity]) VALUES (5, 21, 55)
INSERT [dbo].[bookOrder] ([CustomerOrderID], [BookID], [OrderQuantity]) VALUES (9, 19, 60)
INSERT [dbo].[bookOrder] ([CustomerOrderID], [BookID], [OrderQuantity]) VALUES (11, 21, 45)
INSERT [dbo].[bookOrder] ([CustomerOrderID], [BookID], [OrderQuantity]) VALUES (12, 26, 120)
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([CategoryID], [CategoryName]) VALUES (1, N'Novel                                             ')
INSERT [dbo].[category] ([CategoryID], [CategoryName]) VALUES (2, N'Program                                           ')
INSERT [dbo].[category] ([CategoryID], [CategoryName]) VALUES (3, N'Story                                             ')
SET IDENTITY_INSERT [dbo].[category] OFF
SET IDENTITY_INSERT [dbo].[customer] ON 

INSERT [dbo].[customer] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (1, N'University Of  Montreal', N'2158 ave bennett ', N'Montreal', N'P1Q 3T5', N'514-888-6633', N'514-888-6634', 5000)
INSERT [dbo].[customer] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (4, N'University Of  Mc-Gill', N'845 Rue Sherbrooke Ouest', N'Montreal', N'H3A 0G4', N'438-887-6958', N'438-887-6988', 6000)
INSERT [dbo].[customer] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (8, N'Cgep Saint-Laurant', N' 625 Avenue Sainte-Croix', N'Saint-Laurent', N'H4L 3X7', N'666-6666-666', N'514-747-6523', 5000)
INSERT [dbo].[customer] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (10, N'dfdfdffdfdf', N'1455 Boulevard de Maisonneuve O', N'Montreal', N' H3G 1M8', N'555-555-5555', N'514-887-9967', 6000)
INSERT [dbo].[customer] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (11, N'University Of Quebec', N' 475 Rue du Parvis', N'Quebec', N'G1K 9H7', N'666-777-8888', N'418-657-3555', 5500)
INSERT [dbo].[customer] ([CustomerID], [CustomerName], [Street], [City], [PostalCode], [PhoneNumber], [FaxNumber], [CreditLimit]) VALUES (12, N'LaSalle College', N'dfdfdf', N'montreal', N'dfd', N'514-888-8888', N'514-888-9999', 9999)
SET IDENTITY_INSERT [dbo].[customer] OFF
SET IDENTITY_INSERT [dbo].[customerOrder] ON 

INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (1, 1, CAST(N'2019-06-06' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (2, 1, CAST(N'2019-05-11' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (3, 4, CAST(N'2019-01-10' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (4, 4, CAST(N'2019-03-03' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (5, 8, CAST(N'2019-05-07' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (6, 8, CAST(N'2018-12-20' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (7, 8, CAST(N'2018-11-15' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (8, 10, CAST(N'2018-05-09' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (9, 10, CAST(N'2019-08-11' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (10, 11, CAST(N'2018-09-15' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (11, 11, CAST(N'2019-01-30' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (12, 11, CAST(N'2019-06-25' AS Date))
INSERT [dbo].[customerOrder] ([CustomerOrderID], [CustomerID], [OrderDate]) VALUES (17, 4, CAST(N'2019-07-28' AS Date))
SET IDENTITY_INSERT [dbo].[customerOrder] OFF
INSERT [dbo].[invoice] ([InvoiceID], [TotalPrice], [TypeOfPayment], [PaymentDate], [IsPaid]) VALUES (1, 18.439999999999998, N'Cash', CAST(N'2019-07-12' AS Date), N'Y')
SET IDENTITY_INSERT [dbo].[publisher] ON 

INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (1, N'Penguin Books')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (2, N'Kensingtion Books')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (3, N'Hachette Books')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (4, N'Simonands Books')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (9, N'Mcgraw-Hill Books')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (10, N'Cengagebrain Books')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (11, N'Guzzlar Media Books ')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (12, N'Apress Media Books')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (13, N'Oreilty MediasBooks')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (14, N'Little Golden Books')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (15, N'Apriltale Books')
INSERT [dbo].[publisher] ([PublisherID], [Name]) VALUES (16, N'Amazel Enterprise')
SET IDENTITY_INSERT [dbo].[publisher] OFF
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([RoleID], [Name]) VALUES (1, N'MIS Manager ')
INSERT [dbo].[role] ([RoleID], [Name]) VALUES (2, N'Sales Manager ')
INSERT [dbo].[role] ([RoleID], [Name]) VALUES (3, N'Order Clerks')
INSERT [dbo].[role] ([RoleID], [Name]) VALUES (4, N'Accountant')
INSERT [dbo].[role] ([RoleID], [Name]) VALUES (5, N'Inventory Controller ')
SET IDENTITY_INSERT [dbo].[role] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([UserID], [FirstName], [LastName], [UserName], [Password], [RoleID]) VALUES (1, N'Zhe', N'Yang', N'zhe', N'zhe123', 1)
INSERT [dbo].[users] ([UserID], [FirstName], [LastName], [UserName], [Password], [RoleID]) VALUES (2, N'Krunal', N'Patel', N'krunal', N'123', 2)
INSERT [dbo].[users] ([UserID], [FirstName], [LastName], [UserName], [Password], [RoleID]) VALUES (3, N'Mary', N'Brown', N'mary', N'mary123', 3)
INSERT [dbo].[users] ([UserID], [FirstName], [LastName], [UserName], [Password], [RoleID]) VALUES (5, N'Jennifer', N'Bouchard', N'jennifer', N'jennifer123', 3)
INSERT [dbo].[users] ([UserID], [FirstName], [LastName], [UserName], [Password], [RoleID]) VALUES (6, N'KimHoa', N'Nguyen', N'kim', N'kim123', 4)
INSERT [dbo].[users] ([UserID], [FirstName], [LastName], [UserName], [Password], [RoleID]) VALUES (7, N'Peter', N'Wang', N'peter', N'peter123', 5)
INSERT [dbo].[users] ([UserID], [FirstName], [LastName], [UserName], [Password], [RoleID]) VALUES (9, N'zhedas', N'zhe2dasd', N'zhe2', N'123asdsad', 1)
SET IDENTITY_INSERT [dbo].[users] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [ISBN_Unique]    Script Date: 11/21/2019 9:15:25 AM ******/
ALTER TABLE [dbo].[book] ADD  CONSTRAINT [ISBN_Unique] UNIQUE NONCLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_bookOrder]    Script Date: 11/21/2019 9:15:25 AM ******/
CREATE NONCLUSTERED INDEX [IX_bookOrder] ON [dbo].[bookOrder]
(
	[CustomerOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Unique_UserName]    Script Date: 11/21/2019 9:15:25 AM ******/
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [Unique_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[book]  WITH CHECK ADD  CONSTRAINT [FK_book_author] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[author] ([AuthorID])
GO
ALTER TABLE [dbo].[book] CHECK CONSTRAINT [FK_book_author]
GO
ALTER TABLE [dbo].[book]  WITH NOCHECK ADD  CONSTRAINT [FK_book_category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[category] ([CategoryID])
GO
ALTER TABLE [dbo].[book] CHECK CONSTRAINT [FK_book_category]
GO
ALTER TABLE [dbo].[book]  WITH CHECK ADD  CONSTRAINT [FK_book_publisher] FOREIGN KEY([PublishID])
REFERENCES [dbo].[publisher] ([PublisherID])
GO
ALTER TABLE [dbo].[book] CHECK CONSTRAINT [FK_book_publisher]
GO
ALTER TABLE [dbo].[bookOrder]  WITH CHECK ADD  CONSTRAINT [FK_bookOrder_book_ID] FOREIGN KEY([BookID])
REFERENCES [dbo].[book] ([BookID])
GO
ALTER TABLE [dbo].[bookOrder] CHECK CONSTRAINT [FK_bookOrder_book_ID]
GO
ALTER TABLE [dbo].[bookOrder]  WITH CHECK ADD  CONSTRAINT [FK_bookOrder_customerOrder] FOREIGN KEY([CustomerOrderID])
REFERENCES [dbo].[customerOrder] ([CustomerOrderID])
GO
ALTER TABLE [dbo].[bookOrder] CHECK CONSTRAINT [FK_bookOrder_customerOrder]
GO
ALTER TABLE [dbo].[customerOrder]  WITH CHECK ADD  CONSTRAINT [FK_customerOrder_customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[customer] ([CustomerID])
GO
ALTER TABLE [dbo].[customerOrder] CHECK CONSTRAINT [FK_customerOrder_customer]
GO
ALTER TABLE [dbo].[invoice]  WITH CHECK ADD  CONSTRAINT [FK_invoice_customerOrder] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[customerOrder] ([CustomerOrderID])
GO
ALTER TABLE [dbo].[invoice] CHECK CONSTRAINT [FK_invoice_customerOrder]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[role] ([RoleID])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_role]
GO
USE [master]
GO
ALTER DATABASE [project] SET  READ_WRITE 
GO
