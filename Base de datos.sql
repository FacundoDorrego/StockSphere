USE [master]
GO
/****** Object:  Database [StockSphere]    Script Date: 9/12/2024 01:35:03 ******/
CREATE DATABASE [StockSphere]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StockSphere', FILENAME = N'E:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\StockSphere.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StockSphere_log', FILENAME = N'E:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\StockSphere_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [StockSphere] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StockSphere].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StockSphere] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StockSphere] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StockSphere] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StockSphere] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StockSphere] SET ARITHABORT OFF 
GO
ALTER DATABASE [StockSphere] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StockSphere] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StockSphere] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StockSphere] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StockSphere] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StockSphere] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StockSphere] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StockSphere] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StockSphere] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StockSphere] SET  ENABLE_BROKER 
GO
ALTER DATABASE [StockSphere] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StockSphere] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StockSphere] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StockSphere] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StockSphere] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StockSphere] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StockSphere] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StockSphere] SET RECOVERY FULL 
GO
ALTER DATABASE [StockSphere] SET  MULTI_USER 
GO
ALTER DATABASE [StockSphere] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StockSphere] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StockSphere] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StockSphere] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StockSphere] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StockSphere] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'StockSphere', N'ON'
GO
ALTER DATABASE [StockSphere] SET QUERY_STORE = ON
GO
ALTER DATABASE [StockSphere] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [StockSphere]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[CategoriaID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](255) NULL,
	[EmpresaID] [int] NOT NULL,
 CONSTRAINT [PK__Categori__F353C1C590B878C0] PRIMARY KEY CLUSTERED 
