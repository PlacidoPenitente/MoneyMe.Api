USE [master]
GO
/****** Object:  Database [moneyme_dev]    Script Date: 9/20/2022 8:50:37 AM ******/
CREATE DATABASE [moneyme_dev]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'moneyme_dev', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\moneyme_dev.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'moneyme_dev_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\moneyme_dev_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [moneyme_dev] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [moneyme_dev].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [moneyme_dev] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [moneyme_dev] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [moneyme_dev] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [moneyme_dev] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [moneyme_dev] SET ARITHABORT OFF 
GO
ALTER DATABASE [moneyme_dev] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [moneyme_dev] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [moneyme_dev] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [moneyme_dev] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [moneyme_dev] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [moneyme_dev] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [moneyme_dev] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [moneyme_dev] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [moneyme_dev] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [moneyme_dev] SET  DISABLE_BROKER 
GO
ALTER DATABASE [moneyme_dev] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [moneyme_dev] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [moneyme_dev] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [moneyme_dev] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [moneyme_dev] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [moneyme_dev] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [moneyme_dev] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [moneyme_dev] SET RECOVERY FULL 
GO
ALTER DATABASE [moneyme_dev] SET  MULTI_USER 
GO
ALTER DATABASE [moneyme_dev] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [moneyme_dev] SET DB_CHAINING OFF 
GO
ALTER DATABASE [moneyme_dev] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [moneyme_dev] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [moneyme_dev] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [moneyme_dev] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'moneyme_dev', N'ON'
GO
ALTER DATABASE [moneyme_dev] SET QUERY_STORE = OFF
GO
USE [moneyme_dev]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/20/2022 8:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 9/20/2022 8:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[MobileNumber] [nvarchar](max) NULL,
	[EmailAddress] [nvarchar](max) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
 CONSTRAINT [PK_customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fees]    Script Date: 9/20/2022 8:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fees](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](450) NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[IsPercentage] [bit] NOT NULL,
 CONSTRAINT [PK_fees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[loans]    Script Date: 9/20/2022 8:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[loans](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[LoanAmount] [decimal](18, 2) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
 CONSTRAINT [PK_loans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payments]    Script Date: 9/20/2022 8:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payments](
	[Id] [uniqueidentifier] NOT NULL,
	[Period] [int] NOT NULL,
	[Interest] [decimal](18, 2) NOT NULL,
	[Principal] [decimal](18, 2) NOT NULL,
	[LoanId] [uniqueidentifier] NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
 CONSTRAINT [PK_payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_fees]    Script Date: 9/20/2022 8:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_fees](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[FeeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_product_fees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 9/20/2022 8:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](450) NULL,
	[InterestRate] [decimal](18, 4) NOT NULL,
	[MaximumDuration] [int] NOT NULL,
	[MinimumDuration] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[RuleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[quotes]    Script Date: 9/20/2022 8:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quotes](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[LoanAmount] [decimal](18, 2) NOT NULL,
	[Term] [int] NOT NULL,
	[Interest] [decimal](18, 2) NOT NULL,
	[MonthlyPayment] [decimal](18, 2) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
 CONSTRAINT [PK_quotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rules]    Script Date: 9/20/2022 8:50:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rules](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](450) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
 CONSTRAINT [PK_rules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220907024831_AddedPersistenceModels', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220907201200_UpdatedFee', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220909165400_RenamedDateAdded', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220909221800_RenamedFeeDateAdded', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220909231146_MakeDateModifiedNullable', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220910033151_ChangedFeeProductRelationship', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220910034031_MadeProductNameUnique', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220914163702_AddedRule', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220914212536_AddedDomainService', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220915164900_AddFeeType', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220915184246_MovedFees', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220915191553_ChangeFeProductRelationship', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916163437_FixedProductAndFee', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916181210_FixedProductAndFeeTable', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916181340_RenamedTable', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916231940_RemoveConstraint', N'5.0.17')
GO
INSERT [dbo].[customers] ([Id], [Title], [FirstName], [LastName], [DateOfBirth], [MobileNumber], [EmailAddress], [DateCreated], [DateModified]) VALUES (N'9d2f94e9-7909-44b8-9eb2-4da4b9b2123a', N'Mr', N'John', N'doe', CAST(N'2004-09-02T00:00:00.0000000' AS DateTime2), N'0422111333', N'ghjghj@flower.com.au', CAST(N'2022-09-20T00:48:00.7530056' AS DateTime2), NULL)
GO
INSERT [dbo].[fees] ([Id], [Name], [Amount], [DateCreated], [DateModified], [IsPercentage]) VALUES (N'c7235cb1-6bf3-4c00-9eb4-1149f0d6de06', N'Establishment Fee', CAST(300.00 AS Decimal(18, 2)), CAST(N'2022-09-20T00:40:03.2202465' AS DateTime2), NULL, 0)
INSERT [dbo].[fees] ([Id], [Name], [Amount], [DateCreated], [DateModified], [IsPercentage]) VALUES (N'43f23d6f-a9d7-43d5-a93a-da9d3ff2a373', N'Origination Fee', CAST(100.00 AS Decimal(18, 2)), CAST(N'2022-09-20T00:40:15.7876352' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[product_fees] ([Id], [ProductId], [FeeId]) VALUES (N'262d5b92-94ed-4f43-805b-ae4bd9deecec', N'7e8f1aaa-35fb-4def-a2e7-ead96db78efa', N'c7235cb1-6bf3-4c00-9eb4-1149f0d6de06')
INSERT [dbo].[product_fees] ([Id], [ProductId], [FeeId]) VALUES (N'c55affb2-b32d-4542-bcf4-f53023cf5964', N'38910276-aad4-4b78-9bf1-311f53956a00', N'c7235cb1-6bf3-4c00-9eb4-1149f0d6de06')
GO
INSERT [dbo].[products] ([Id], [Name], [InterestRate], [MaximumDuration], [MinimumDuration], [DateCreated], [DateModified], [RuleId]) VALUES (N'38910276-aad4-4b78-9bf1-311f53956a00', N'Product A (Interest Free)', CAST(0.0000 AS Decimal(18, 4)), 5, 1, CAST(N'2022-09-20T00:45:07.9935930' AS DateTime2), NULL, N'3aeb1dc9-cfde-47aa-a832-aa3147133148')
INSERT [dbo].[products] ([Id], [Name], [InterestRate], [MaximumDuration], [MinimumDuration], [DateCreated], [DateModified], [RuleId]) VALUES (N'7e8f1aaa-35fb-4def-a2e7-ead96db78efa', N'Product B (No Interest Free)', CAST(9.4900 AS Decimal(18, 4)), 36, 12, CAST(N'2022-09-20T00:46:22.8829055' AS DateTime2), NULL, N'2e9aeb82-69e3-40c3-b112-7a844babf683')
GO
INSERT [dbo].[quotes] ([Id], [CustomerId], [ProductId], [LoanAmount], [Term], [Interest], [MonthlyPayment], [DateCreated], [DateModified]) VALUES (N'802bb160-8af0-41ea-b698-13ad96627d59', N'9d2f94e9-7909-44b8-9eb2-4da4b9b2123a', N'7e8f1aaa-35fb-4def-a2e7-ead96db78efa', CAST(8800.00 AS Decimal(18, 2)), 22, CAST(850.41 AS Decimal(18, 2)), CAST(452.29 AS Decimal(18, 2)), CAST(N'2022-09-20T00:49:23.2904238' AS DateTime2), CAST(N'2022-09-20T00:49:23.2953578' AS DateTime2))
INSERT [dbo].[quotes] ([Id], [CustomerId], [ProductId], [LoanAmount], [Term], [Interest], [MonthlyPayment], [DateCreated], [DateModified]) VALUES (N'98ef6328-cdc7-4ae8-8bfe-db4803f046ad', N'9d2f94e9-7909-44b8-9eb2-4da4b9b2123a', N'7e8f1aaa-35fb-4def-a2e7-ead96db78efa', CAST(2100.00 AS Decimal(18, 2)), 26, CAST(264.63 AS Decimal(18, 2)), CAST(102.49 AS Decimal(18, 2)), CAST(N'2022-09-20T00:48:29.8991046' AS DateTime2), CAST(N'2022-09-20T00:48:29.9231379' AS DateTime2))
GO
INSERT [dbo].[rules] ([Id], [Name], [DateCreated], [DateModified]) VALUES (N'2e9aeb82-69e3-40c3-b112-7a844babf683', N'No Interest Free Rule', CAST(N'2022-09-20T00:40:55.0050513' AS DateTime2), NULL)
INSERT [dbo].[rules] ([Id], [Name], [DateCreated], [DateModified]) VALUES (N'3aeb1dc9-cfde-47aa-a832-aa3147133148', N'Interest Free Rule', CAST(N'2022-09-20T00:41:03.1726508' AS DateTime2), NULL)
INSERT [dbo].[rules] ([Id], [Name], [DateCreated], [DateModified]) VALUES (N'd7c21b24-9510-4fb5-92ba-ab0316f0ea72', N'First Two Months Interest Free Rule', CAST(N'2022-09-20T00:41:41.2623132' AS DateTime2), NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_fees_Name]    Script Date: 9/20/2022 8:50:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_fees_Name] ON [dbo].[fees]
(
	[Name] ASC
)
WHERE ([Name] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_payments_LoanId]    Script Date: 9/20/2022 8:50:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_payments_LoanId] ON [dbo].[payments]
(
	[LoanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_product_fees_FeeId]    Script Date: 9/20/2022 8:50:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_product_fees_FeeId] ON [dbo].[product_fees]
(
	[FeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_product_fees_ProductId]    Script Date: 9/20/2022 8:50:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_product_fees_ProductId] ON [dbo].[product_fees]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_products_Name]    Script Date: 9/20/2022 8:50:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_products_Name] ON [dbo].[products]
(
	[Name] ASC
)
WHERE ([Name] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_rules_Name]    Script Date: 9/20/2022 8:50:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_rules_Name] ON [dbo].[rules]
(
	[Name] ASC
)
WHERE ([Name] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[fees] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsPercentage]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [RuleId]
GO
ALTER TABLE [dbo].[payments]  WITH CHECK ADD  CONSTRAINT [FK_payments_loans_LoanId] FOREIGN KEY([LoanId])
REFERENCES [dbo].[loans] ([Id])
GO
ALTER TABLE [dbo].[payments] CHECK CONSTRAINT [FK_payments_loans_LoanId]
GO
ALTER TABLE [dbo].[product_fees]  WITH CHECK ADD  CONSTRAINT [FK_product_fees_fees_FeeId] FOREIGN KEY([FeeId])
REFERENCES [dbo].[fees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[product_fees] CHECK CONSTRAINT [FK_product_fees_fees_FeeId]
GO
ALTER TABLE [dbo].[product_fees]  WITH CHECK ADD  CONSTRAINT [FK_product_fees_products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[product_fees] CHECK CONSTRAINT [FK_product_fees_products_ProductId]
GO
USE [master]
GO
ALTER DATABASE [moneyme_dev] SET  READ_WRITE 
GO
