USE [master]
GO
/****** Object:  Database [HotelDB]    Script Date: 10/31/2013 17:34:56 ******/
CREATE DATABASE [HotelDB] ON  PRIMARY 
( NAME = N'HotelDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\HotelDB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HotelDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\HotelDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [HotelDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HotelDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HotelDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [HotelDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [HotelDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [HotelDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [HotelDB] SET ARITHABORT OFF
GO
ALTER DATABASE [HotelDB] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [HotelDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [HotelDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [HotelDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [HotelDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [HotelDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [HotelDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [HotelDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [HotelDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [HotelDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [HotelDB] SET  DISABLE_BROKER
GO
ALTER DATABASE [HotelDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [HotelDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [HotelDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [HotelDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [HotelDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [HotelDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [HotelDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [HotelDB] SET  READ_WRITE
GO
ALTER DATABASE [HotelDB] SET RECOVERY SIMPLE
GO
ALTER DATABASE [HotelDB] SET  MULTI_USER
GO
ALTER DATABASE [HotelDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [HotelDB] SET DB_CHAINING OFF
GO
USE [HotelDB]
GO
/****** Object:  User [HotelAdmin]    Script Date: 10/31/2013 17:34:56 ******/
CREATE USER [HotelAdmin] FOR LOGIN [HotelAdmin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[zd_zy]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_zy](
	[zy] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_zx]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_zx](
	[zcz] [nvarchar](8) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_ZjLx]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[zd_ZjLx](
	[zjlx] [varchar](50) NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_zd_ZjLx] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[zd_zh]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_zh](
	[zhzt] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_zdfj]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_zdfj](
	[jbdm] [nvarchar](1) NULL,
	[kfjb] [nvarchar](20) NULL,
	[hour] [tinyint] NULL,
	[zdfj] [money] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zd_Wl]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zd_Wl](
	[wlry] [nvarchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_wh]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_wh](
	[whcd] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_tq]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[zd_tq](
	[zktq] [nvarchar](10) NULL,
	[zkl] [money] NULL,
	[hyzk] [money] NULL,
	[TjfZk] [bit] NOT NULL,
	[TqMm] [varchar](50) NULL,
	[TimeLimite] [bit] NOT NULL,
	[default] [bit] NULL,
	[Validate] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[zd_td]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_td](
	[tdlb] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_sy]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_sy](
	[tlsy] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_ql]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_ql](
	[qzlb] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_mz]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_mz](
	[mz] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zd_KrZt]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Zd_KrZt](
	[ZtDm] [char](2) NULL,
	[ZtMc] [char](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[zd_km]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[zd_km](
	[a_km] [varchar](50) NULL,
	[a_jd] [varchar](2) NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_zd_km] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[zd_kl]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_kl](
	[khlb] [nvarchar](10) NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_zd_kl] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_jb]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[zd_jb](
	[jbdm] [nvarchar](1) NULL,
	[kfjb] [nvarchar](20) NULL,
	[f_cw] [tinyint] NULL,
	[f_dj] [money] NULL,
	[zdfj] [money] NULL,
	[btj] [money] NULL,
	[lcf] [money] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[zd_minHours] [float] NULL,
	[zd_maxHours] [float] NULL,
	[zd_jzPrice] [decimal](18, 0) NULL,
	[zd_beginTime] [varchar](4) NULL,
	[zd_endTime] [varchar](4) NULL,
 CONSTRAINT [PK_zd_jb] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Zd_Hmd]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Zd_Hmd](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[xm] [varchar](50) NULL,
	[IdCard] [varchar](50) NULL,
	[address] [varchar](80) NULL,
 CONSTRAINT [PK_Zd_Hmd] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[zd_gj]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_gj](
	[gj] [nvarchar](20) NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_zd_gj] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_fs]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zd_fs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fkfs] [nvarchar](10) NULL,
 CONSTRAINT [PK_zd_fs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zd_FjZt]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[zd_FjZt](
	[FjZt] [varchar](10) NULL,
	[Color] [varchar](10) NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_zd_FJZT] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Zd_Fj]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Zd_Fj](
	[f_fh] [nvarchar](20) NULL,
	[f_zt] [nvarchar](20) NULL,
	[f_ztmc] [nvarchar](20) NULL,
	[f_dm] [nvarchar](20) NULL,
	[f_jb] [nvarchar](20) NULL,
	[f_cw] [tinyint] NULL,
	[f_dj] [money] NULL,
	[HalfDj] [money] NULL,
	[f_jcf] [money] NULL,
	[f_cjf] [money] NULL,
	[s_cz] [nvarchar](20) NULL,
	[ChJb] [nvarchar](20) NULL,
	[ChJg] [money] NULL,
	[ChCw] [tinyint] NULL,
	[ChgRoomZK] [bit] NULL,
	[Tjf] [bit] NULL,
	[Memo] [nvarchar](50) NULL,
	[FixLog] [varchar](200) NULL,
	[SkZk] [bit] NULL,
	[Type] [tinyint] NULL,
	[TypeDesc] [varchar](200) NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[JbID] [int] NULL,
 CONSTRAINT [PK_Zd_Fj] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Yd_Pf]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Yd_Pf](
	[AutoId] [int] IDENTITY(1,1) NOT NULL,
	[id] [varchar](50) NULL,
	[f_dm] [varchar](50) NULL,
	[f_jb] [varchar](50) NULL,
	[f_fh] [varchar](50) NULL,
	[f_dj] [money] NULL,
	[f_jcf] [money] NULL,
	[f_cjf] [money] NULL,
	[z_r] [varchar](50) NULL,
	[z_l] [real] NULL,
	[z_dj] [money] NULL,
	[bz] [varchar](50) NULL,
	[s_cz] [varchar](50) NULL,
	[s_zt] [varchar](50) NULL,
	[EFlag] [varchar](50) NULL,
	[CurTime] [datetime] NULL,
	[YDID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Yd_Pf] PRIMARY KEY CLUSTERED 
(
	[AutoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Yd_Fj]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Yd_Fj](
	[AutoId] [int] IDENTITY(1,1) NOT NULL,
	[id] [varchar](50) NULL,
	[f_dm] [varchar](50) NULL,
	[f_jb] [varchar](50) NULL,
	[f_sl] [smallint] NULL,
	[r_sl] [smallint] NULL,
	[x_sl] [smallint] NULL,
	[f_dj] [money] NULL,
	[s_cz] [varchar](50) NULL,
	[EFlag] [varchar](50) NULL,
	[z_dj] [money] NULL,
	[YDID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Yd_Fj] PRIMARY KEY CLUSTERED 
(
	[AutoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Yd_Dd]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Yd_Dd](
	[id] [varchar](50) NULL,
	[a_id] [varchar](50) NULL,
	[mc] [varchar](50) NULL,
	[jc] [varchar](50) NULL,
	[lx] [varchar](50) NULL,
	[gj] [varchar](50) NULL,
	[dr] [datetime] NULL,
	[lr] [datetime] NULL,
	[rs] [smallint] NULL,
	[d_mc] [varchar](50) NULL,
	[d_dh] [varchar](50) NULL,
	[d_dw] [varchar](50) NULL,
	[d_fs] [varchar](50) NULL,
	[d_dj] [money] NULL,
	[d_bz] [varchar](50) NULL,
	[d_rq] [datetime] NULL,
	[s_cz] [varchar](50) NULL,
	[s_zt] [varchar](50) NULL,
	[s_pf] [tinyint] NULL,
	[EFlag] [varchar](50) NULL,
	[MemberCardNo] [varchar](50) NULL,
	[Company] [varchar](50) NULL,
	[Tqr] [varchar](50) NULL,
	[Zkl] [money] NULL,
	[AutoId] [int] IDENTITY(1,1) NOT NULL,
	[YDID] [uniqueidentifier] NOT NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_Yd_Dd] PRIMARY KEY CLUSTERED 
(
	[AutoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WH_Supplier]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WH_Supplier](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [varchar](150) NULL,
	[SupplierSimple] [varchar](150) NULL,
	[Address] [varchar](250) NULL,
	[PostCode] [varchar](10) NULL,
	[Tel] [varchar](20) NULL,
 CONSTRAINT [PK_WH_Supplier] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WH_Position]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WH_Position](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[LocCode] [varchar](50) NULL,
	[LocName] [varchar](50) NULL,
 CONSTRAINT [PK_WH_Position] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WH_InOrderDetails]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WH_InOrderDetails](
	[AutoID] [int] NOT NULL,
	[ProductName] [varchar](50) NULL,
	[Counts] [int] NULL,
	[InPrice] [decimal](18, 0) NULL,
	[TotalPrice] [decimal](18, 0) NULL,
	[Used] [varchar](250) NULL,
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_InOrderDetails] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WH_InOrder]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WH_InOrder](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNum] [varchar](50) NULL,
	[Supplier] [varchar](250) NULL,
	[Date] [datetime] NULL,
	[Position] [varchar](50) NULL,
	[Guid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WH_InOrder] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WH_GoodType]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WH_GoodType](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](50) NULL,
 CONSTRAINT [PK_WH_GoodType] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WH_Goods]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WH_Goods](
	[GoodsNo] [int] IDENTITY(1,1) NOT NULL,
	[GoodsName] [varchar](50) NULL,
	[GoodsSimple] [varchar](20) NULL,
	[MakeUnit] [varchar](16) NULL,
	[GoodsStyle] [varchar](20) NULL,
	[GoodsType] [varchar](20) NULL,
	[CountMethod] [varchar](20) NULL,
	[Unit] [varchar](8) NULL,
	[UnitNum] [int] NULL,
	[LargeUnit] [varchar](8) NULL,
	[PreserveDate] [smallint] NULL,
	[GoodsClass] [varchar](10) NULL,
	[GoodsSource] [nvarchar](16) NULL,
	[StorePrice] [money] NULL,
	[InPrice] [money] NULL,
	[OutPrice] [money] NULL,
	[SalePrice] [money] NULL,
	[Remark] [varchar](40) NULL,
	[Quantity] [int] NULL,
	[Price] [money] NULL,
	[Money] [money] NULL,
	[PreQuantity] [int] NULL,
	[PreMoney] [money] NULL,
	[TotalInQuantity] [int] NULL,
	[TotalInMoney] [money] NULL,
	[TotalOutQuantity] [int] NULL,
	[TotalOutMoney] [money] NULL,
	[BuyTimes] [smallint] NULL,
	[BuyQuantity] [int] NULL,
	[BuyMoney] [money] NULL,
	[BuyBackQuantity] [int] NULL,
	[BuyBackMoney] [money] NULL,
	[SaleTimes] [smallint] NULL,
	[SaleQuantity] [int] NULL,
	[SaleMoney] [money] NULL,
	[SaleBackQuantity] [int] NULL,
	[SaleBackMoney] [money] NULL,
	[TotalProfit] [money] NULL,
	[BuyCloseMoney] [money] NULL,
	[SaleCloseMoney] [money] NULL,
	[StandPrice] [money] NULL,
	[SalePrice1] [money] NULL,
	[SalePrice2] [money] NULL,
	[SalePrice3] [money] NULL,
	[SalePrice4] [money] NULL,
	[LimitInPrice] [money] NULL,
	[IsStoreManage] [bit] NULL,
	[IsScore] [bit] NULL,
 CONSTRAINT [PK_WH_Goods] PRIMARY KEY CLUSTERED 
(
	[GoodsNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StorePart]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StorePart](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreLocId] [tinyint] NULL,
	[StoreLoc] [varchar](12) NULL,
	[GoodsNo] [int] NOT NULL,
	[Quantity] [int] NULL,
	[Price] [money] NULL,
	[Money] [money] NULL,
	[PreQuantity] [int] NULL,
	[PreMoney] [money] NULL,
	[TotalInQuantity] [int] NULL,
	[TotalInMoney] [money] NULL,
	[TotalOutQuantity] [int] NULL,
	[TotalOutMoney] [money] NULL,
	[HighLevel] [int] NULL,
	[LowLevel] [int] NULL,
	[ProofNotOut] [int] NULL,
	[ProofNotIn] [int] NULL,
	[CheckInNum] [int] NULL,
	[CheckInMoney] [money] NULL,
	[CheckOutNum] [int] NULL,
	[CheckOutMoney] [money] NULL,
	[SalePrice] [money] NULL,
	[SaleMoney] [money] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StoreChkSl]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StoreChkSl](
	[StoreChkNo] [int] NOT NULL,
	[StoreChkIndex] [int] NULL,
	[GoodsNo] [varchar](14) NOT NULL,
	[PreQuantity] [int] NULL,
	[ChkQuantity] [int] NULL,
	[ChgQuantity] [int] NULL,
	[Price] [money] NULL,
	[Money] [money] NULL,
	[Reserve] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StoreChkMa]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StoreChkMa](
	[StoreChkNo] [int] IDENTITY(1,1) NOT NULL,
	[OwnProofNo] [varchar](10) NULL,
	[Date] [smalldatetime] NULL,
	[StoreLoc] [tinyint] NULL,
	[TotalMoney] [money] NULL,
	[Staff] [varchar](10) NULL,
	[CheckStaff] [varchar](10) NULL,
	[DepartMent] [varchar](12) NULL,
	[Remark] [varchar](40) NULL,
	[CheckFlag] [varchar](1) NULL,
	[CheckMan] [varchar](10) NULL,
	[DelFlag] [int] NULL,
	[InputTime] [datetime] NULL,
 CONSTRAINT [PK_StoreChkMa] PRIMARY KEY NONCLUSTERED 
(
	[StoreChkNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StorageList]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StorageList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HappenDate] [smalldatetime] NULL,
	[ListType] [nvarchar](10) NULL,
	[ListNo] [int] NULL,
	[StockCode] [nvarchar](24) NULL,
	[WareCode] [nvarchar](24) NULL,
	[Docket] [nvarchar](60) NULL,
	[ComeNumb] [int] NULL,
	[ComePrice] [money] NULL,
	[ComeSum] [money] NULL,
	[OutNumb] [int] NULL,
	[OutPrice] [money] NULL,
	[OutSum] [money] NULL,
	[LeaveNumb] [int] NULL,
	[LeavePrice] [money] NULL,
	[LeaveSum] [money] NULL,
	[Pxh] [int] NULL,
	[SaleMoney] [money] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Setting_BlockedMan]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Setting_BlockedMan](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[IDCardNo] [varchar](50) NULL,
	[Remark] [varchar](250) NULL,
 CONSTRAINT [PK_Setting_BlockedMan] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Register_Suike]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Register_Suike](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Register_Suike] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Register_Room]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Register_Room](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Register_Room] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Register_Order]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Register_Order](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNum] [varchar](50) NOT NULL,
	[CustomOrderNum] [varchar](50) NULL,
	[Deposit] [decimal](18, 0) NULL,
	[OrderGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Register_Order] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RBAC_User]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RBAC_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nchar](50) NULL,
	[Password] [nchar](50) NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_RBAC_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RBAC_Role]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RBAC_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nchar](50) NULL,
 CONSTRAINT [PK_RBAC_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RBAC_Rights]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RBAC_Rights](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ModuleID] [int] NULL,
	[ActionID] [int] NULL,
	[RoleID] [int] NULL,
	[Enable] [int] NULL,
 CONSTRAINT [PK_RBAC_Rights] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RBAC_Module]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RBAC_Module](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleTitle] [nchar](50) NULL,
	[Ext1] [nchar](50) NULL,
 CONSTRAINT [PK_RBAC_Module] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RBAC_Action]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RBAC_Action](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleID] [int] NULL,
	[ActionTitle] [nchar](50) NULL,
 CONSTRAINT [PK_RBAC_Action] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PoliceInterface]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PoliceInterface](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[k_id] [varchar](20) NOT NULL,
	[f_fh] [varchar](8) NOT NULL,
	[Type] [varchar](1) NOT NULL,
	[IsSend] [bit] NOT NULL,
	[SendTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberZdFj]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MemberZdFj](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CardTypeId] [int] NULL,
	[FjJb] [varchar](20) NULL,
	[hours] [int] NULL,
	[zdfj] [money] NULL,
	[Status] [tinyint] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberScoreSetup]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MemberScoreSetup](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CardType] [varchar](20) NULL,
	[LevelScore] [int] NULL,
	[Discount] [money] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberDiscountOption]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MemberDiscountOption](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CardTypeId] [int] NULL,
	[f_jb] [varchar](50) NULL,
	[f_dj] [money] NULL,
	[z_dj] [money] NULL,
	[Memo] [varchar](50) NULL,
	[ZdFj] [money] NULL,
	[Lcf] [money] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberCharge]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MemberCharge](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CardNo] [varchar](20) NULL,
	[CurTime] [datetime] NULL,
	[s_gzr] [datetime] NULL,
	[ChargeMoney] [money] NULL,
	[ActualCharge] [money] NULL,
	[FkFs] [varchar](10) NULL,
	[s_cz] [varchar](20) NULL,
	[Memo] [varchar](200) NULL,
 CONSTRAINT [PK_MemberCharge] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberCard]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MemberCard](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CardType] [varchar](20) NULL,
	[ScorePercent] [float] NULL,
	[RequestScore] [float] NULL,
	[Discount] [float] NULL,
	[CheckOutDelay] [int] NULL,
	[IsCharge] [bit] NULL,
	[ChargePercent] [money] NULL,
	[YePrompt] [money] NULL,
	[DiscountType] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Member]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[MemberNo] [varchar](15) NULL,
	[MemberName] [varchar](100) NULL,
	[MemberCardNo] [varchar](20) NULL,
	[CardType] [varchar](20) NULL,
	[IdCard] [varchar](20) NULL,
	[Sex] [varchar](2) NULL,
	[BirthDay] [varchar](50) NULL,
	[HomeTelphone] [varchar](15) NULL,
	[Mobile] [varchar](15) NULL,
	[Address] [varchar](200) NULL,
	[IsValidate] [tinyint] NULL,
	[Validate] [varchar](50) NULL,
	[Password] [varchar](10) NULL,
	[Status] [tinyint] NULL,
	[RegTime] [datetime] NULL,
	[Charge] [money] NULL,
	[RestCharge] [money] NULL,
	[Score] [int] NULL,
	[Times] [int] NULL,
	[ScorePercent] [float] NULL,
	[UseScore] [int] NULL,
	[RestScore] [int] NULL,
	[Discount] [float] NULL,
	[UserName] [varchar](10) NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IdCard]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IdCard](
	[BM] [int] NULL,
	[DQ] [varchar](50) NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_IdCard] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[H_RuzhuSuike]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[H_RuzhuSuike](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[XingMing] [varchar](50) NULL,
	[XingBie] [varchar](50) NULL,
	[ShenfenZheng] [varchar](50) NULL,
	[DiZhi] [varchar](150) NULL,
	[OrderGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_H_RuzhuSuike] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[H_RuzhuOrder]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[H_RuzhuOrder](
	[AutoID] [varchar](12) NOT NULL,
	[ChangTu] [varchar](1) NULL,
	[ShiHua] [varchar](1) NULL,
	[ChangBao] [varchar](1) NULL,
	[ZhongDian] [varchar](1) NULL,
	[DaodianTime] [varchar](50) NULL,
	[LidianTime] [varchar](50) NULL,
	[JiaoxingFuwu] [datetime] NULL,
	[FukuanFangshi] [varchar](50) NULL,
	[YaJin] [decimal](18, 0) NULL,
	[XieyiDanwei] [varchar](150) NULL,
	[TeQuanRen] [varchar](50) NULL,
	[ZheKouLv] [float] NULL,
	[ShijiFangjia] [decimal](18, 0) NULL,
	[BeiZhu] [varchar](500) NULL,
	[BaoMi] [varchar](1) NULL,
	[ShougongDanhao] [varchar](50) NULL,
	[KerenLeibie] [varchar](50) NULL,
	[TuanDui] [varchar](50) NULL,
	[ZhengjianLeibie] [varchar](50) NULL,
	[DianHua] [varchar](50) NULL,
	[FangjianLeixing] [varchar](50) NULL,
	[XingBie] [varchar](50) NULL,
	[HuiYuanKa] [varchar](50) NULL,
	[JiFen] [int] NULL,
	[XingMing] [varchar](50) NULL,
	[ZhengJianHao] [varchar](50) NULL,
	[DiZhi] [varchar](50) NULL,
	[BiaozhunFangjia] [varchar](50) NULL,
	[OrderGuid] [uniqueidentifier] NOT NULL,
	[IsMain] [int] NULL,
	[MainGuid] [uniqueidentifier] NULL,
	[FangHao] [varchar](50) NULL,
	[Status] [smallint] NULL,
 CONSTRAINT [PK_RuzhuOrder] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[H_RuzhuDetail]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[H_RuzhuDetail](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [varchar](12) NULL,
	[FangJianHao] [varchar](50) NULL,
	[XingMing] [varchar](50) NULL,
	[XingBie] [varchar](50) NULL,
	[ZhengjianLeixing] [varchar](50) NULL,
	[ZhengjianHaoma] [varchar](50) NULL,
	[ZhengjianDizhi] [varchar](50) NULL,
	[YuanFangJia] [decimal](18, 0) NULL,
	[ZheKouLv] [float] NULL,
	[ShijiFangjia] [decimal](18, 0) NULL,
	[OrderGuid] [uniqueidentifier] NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_H_RuzhuDetail] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[AutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[ZhengjianLeixing] [varchar](50) NULL,
	[ZhengjianHaoma] [varchar](50) NULL,
	[DianHua] [varchar](50) NULL,
	[XingBie] [varchar](50) NULL,
	[DiZhi] [varchar](150) NULL,
	[KeRenLeiXing] [varchar](50) NULL,
	[TuanDui] [varchar](50) NULL,
	[HuiYuanKa] [varchar](50) NULL,
	[JiFen] [varchar](50) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Cash_RunningDetails]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cash_RunningDetails](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[OrderGuid] [uniqueidentifier] NOT NULL,
	[CustomerName] [varchar](50) NULL,
	[RoomNo] [varchar](50) NULL,
	[KM] [varchar](50) NULL,
	[Price] [decimal](18, 0) NULL,
	[Deposit] [decimal](18, 0) NULL,
	[Remark] [varchar](500) NULL,
	[RunningNum] [varchar](50) NULL,
	[RunningNumAuto] [varchar](50) NULL,
	[RunningTime] [datetime] NULL,
	[Payment] [varchar](50) NULL,
	[Operator] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_RunningDetails] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'押金' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cash_RunningDetails', @level2type=N'COLUMN',@level2name=N'Deposit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cash_RunningDetails', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cash_RunningDetails', @level2type=N'COLUMN',@level2name=N'RunningNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据号码（自动）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cash_RunningDetails', @level2type=N'COLUMN',@level2name=N'RunningNumAuto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cash_RunningDetails', @level2type=N'COLUMN',@level2name=N'RunningTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cash_RunningDetails', @level2type=N'COLUMN',@level2name=N'Payment'
GO
/****** Object:  Table [dbo].[AgreeCompanyPrice]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AgreeCompanyPrice](
	[id] [int] NULL,
	[ComId] [int] NULL,
	[FjJb] [varchar](20) NULL,
	[FjDj] [money] NULL,
	[ReturnFee] [money] NULL,
	[Memo] [varchar](50) NULL,
	[ZdFj] [money] NULL,
	[BtFj] [money] NULL,
	[Lcf] [money] NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_AgreeCompanyPrice] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AgreeCompany]    Script Date: 10/31/2013 17:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AgreeCompany](
	[id] [int] NULL,
	[Company] [varchar](50) NULL,
	[ContactMan] [varchar](10) NULL,
	[Telphone] [varchar](20) NULL,
	[IsCredit] [bit] NULL,
	[CreditMoney] [money] NULL,
	[RecvMoney] [money] NULL,
	[SaleMan] [varchar](10) NULL,
	[Address] [varchar](50) NULL,
	[CreditLevel] [money] NULL,
	[Status] [tinyint] NULL,
	[Memo] [varchar](200) NULL,
	[IsRetComm] [bit] NULL,
	[RetType] [tinyint] NULL,
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_AgreeCompany] PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_zd_zdfj_zdfj]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[zd_zdfj] ADD  CONSTRAINT [DF_zd_zdfj_zdfj]  DEFAULT ((0)) FOR [zdfj]
GO
/****** Object:  Default [DF_zd_tq_TjfZk]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[zd_tq] ADD  CONSTRAINT [DF_zd_tq_TjfZk]  DEFAULT ((0)) FOR [TjfZk]
GO
/****** Object:  Default [DF_zd_tq_TqMm]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[zd_tq] ADD  CONSTRAINT [DF_zd_tq_TqMm]  DEFAULT ('') FOR [TqMm]
GO
/****** Object:  Default [DF_zd_tq_TimeLimite]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[zd_tq] ADD  CONSTRAINT [DF_zd_tq_TimeLimite]  DEFAULT ((1)) FOR [TimeLimite]
GO
/****** Object:  Default [DF_zd_tq_default_1]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[zd_tq] ADD  CONSTRAINT [DF_zd_tq_default_1]  DEFAULT ((0)) FOR [default]
GO
/****** Object:  Default [DF_zd_tq_Validate_1]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[zd_tq] ADD  CONSTRAINT [DF_zd_tq_Validate_1]  DEFAULT ((2)) FOR [Validate]
GO
/****** Object:  Default [DF_zd_jb_zdfj]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[zd_jb] ADD  CONSTRAINT [DF_zd_jb_zdfj]  DEFAULT ((0)) FOR [zdfj]
GO
/****** Object:  Default [DF_zd_jb_btj]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[zd_jb] ADD  CONSTRAINT [DF_zd_jb_btj]  DEFAULT ((0)) FOR [btj]
GO
/****** Object:  Default [DF_zd_jb_byf]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[zd_jb] ADD  CONSTRAINT [DF_zd_jb_byf]  DEFAULT ((0)) FOR [lcf]
GO
/****** Object:  Default [DF_Zd_Fj_f_dj]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Zd_Fj] ADD  CONSTRAINT [DF_Zd_Fj_f_dj]  DEFAULT ((0)) FOR [f_dj]
GO
/****** Object:  Default [DF_Zd_Fj_f_jcf]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Zd_Fj] ADD  CONSTRAINT [DF_Zd_Fj_f_jcf]  DEFAULT ((0)) FOR [f_jcf]
GO
/****** Object:  Default [DF_Zd_Fj_f_cjf]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Zd_Fj] ADD  CONSTRAINT [DF_Zd_Fj_f_cjf]  DEFAULT ((0)) FOR [f_cjf]
GO
/****** Object:  Default [DF_Zd_Fj_ChgRoomZK]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Zd_Fj] ADD  CONSTRAINT [DF_Zd_Fj_ChgRoomZK]  DEFAULT ((1)) FOR [ChgRoomZK]
GO
/****** Object:  Default [DF_Zd_Fj_Tjf]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Zd_Fj] ADD  CONSTRAINT [DF_Zd_Fj_Tjf]  DEFAULT ((0)) FOR [Tjf]
GO
/****** Object:  Default [DF_Zd_Fj_SkZk]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Zd_Fj] ADD  CONSTRAINT [DF_Zd_Fj_SkZk]  DEFAULT ((0)) FOR [SkZk]
GO
/****** Object:  Default [DF__Yd_Pf__CurTime__5B78929E]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Yd_Pf] ADD  CONSTRAINT [DF__Yd_Pf__CurTime__5B78929E]  DEFAULT (getdate()) FOR [CurTime]
GO
/****** Object:  Default [DF_Yd_Dd_Status]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Yd_Dd] ADD  CONSTRAINT [DF_Yd_Dd_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF_StoreChkMa_InputTime]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StoreChkMa] ADD  CONSTRAINT [DF_StoreChkMa_InputTime]  DEFAULT (getdate()) FOR [InputTime]
GO
/****** Object:  Default [DF_StorageList_ComeNumb]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StorageList] ADD  CONSTRAINT [DF_StorageList_ComeNumb]  DEFAULT ((0)) FOR [ComeNumb]
GO
/****** Object:  Default [DF_StorageList_ComePrice]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StorageList] ADD  CONSTRAINT [DF_StorageList_ComePrice]  DEFAULT ((0)) FOR [ComePrice]
GO
/****** Object:  Default [DF_StorageList_ComeSum]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StorageList] ADD  CONSTRAINT [DF_StorageList_ComeSum]  DEFAULT ((0)) FOR [ComeSum]
GO
/****** Object:  Default [DF_StorageList_OutNumb]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StorageList] ADD  CONSTRAINT [DF_StorageList_OutNumb]  DEFAULT ((0)) FOR [OutNumb]
GO
/****** Object:  Default [DF_StorageList_OutPrice]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StorageList] ADD  CONSTRAINT [DF_StorageList_OutPrice]  DEFAULT ((0)) FOR [OutPrice]
GO
/****** Object:  Default [DF_StorageList_OutSum]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StorageList] ADD  CONSTRAINT [DF_StorageList_OutSum]  DEFAULT ((0)) FOR [OutSum]
GO
/****** Object:  Default [DF_StorageList_LeaveNumb]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StorageList] ADD  CONSTRAINT [DF_StorageList_LeaveNumb]  DEFAULT ((0)) FOR [LeaveNumb]
GO
/****** Object:  Default [DF_StorageList_LeavePrice]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StorageList] ADD  CONSTRAINT [DF_StorageList_LeavePrice]  DEFAULT ((0)) FOR [LeavePrice]
GO
/****** Object:  Default [DF_StorageList_LeaveSum]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StorageList] ADD  CONSTRAINT [DF_StorageList_LeaveSum]  DEFAULT ((0)) FOR [LeaveSum]
GO
/****** Object:  Default [DF_StorageList_Pxh]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[StorageList] ADD  CONSTRAINT [DF_StorageList_Pxh]  DEFAULT ((0)) FOR [Pxh]
GO
/****** Object:  Default [DF_PoliceInterface_SendTime]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[PoliceInterface] ADD  CONSTRAINT [DF_PoliceInterface_SendTime]  DEFAULT (getdate()) FOR [SendTime]
GO
/****** Object:  Default [DF_MemberCharge_ActualCharge]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[MemberCharge] ADD  CONSTRAINT [DF_MemberCharge_ActualCharge]  DEFAULT ((0)) FOR [ActualCharge]
GO
/****** Object:  Default [DF_MemberCard_CheckOutDelay]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[MemberCard] ADD  CONSTRAINT [DF_MemberCard_CheckOutDelay]  DEFAULT ((0)) FOR [CheckOutDelay]
GO
/****** Object:  Default [DF_MemberCard_IsCharge]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[MemberCard] ADD  CONSTRAINT [DF_MemberCard_IsCharge]  DEFAULT ((0)) FOR [IsCharge]
GO
/****** Object:  Default [DF_MemberCard_ChargePercent]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[MemberCard] ADD  CONSTRAINT [DF_MemberCard_ChargePercent]  DEFAULT ((0)) FOR [ChargePercent]
GO
/****** Object:  Default [DF_MemberCard_YePrompt]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[MemberCard] ADD  CONSTRAINT [DF_MemberCard_YePrompt]  DEFAULT ((0)) FOR [YePrompt]
GO
/****** Object:  Default [DF_Member_Status]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_Member_RegTime]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_RegTime]  DEFAULT (getdate()) FOR [RegTime]
GO
/****** Object:  Default [DF_Member_Charge]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_Charge]  DEFAULT ((0)) FOR [Charge]
GO
/****** Object:  Default [DF_Member_RestCharge]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_RestCharge]  DEFAULT ((0)) FOR [RestCharge]
GO
/****** Object:  Default [DF_Member_Score]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_Score]  DEFAULT ((0)) FOR [Score]
GO
/****** Object:  Default [DF_Member_Times]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_Times]  DEFAULT ((0)) FOR [Times]
GO
/****** Object:  Default [DF_Member_ScorePercent]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_ScorePercent]  DEFAULT ((1)) FOR [ScorePercent]
GO
/****** Object:  Default [DF_Member_UseScore]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_UseScore]  DEFAULT ((0)) FOR [UseScore]
GO
/****** Object:  Default [DF_Member_RestScore]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_RestScore]  DEFAULT ((0)) FOR [RestScore]
GO
/****** Object:  Default [DF_Member_Discount]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_Discount]  DEFAULT ((1)) FOR [Discount]
GO
/****** Object:  Default [DF_Table_1_IsChangTu]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[H_RuzhuOrder] ADD  CONSTRAINT [DF_Table_1_IsChangTu]  DEFAULT ((0)) FOR [ChangTu]
GO
/****** Object:  Default [DF_RuzhuOrder_ShiHua]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[H_RuzhuOrder] ADD  CONSTRAINT [DF_RuzhuOrder_ShiHua]  DEFAULT ((0)) FOR [ShiHua]
GO
/****** Object:  Default [DF_RuzhuOrder_ChangBao]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[H_RuzhuOrder] ADD  CONSTRAINT [DF_RuzhuOrder_ChangBao]  DEFAULT ((0)) FOR [ChangBao]
GO
/****** Object:  Default [DF_RuzhuOrder_ZhongDian]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[H_RuzhuOrder] ADD  CONSTRAINT [DF_RuzhuOrder_ZhongDian]  DEFAULT ((0)) FOR [ZhongDian]
GO
/****** Object:  Default [DF_RuzhuOrder_JiaoxingFuwu]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[H_RuzhuOrder] ADD  CONSTRAINT [DF_RuzhuOrder_JiaoxingFuwu]  DEFAULT (((1900)-(1))-(1)) FOR [JiaoxingFuwu]
GO
/****** Object:  Default [DF_RuzhuOrder_BaoMi]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[H_RuzhuOrder] ADD  CONSTRAINT [DF_RuzhuOrder_BaoMi]  DEFAULT ((0)) FOR [BaoMi]
GO
/****** Object:  Default [DF_H_RuzhuOrder_Status]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[H_RuzhuOrder] ADD  CONSTRAINT [DF_H_RuzhuOrder_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF_H_RuzhuDetail_Status]    Script Date: 10/31/2013 17:34:58 ******/
ALTER TABLE [dbo].[H_RuzhuDetail] ADD  CONSTRAINT [DF_H_RuzhuDetail_Status]  DEFAULT ((0)) FOR [Status]
GO