(
	[CategoriaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[EmpleadoID] [int] IDENTITY(1,1) NOT NULL,
	[EmpresaID] [int] NOT NULL,
	[UsuarioID] [int] NOT NULL,
 CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED 
(
	[EmpleadoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresas]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresas](
	[EmpresaID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[UsuarioID] [int] NOT NULL,
	[Activa] [bit] NOT NULL,
 CONSTRAINT [PK__Empresas__7B9F21366D39C316] PRIMARY KEY CLUSTERED 
(
	[EmpresaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovimientosInventario]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovimientosInventario](
	[MovimientoID] [int] IDENTITY(1,1) NOT NULL,
	[ProductoID] [int] NOT NULL,
	[TipoMovimiento] [nvarchar](50) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[FechaMovimiento] [datetime] NULL,
	[Observaciones] [nvarchar](255) NULL,
	[UsuarioID] [int] NOT NULL,
	[EmpresaID] [int] NOT NULL,
 CONSTRAINT [PK__Movimien__BF923FCCA7B73D80] PRIMARY KEY CLUSTERED 
(
	[MovimientoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[ProductoID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[ProveedorID] [int] NOT NULL,
	[Marca] [nvarchar](255) NOT NULL,
	[Descripcion] [nvarchar](255) NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Stock] [int] NOT NULL,
	[CategoriaID] [int] NOT NULL,
	[FechaIngreso] [datetime] NULL,
	[Activo] [bit] NULL,
	[EmpresaID] [int] NOT NULL,
 CONSTRAINT [PK__Producto__A430AE83FA346011] PRIMARY KEY CLUSTERED 
(
	[ProductoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[ProveedorID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Direccion] [nvarchar](255) NULL,
	[EmpresaID] [int] NULL,
 CONSTRAINT [PK__Proveedo__61266BB9C01EA2D0] PRIMARY KEY CLUSTERED 
(
	[ProveedorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RolID] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[CorreoElectronico] [nvarchar](255) NOT NULL,
	[Clave] [nvarchar](255) NOT NULL,
	[RolID] [int] NOT NULL,
	[NombreUsuario] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Usuarios__2B3DE798E3B963CA] PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ventas]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ventas](
	[VentasID] [int] IDENTITY(1,1) NOT NULL,
	[EmpresaID] [int] NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[ProductoID] [int] NOT NULL,
	[CategoriaID] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Monto] [decimal](10, 2) NOT NULL,
	[FechaVenta] [datetime] NOT NULL,
 CONSTRAINT [PK_Ventas] PRIMARY KEY CLUSTERED 
(
	[VentasID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MovimientosInventario] ADD  CONSTRAINT [DF__Movimient__Fecha__3D5E1FD2]  DEFAULT (getdate()) FOR [FechaMovimiento]
GO
ALTER TABLE [dbo].[Productos] ADD  CONSTRAINT [DF__Productos__Fecha__37A5467C]  DEFAULT (getdate()) FOR [FechaIngreso]
GO
ALTER TABLE [dbo].[Productos] ADD  CONSTRAINT [DF__Productos__Activ__38996AB5]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Ventas] ADD  CONSTRAINT [DF_Ventas_FechaVenta]  DEFAULT (getdate()) FOR [FechaVenta]
GO
ALTER TABLE [dbo].[Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Categorias_Empresas] FOREIGN KEY([EmpresaID])
REFERENCES [dbo].[Empresas] ([EmpresaID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Categorias] CHECK CONSTRAINT [FK_Categorias_Empresas]
GO
ALTER TABLE [dbo].[Empleados]  WITH CHECK ADD  CONSTRAINT [FK_Empleados_Empresas] FOREIGN KEY([EmpresaID])
REFERENCES [dbo].[Empresas] ([EmpresaID])
GO
ALTER TABLE [dbo].[Empleados] CHECK CONSTRAINT [FK_Empleados_Empresas]
GO
ALTER TABLE [dbo].[Empleados]  WITH CHECK ADD  CONSTRAINT [FK_Empleados_Usuarios] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Empleados] CHECK CONSTRAINT [FK_Empleados_Usuarios]
GO
ALTER TABLE [dbo].[Empresas]  WITH CHECK ADD  CONSTRAINT [FK_Empresas_Usuarios] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Empresas] CHECK CONSTRAINT [FK_Empresas_Usuarios]
GO
ALTER TABLE [dbo].[MovimientosInventario]  WITH CHECK ADD  CONSTRAINT [FK_MovimientosInventario_Empresas] FOREIGN KEY([EmpresaID])
REFERENCES [dbo].[Empresas] ([EmpresaID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MovimientosInventario] CHECK CONSTRAINT [FK_MovimientosInventario_Empresas]
GO
ALTER TABLE [dbo].[MovimientosInventario]  WITH CHECK ADD  CONSTRAINT [FK_MovimientosInventario_Usuarios] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[MovimientosInventario] CHECK CONSTRAINT [FK_MovimientosInventario_Usuarios]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Categorias] FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categorias] ([CategoriaID])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Categorias]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Empresas] FOREIGN KEY([EmpresaID])
REFERENCES [dbo].[Empresas] ([EmpresaID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Empresas]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Proveedores] FOREIGN KEY([ProveedorID])
REFERENCES [dbo].[Proveedores] ([ProveedorID])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Proveedores]
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD  CONSTRAINT [FK_Proveedores_Empresas] FOREIGN KEY([EmpresaID])
REFERENCES [dbo].[Empresas] ([EmpresaID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Proveedores] CHECK CONSTRAINT [FK_Proveedores_Empresas]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([RolID])
REFERENCES [dbo].[Roles] ([RolID])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
ALTER TABLE [dbo].[Ventas]  WITH CHECK ADD  CONSTRAINT [FK_Ventas_Empresas] FOREIGN KEY([EmpresaID])
REFERENCES [dbo].[Empresas] ([EmpresaID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ventas] CHECK CONSTRAINT [FK_Ventas_Empresas]
GO
ALTER TABLE [dbo].[Ventas]  WITH CHECK ADD  CONSTRAINT [FK_Ventas_Usuarios] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Ventas] CHECK CONSTRAINT [FK_Ventas_Usuarios]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCategoria]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarCategoria]
   @CategoriaID INT,
   @Nombre NVARCHAR(100),
   @Descripcion NVARCHAR(255) = NULL
AS
BEGIN
   SET NOCOUNT ON;

   UPDATE Categorias
   SET Nombre = @Nombre,
       Descripcion = @Descripcion
   WHERE CategoriaID = @CategoriaID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ActualizarEmpresa]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEmpresa]
    @EmpresaID INT,  
    @Nombre NVARCHAR(100) 
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Empresas
    SET Nombre = @Nombre
    WHERE EmpresaID = @EmpresaID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ActualizarProducto]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarProducto]
   @ProductoID INT,
   @Nombre NVARCHAR(100),
   @Descripcion NVARCHAR(255),
   @Precio DECIMAL(10, 2),
   @Stock INT,
   @CategoriaID INT = NULL,
   @ProveedorID INT = NULL,
   @Marca NVARCHAR(255)
