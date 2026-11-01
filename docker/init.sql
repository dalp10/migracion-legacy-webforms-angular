-- Esquema y datos de demostración para el caso de estudio (módulo de Pedidos).
-- Solo para desarrollo local. En un proyecto real usa migraciones de EF Core.
IF DB_ID('ModuloPedidos') IS NULL
    CREATE DATABASE ModuloPedidos;
GO

USE ModuloPedidos;
GO

IF OBJECT_ID('dbo.Clientes') IS NULL
CREATE TABLE dbo.Clientes (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario  NVARCHAR(MAX) NOT NULL,
    FechaRegistro  DATETIME2 NOT NULL
);

IF OBJECT_ID('dbo.Pedidos') IS NULL
CREATE TABLE dbo.Pedidos (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId      INT NOT NULL,
    Total          DECIMAL(18,2) NOT NULL,
    Estado         NVARCHAR(MAX) NOT NULL,
    FechaCreacion  DATETIME2 NOT NULL
);

IF OBJECT_ID('dbo.Facturas') IS NULL
CREATE TABLE dbo.Facturas (
    Id         INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId  INT NOT NULL,
    Vencida    BIT NOT NULL
);

IF OBJECT_ID('dbo.Usuarios') IS NULL
CREATE TABLE dbo.Usuarios (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario  NVARCHAR(MAX) NOT NULL,
    PasswordHash   NVARCHAR(MAX) NOT NULL,
    Rol            NVARCHAR(MAX) NOT NULL
);

IF NOT EXISTS (SELECT 1 FROM dbo.Clientes)
BEGIN
    -- Cliente 1: 6 años de antigüedad (15 % de descuento), sin facturas vencidas.
    -- Cliente 2: 1 año de antigüedad (sin descuento), sin facturas vencidas.
    -- Cliente 3: tiene una factura vencida, por lo que NO se pueden aprobar sus pedidos.
    INSERT INTO dbo.Clientes (NombreUsuario, FechaRegistro) VALUES
        (N'cliente.antiguo',  DATEADD(YEAR, -6, SYSUTCDATETIME())),
        (N'cliente.nuevo',    DATEADD(YEAR, -1, SYSUTCDATETIME())),
        (N'cliente.moroso',   DATEADD(YEAR, -3, SYSUTCDATETIME()));

    INSERT INTO dbo.Pedidos (ClienteId, Total, Estado, FechaCreacion) VALUES
        (1, 1200.00, N'Pendiente', SYSUTCDATETIME()),
        (1,  350.50, N'Pendiente', SYSUTCDATETIME()),
        (2,   89.90, N'Pendiente', SYSUTCDATETIME()),
        (3,  940.00, N'Pendiente', SYSUTCDATETIME());

    INSERT INTO dbo.Facturas (ClienteId, Vencida) VALUES
        (1, 0), (2, 0), (3, 1);
END
GO
