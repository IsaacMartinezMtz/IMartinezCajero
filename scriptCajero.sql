USE [master]
GO
/****** Object:  Database [Cajero]    Script Date: 24/01/2024 06:15:29 p. m. ******/
CREATE DATABASE [Cajero]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Cajero', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Cajero.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Cajero_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Cajero_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Cajero] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cajero].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cajero] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cajero] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cajero] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cajero] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cajero] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cajero] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cajero] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Cajero] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cajero] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cajero] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cajero] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cajero] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cajero] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cajero] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cajero] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cajero] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Cajero] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cajero] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cajero] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cajero] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cajero] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cajero] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cajero] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cajero] SET RECOVERY FULL 
GO
ALTER DATABASE [Cajero] SET  MULTI_USER 
GO
ALTER DATABASE [Cajero] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cajero] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cajero] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cajero] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Cajero', N'ON'
GO
USE [Cajero]
GO
/****** Object:  StoredProcedure [dbo].[GetALLCajero]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetALLCajero]
AS
SELECT IdBillete, Denominacion, CantidadBilletes FROM CajeroSaldo
GO
/****** Object:  StoredProcedure [dbo].[GetCuenta]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCuenta] 
@NoCuenta INT
AS
SELECT NoCuenta, Nip, Nombre,ApellidoPaterno FROM Usuario 
WHERE NoCuenta = @NoCuenta
GO
/****** Object:  StoredProcedure [dbo].[GetCuentaTranferir]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCuentaTranferir]
@NumeroCuentaReceptora INT
AS
SELECT NumeroCuenta, SaldoActual FROM CuentaSaldo WHERE NumeroCuenta = @NumeroCuentaReceptora
GO
/****** Object:  StoredProcedure [dbo].[GetSaldo]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSaldo]
@NumeroCuenta INT 
AS
SELECT IdCuentaSaldo, NumeroCuenta, SaldoActual, Usuario.Nombre, Usuario.ApellidoPaterno FROM CuentaSaldo
INNER JOIN Usuario ON NumeroCuenta = NoCuenta
WHERE  NumeroCuenta = @NumeroCuenta

GO
/****** Object:  StoredProcedure [dbo].[SaldoTotalCajero]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaldoTotalCajero]
AS
SELECT SUM(Denominacion * CantidadBilletes) AS total
FROM CajeroSaldo;
GO
/****** Object:  StoredProcedure [dbo].[Transferencia]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Transferencia]
@NumeroCuentaEmisora INT,
@NumeroCuentaReceptora INT,
@SaldoEmisor INT,
@SaldoReceptor INT
AS
Update CuentaSaldo SET SaldoActual = @SaldoEmisor WHERE NumeroCuenta = @NumeroCuentaEmisora
UPDATE CuentaSaldo SET SaldoActual = @SaldoReceptor WHERE NumeroCuenta = @NumeroCuentaReceptora
GO
/****** Object:  StoredProcedure [dbo].[UpdateSaldoCajero]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSaldoCajero]
@IdBillete int,
@CantidadBilletes int
AS
UPDATE CajeroSaldo SET CantidadBilletes=@CantidadBilletes WHERE IdBillete = @IdBillete; 
GO
/****** Object:  StoredProcedure [dbo].[UpdateSaldoCliente]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSaldoCliente]
@NumeroCuenta int, 
@SaldoActual INT
AS
UPDATE CuentaSaldo SET SaldoActual = @SaldoActual WHERE NumeroCuenta = @NumeroCuenta; 
GO
/****** Object:  Table [dbo].[CajeroSaldo]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CajeroSaldo](
	[IdBillete] [int] IDENTITY(1,1) NOT NULL,
	[Denominacion] [int] NOT NULL,
	[CantidadBilletes] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBillete] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CuentaSaldo]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CuentaSaldo](
	[IdCuentaSaldo] [int] IDENTITY(1,1) NOT NULL,
	[NumeroCuenta] [int] NULL,
	[SaldoActual] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCuentaSaldo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 24/01/2024 06:15:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[NoCuenta] [int] NOT NULL,
	[Nombre] [varchar](80) NOT NULL,
	[ApellidoPaterno] [varchar](80) NOT NULL,
	[Nip] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NoCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CuentaSaldo]  WITH CHECK ADD FOREIGN KEY([NumeroCuenta])
REFERENCES [dbo].[Usuario] ([NoCuenta])
GO
USE [master]
GO
ALTER DATABASE [Cajero] SET  READ_WRITE 
GO
