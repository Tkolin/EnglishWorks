USE [master]
GO
/****** Object:  Database [EnglishKlassBD]    Script Date: 18.03.2023 4:33:39 ******/
CREATE DATABASE [EnglishKlassBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EnglishKlassBD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\EnglishKlassBD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EnglishKlassBD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\EnglishKlassBD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [EnglishKlassBD] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EnglishKlassBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EnglishKlassBD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET ARITHABORT OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EnglishKlassBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EnglishKlassBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EnglishKlassBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EnglishKlassBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EnglishKlassBD] SET  MULTI_USER 
GO
ALTER DATABASE [EnglishKlassBD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EnglishKlassBD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EnglishKlassBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EnglishKlassBD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EnglishKlassBD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EnglishKlassBD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [EnglishKlassBD] SET QUERY_STORE = OFF
GO
USE [EnglishKlassBD]
GO
/****** Object:  Table [dbo].[AccountingForTasks]    Script Date: 18.03.2023 4:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountingForTasks](
	[Number] [int] IDENTITY(1,1) NOT NULL,
	[Task_ID] [int] NOT NULL,
	[Student_ID] [int] NULL,
	[DateStart] [date] NULL,
	[DateEnd] [date] NULL,
 CONSTRAINT [PK_AccountingForTasks] PRIMARY KEY CLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassGroup]    Script Date: 18.03.2023 4:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [char](1) NOT NULL,
	[Teacher_ID] [int] NOT NULL,
	[Number] [int] NOT NULL,
 CONSTRAINT [PK_ClassGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genders]    Script Date: 18.03.2023 4:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Genders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 18.03.2023 4:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 18.03.2023 4:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Lastname] [varchar](50) NOT NULL,
	[Firstname] [varchar](50) NULL,
	[Patronymic] [varchar](50) NULL,
	[DateBirth] [date] NOT NULL,
	[Gender_ID] [int] NOT NULL,
	[Class_ID] [int] NOT NULL,
	[Users_ID] [varchar](50) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 18.03.2023 4:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[ContentTasks] [text] NULL,
	[Description] [text] NULL,
	[Type_ID] [int] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 18.03.2023 4:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[ID] [int] NOT NULL,
	[Lastname] [varchar](50) NOT NULL,
	[Firstname] [varchar](50) NOT NULL,
	[Patronymic] [varchar](50) NULL,
	[DateBirth] [date] NULL,
	[Gender_ID] [int] NOT NULL,
	[Users_ID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Teachers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeTasks]    Script Date: 18.03.2023 4:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeTasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TypeTasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 18.03.2023 4:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Role_ID] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AccountingForTasks] ON 

INSERT [dbo].[AccountingForTasks] ([Number], [Task_ID], [Student_ID], [DateStart], [DateEnd]) VALUES (4, 1, 2, CAST(N'2023-10-01' AS Date), CAST(N'2023-10-04' AS Date))
INSERT [dbo].[AccountingForTasks] ([Number], [Task_ID], [Student_ID], [DateStart], [DateEnd]) VALUES (5, 1, 2, CAST(N'2023-10-01' AS Date), NULL)
INSERT [dbo].[AccountingForTasks] ([Number], [Task_ID], [Student_ID], [DateStart], [DateEnd]) VALUES (6, 2, 2, CAST(N'2023-10-04' AS Date), NULL)
INSERT [dbo].[AccountingForTasks] ([Number], [Task_ID], [Student_ID], [DateStart], [DateEnd]) VALUES (7, 2, 2, CAST(N'2023-03-05' AS Date), CAST(N'2023-03-04' AS Date))
INSERT [dbo].[AccountingForTasks] ([Number], [Task_ID], [Student_ID], [DateStart], [DateEnd]) VALUES (9, 2, 2, CAST(N'2023-03-05' AS Date), CAST(N'2023-03-19' AS Date))
INSERT [dbo].[AccountingForTasks] ([Number], [Task_ID], [Student_ID], [DateStart], [DateEnd]) VALUES (10, 2, 9, CAST(N'2023-03-05' AS Date), CAST(N'2023-03-19' AS Date))
INSERT [dbo].[AccountingForTasks] ([Number], [Task_ID], [Student_ID], [DateStart], [DateEnd]) VALUES (11, 2, 2, NULL, NULL)
INSERT [dbo].[AccountingForTasks] ([Number], [Task_ID], [Student_ID], [DateStart], [DateEnd]) VALUES (12, 2, 3, NULL, NULL)
INSERT [dbo].[AccountingForTasks] ([Number], [Task_ID], [Student_ID], [DateStart], [DateEnd]) VALUES (13, 2, 9, NULL, NULL)
SET IDENTITY_INSERT [dbo].[AccountingForTasks] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassGroup] ON 

INSERT [dbo].[ClassGroup] ([ID], [Name], [Teacher_ID], [Number]) VALUES (1, N'А', 1, 6)
INSERT [dbo].[ClassGroup] ([ID], [Name], [Teacher_ID], [Number]) VALUES (2, N'Б', 2, 8)
SET IDENTITY_INSERT [dbo].[ClassGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[Genders] ON 

INSERT [dbo].[Genders] ([ID], [Name]) VALUES (1, N'Мужчина')
INSERT [dbo].[Genders] ([ID], [Name]) VALUES (2, N'Женщина')
SET IDENTITY_INSERT [dbo].[Genders] OFF
GO
INSERT [dbo].[Roles] ([ID], [Name]) VALUES (0, N'admin')
INSERT [dbo].[Roles] ([ID], [Name]) VALUES (1, N'teacher')
INSERT [dbo].[Roles] ([ID], [Name]) VALUES (2, N'user')
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([ID], [Lastname], [Firstname], [Patronymic], [DateBirth], [Gender_ID], [Class_ID], [Users_ID]) VALUES (2, N'Ученек1', N'Ученек1', N'Ученек1', CAST(N'2004-01-25' AS Date), 1, 1, N'13')
INSERT [dbo].[Students] ([ID], [Lastname], [Firstname], [Patronymic], [DateBirth], [Gender_ID], [Class_ID], [Users_ID]) VALUES (3, N'Ученик2', N'Ученик2', N'Ученик2', CAST(N'2000-01-24' AS Date), 2, 1, N'133')
INSERT [dbo].[Students] ([ID], [Lastname], [Firstname], [Patronymic], [DateBirth], [Gender_ID], [Class_ID], [Users_ID]) VALUES (7, N'Ученик3', N'Ученик3', N'Ученик3', CAST(N'2019-01-01' AS Date), 2, 2, N'133')
INSERT [dbo].[Students] ([ID], [Lastname], [Firstname], [Patronymic], [DateBirth], [Gender_ID], [Class_ID], [Users_ID]) VALUES (8, N'afefa', N'aefaef', N'afef', CAST(N'2023-03-10' AS Date), 1, 2, N'133')
INSERT [dbo].[Students] ([ID], [Lastname], [Firstname], [Patronymic], [DateBirth], [Gender_ID], [Class_ID], [Users_ID]) VALUES (9, N'fsadf', N'sadf', N'fasf', CAST(N'2023-03-04' AS Date), 2, 1, N'122')
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([ID], [Name], [ContentTasks], [Description], [Type_ID]) VALUES (1, N'Название задания1', N'задания1', N'описание', 1)
INSERT [dbo].[Tasks] ([ID], [Name], [ContentTasks], [Description], [Type_ID]) VALUES (2, N'Название задания2', N'задание2', N'описание', 2)
INSERT [dbo].[Tasks] ([ID], [Name], [ContentTasks], [Description], [Type_ID]) VALUES (3, N'Название задания4', N'задание3', N'описание', 3)
INSERT [dbo].[Tasks] ([ID], [Name], [ContentTasks], [Description], [Type_ID]) VALUES (4, N'fadf', N'fadfad', N'fadf', 2)
INSERT [dbo].[Tasks] ([ID], [Name], [ContentTasks], [Description], [Type_ID]) VALUES (7, N'fadf1fea', N'fadffae', N'fadffae', 1)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
INSERT [dbo].[Teachers] ([ID], [Lastname], [Firstname], [Patronymic], [DateBirth], [Gender_ID], [Users_ID]) VALUES (1, N'Учитель1', N'Учитель1', N'Учитель1', CAST(N'2000-01-20' AS Date), 1, N'12')
INSERT [dbo].[Teachers] ([ID], [Lastname], [Firstname], [Patronymic], [DateBirth], [Gender_ID], [Users_ID]) VALUES (2, N'Учитель2', N'Учитель2', N'Учитель2', CAST(N'2000-02-10' AS Date), 2, N'122')
GO
SET IDENTITY_INSERT [dbo].[TypeTasks] ON 

INSERT [dbo].[TypeTasks] ([ID], [Name]) VALUES (1, N'Изучение')
INSERT [dbo].[TypeTasks] ([ID], [Name]) VALUES (2, N'Конспектирование')
INSERT [dbo].[TypeTasks] ([ID], [Name]) VALUES (3, N'Учение на изусть')
SET IDENTITY_INSERT [dbo].[TypeTasks] OFF
GO
INSERT [dbo].[Users] ([Login], [Password], [Role_ID]) VALUES (N'11', N'1', 0)
INSERT [dbo].[Users] ([Login], [Password], [Role_ID]) VALUES (N'12', N'1', 1)
INSERT [dbo].[Users] ([Login], [Password], [Role_ID]) VALUES (N'122', N'1', 1)
INSERT [dbo].[Users] ([Login], [Password], [Role_ID]) VALUES (N'13', N'1', 2)
INSERT [dbo].[Users] ([Login], [Password], [Role_ID]) VALUES (N'133', N'1', 2)
GO
ALTER TABLE [dbo].[AccountingForTasks]  WITH CHECK ADD  CONSTRAINT [FK_AccountingForTasks_Students] FOREIGN KEY([Student_ID])
REFERENCES [dbo].[Students] ([ID])
GO
ALTER TABLE [dbo].[AccountingForTasks] CHECK CONSTRAINT [FK_AccountingForTasks_Students]
GO
ALTER TABLE [dbo].[AccountingForTasks]  WITH CHECK ADD  CONSTRAINT [FK_AccountingForTasks_Tasks] FOREIGN KEY([Task_ID])
REFERENCES [dbo].[Tasks] ([ID])
GO
ALTER TABLE [dbo].[AccountingForTasks] CHECK CONSTRAINT [FK_AccountingForTasks_Tasks]
GO
ALTER TABLE [dbo].[ClassGroup]  WITH CHECK ADD  CONSTRAINT [FK_ClassGroup_Teachers] FOREIGN KEY([Teacher_ID])
REFERENCES [dbo].[Teachers] ([ID])
GO
ALTER TABLE [dbo].[ClassGroup] CHECK CONSTRAINT [FK_ClassGroup_Teachers]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_ClassGroup] FOREIGN KEY([Class_ID])
REFERENCES [dbo].[ClassGroup] ([ID])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_ClassGroup]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Genders] FOREIGN KEY([Gender_ID])
REFERENCES [dbo].[Genders] ([ID])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Genders]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Users] FOREIGN KEY([Users_ID])
REFERENCES [dbo].[Users] ([Login])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Users]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_TypeTasks] FOREIGN KEY([Type_ID])
REFERENCES [dbo].[TypeTasks] ([ID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_TypeTasks]
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_Genders] FOREIGN KEY([Gender_ID])
REFERENCES [dbo].[Genders] ([ID])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_Genders]
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_Users] FOREIGN KEY([Users_ID])
REFERENCES [dbo].[Users] ([Login])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([Role_ID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
USE [master]
GO
ALTER DATABASE [EnglishKlassBD] SET  READ_WRITE 
GO