AS
BEGIN
   SET NOCOUNT ON;

   UPDATE Productos
   SET Nombre = @Nombre,
       Descripcion = @Descripcion,
       Precio = @Precio,
       Stock = @Stock,
       CategoriaID = @CategoriaID,
	   ProveedorID = @ProveedorID,
	   Marca = @Marca
   WHERE ProductoID = @ProductoID;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarProveedor]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarProveedor]
   @ProveedorID INT,
   @Nombre NVARCHAR(100),
   @Telefono NVARCHAR(20) = NULL,
   @Email NVARCHAR(100) = NULL,
   @Direccion NVARCHAR(255) = NULL,
   @EmpresaId int = NULL
AS
BEGIN
   SET NOCOUNT ON;

   UPDATE Proveedores
   SET Nombre = @Nombre,
       Telefono = @Telefono,
       Email = @Email,
       Direccion = @Direccion,
	   EmpresaID = @EmpresaId
   WHERE ProveedorID = @ProveedorID;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarStock]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ActualizarStock] 
	@productoID int = null,
	@cantidad int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   update Productos set Stock = @cantidad where ProductoID = @productoID
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarCategoria]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarCategoria]
   @Nombre NVARCHAR(100),
   @Descripcion NVARCHAR(255) = NULL,
   @EmpresaID int = NULL
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO Categorias (Nombre, Descripcion,EmpresaID)
   VALUES (@Nombre, @Descripcion,@EmpresaID);
END;


GO
/****** Object:  StoredProcedure [dbo].[AgregarEmpleado]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AgregarEmpleado] 
	-- Add the parameters for the stored procedure here
	@UsuarioID INT = NULL,
	@EmpresaID INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Empresas WHERE EmpresaID = @EmpresaID AND UsuarioID=@UsuarioID )
    BEGIN
        RAISERROR('El usuario ya ha sido asignado a esta empresa.', 16, 1);
        RETURN;
    END
	INSERT INTO Empleados (UsuarioID, EmpresaID)
    VALUES (@UsuarioID, @EmpresaID);
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarEmpresa]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarEmpresa]
    @UsuarioID INT,  
    @Nombre NVARCHAR(100),  
	@FechaCreacion DateTime,
	@Activa bit
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Empresas (UsuarioID, Nombre,FechaCreacion,Activa)
    VALUES (@UsuarioID, @Nombre,@FechaCreacion,@Activa);
END;

GO
/****** Object:  StoredProcedure [dbo].[AgregarProducto]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarProducto]
   @Nombre NVARCHAR(100),
   @Descripcion NVARCHAR(255),
   @Precio DECIMAL(10, 2),
   @Stock INT,
   @CategoriaID INT = NULL,
   @EmpresaID INT = NULL,
   @ProveedorID INT = NULL,
   @Marca NVARCHAR(255)
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaID,EmpresaID,ProveedorID,Marca)
   VALUES (@Nombre, @Descripcion, @Precio, @Stock, @CategoriaID,@EmpresaID,@ProveedorID,@Marca);
END;
GO
/****** Object:  StoredProcedure [dbo].[AgregarProveedor]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarProveedor]
   @Nombre NVARCHAR(100),
   @Telefono NVARCHAR(20) = NULL,
   @Email NVARCHAR(100) = NULL,
   @Direccion NVARCHAR(255) = NULL,
   @EmpresaId int = NULL
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO Proveedores (Nombre, Telefono, Email, Direccion,EmpresaID)
   VALUES (@Nombre, @Telefono, @Email, @Direccion,@EmpresaId);
END;
GO
/****** Object:  StoredProcedure [dbo].[AgregarVenta]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AgregarVenta]
	@EmpresaID int = null,
	@ProductoID int = null,
	@CategoriaID int = null,
	@UsuarioID int = null,
	@FechaVenta datetime = null,
	@Monto decimal = null,
	@Cantidad int = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO Ventas(EmpresaID,ProductoID,UsuarioID,CategoriaID,FechaVenta,Monto,Cantidad) VALUES 
	(@EmpresaID,@ProductoID,@UsuarioID,@CategoriaID,@FechaVenta,@Monto,@Cantidad)
END
GO
/****** Object:  StoredProcedure [dbo].[CrearUsuario]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrearUsuario]
@Clave nvarchar(255)=NULL,
@Correo nvarchar(255)=NULL,
@RolID INT = NULL,
@NombreUsuario nvarchar(255)=NULL
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO Usuarios (CorreoElectronico,Clave,RolID,NombreUsuario) VALUES (@Correo,@Clave,@RolID,@NombreUsuario)
END;

GO
/****** Object:  StoredProcedure [dbo].[EliminarCategoria]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarCategoria]
   @CategoriaID INT
AS
BEGIN
   SET NOCOUNT ON;

   DELETE FROM Categorias
   WHERE CategoriaID = @CategoriaID;
END;

GO
/****** Object:  StoredProcedure [dbo].[EliminarEmpleado]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarEmpleado]
   @EmpleadoID INT = NULL
AS
BEGIN
   SET NOCOUNT ON;

   DELETE FROM Empleados
   WHERE EmpleadoID = @EmpleadoID;
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarEmpresa]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarEmpresa]
    @EmpresaID INT  
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Empresas Set Activa = 0
    WHERE EmpresaID = @EmpresaID;
END;

GO
/****** Object:  StoredProcedure [dbo].[EliminarProducto]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarProducto]
   @ProductoID INT
AS
BEGIN
   SET NOCOUNT ON;

   DELETE FROM Productos
   WHERE ProductoID = @ProductoID;
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarProveedor]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarProveedor]
   @ProveedorID INT
AS
BEGIN
   SET NOCOUNT ON;

   DELETE FROM Proveedores
   WHERE ProveedorID = @ProveedorID;
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarUsuario]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarUsuario]
   @UsuarioID INT
AS
BEGIN
   SET NOCOUNT ON;

   DELETE FROM Usuarios
   WHERE UsuarioID = @UsuarioID;
END;
GO
/****** Object:  StoredProcedure [dbo].[LoguearUsuario]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[LoguearUsuario]
@Clave nvarchar(255)=NULL,
@Correo nvarchar(255)=NULL
AS
BEGIN
   SET NOCOUNT ON;

   SELECT * FROM Usuarios where @Correo=CorreoElectronico and @Clave = Clave
END;

GO
/****** Object:  StoredProcedure [dbo].[ModificarUsuario]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModificarUsuario]
   @NombreUsuario NVARCHAR(255),
   @Correo NVARCHAR(255),
   @Clave NVARCHAR(255),
   @RolID INT = NULL,
   @UsuarioID INT = NULL
AS
BEGIN
   SET NOCOUNT ON;

   UPDATE Usuarios
   SET NombreUsuario = @NombreUsuario,
       CorreoElectronico = @Correo,
       Clave = @Clave,
	   RolID = @RolID
   WHERE UsuarioID = @UsuarioID;
END;
GO
/****** Object:  StoredProcedure [dbo].[MovimientoInventarioSP]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MovimientoInventarioSP]
    @ProductoID INT,  
    @Cantidad INT,
	@Tipomovimiento varchar(200),
	@FechaMov DATETIME,
	@Obs varchar(200) = NULL,
	@UsuarioID INT = NULL,
	@EmpresaID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO MovimientosInventario (Cantidad,FechaMovimiento,TipoMovimiento,Observaciones,ProductoID,UsuarioID,EmpresaId)
	VALUES (@Cantidad,@FechaMov,@Tipomovimiento,@Obs,@ProductoID,@UsuarioID,@EmpresaID)
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerCategorias]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerCategorias]
AS
BEGIN
   SET NOCOUNT ON;

   SELECT * FROM Categorias;
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerCategoriasxID]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ObtenerCategoriasxID]
@CategoriaID int = null
AS
BEGIN
   SET NOCOUNT ON;

   SELECT * FROM Categorias where CategoriaID=@CategoriaID
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerEmpleadosxEmpresa]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ObtenerEmpleadosxEmpresa]
@EmpresaID INT = NULL
AS
BEGIN
   SET NOCOUNT ON;

   SELECT * FROM Empleados WHERE EmpresaID=@EmpresaID
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerEmpleadoxID]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerEmpleadoxID] 
	-- Add the parameters for the stored procedure here
	@EmpleadoID INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   SELECT * FROM Empleados WHERE @EmpleadoID = EmpleadoID
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerEmpleadoxIDUsu]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[ObtenerEmpleadoxIDUsu] 
	-- Add the parameters for the stored procedure here
	@UsuarioID INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   SELECT * FROM Empleados WHERE @UsuarioID = UsuarioID
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerEmpresasPorUsuario]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerEmpresasPorUsuario]
    @UsuarioID INT  -- ID del usuario para obtener sus empresas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Empresas
    WHERE UsuarioID = @UsuarioID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerEmpresaxID]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ObtenerEmpresaxID]
    @EmpresaID INT  
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Empresas
    WHERE EmpresaID = @EmpresaID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerMovimientos]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerMovimientos]
 AS
BEGIN
	select * from MovimientosInventario
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerProductos]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerProductos]
AS
BEGIN
   SET NOCOUNT ON;

   SELECT * FROM Productos;
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerProductoxID]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerProductoxID] 
	-- Add the parameters for the stored procedure here
	@ProductoID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Productos where @ProductoID = ProductoID
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerProveedores]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerProveedores]
AS
BEGIN
   SET NOCOUNT ON;

   SELECT * FROM Proveedores;
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerProveedoresxEmpresa]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerProveedoresxEmpresa]
@empresaID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT * FROM Proveedores WHERE @empresaID=EmpresaID

END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerProveedoresxID]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ObtenerProveedoresxID]
@ProveedorID int
AS
BEGIN
   SET NOCOUNT ON;

   SELECT * FROM Proveedores where ProveedorID=@ProveedorID
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerStock]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerStock]
	-- Add the parameters for the stored procedure here
	@ProductoID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Select Stock from Productos where @ProductoID=ProductoID

END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerUltimoIdProducto]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerUltimoIdProducto]
AS
BEGIN
    
    SELECT TOP 1 ProductoID
    FROM Productos
    ORDER BY ProductoID DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarios]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerUsuarios] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Usuarios
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarioxID]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ObtenerUsuarioxID]
@UsuarioID int
AS
BEGIN
   SET NOCOUNT ON;

   SELECT * FROM Usuarios where UsuarioID=@UsuarioID
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerVentasxEmpresa]    Script Date: 9/12/2024 01:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ObtenerVentasxEmpresa]
@EmpresaID int = NULL
AS
BEGIN
   SET NOCOUNT ON;

   SELECT * FROM Ventas where @EmpresaID=EmpresaID
END;
GO
USE [master]
GO
ALTER DATABASE [StockSphere] SET  READ_WRITE 
GO
