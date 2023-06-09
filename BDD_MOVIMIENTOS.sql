
USE [master]
GO
CREATE DATABASE [BDD_MOVIMIENTOS]
GO


GO
USE [BDD_MOVIMIENTOS];
GO

CREATE TABLE [BM_PERSONA] (
	ID_PERSONA bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
	NOMBRE varchar(150) NOT NULL,
	GENERO char(1) NOT NULL,/* M - F*/
	EDAD int NOT NULL,
	IDENTIFICACION varchar(15) NOT NULL,
	DIRECCION varchar(250),
	TELEFONO int

)
GO
CREATE TABLE [BM_CLIENTE] (
	ID_CLIENTE bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ID_PERSONA bigint NOT NULL,
	CONTRASENIA varchar(50) NOT NULL,
	ESTADO bit NOT NULL DEFAULT '1'

)
GO
CREATE TABLE [BM_CUENTA] (
	ID_CUENTA bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ID_PERSONA bigint NOT NULL,
	NUMERO_CUENTA int NOT NULL,
	TIPO_CUENTA varchar(3) NOT NULL,/* AHO - COR*/
	SALDO_INICIAL decimal(10,2) NOT NULL DEFAULT '0',
	ESTADO bit NOT NULL DEFAULT '1'

)
GO
CREATE TABLE [BM_MOVIMIENTOS] (
	ID_MOVIMIENTOS bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ID_CUENTA bigint NOT NULL,
	FECHA datetime NOT NULL DEFAULT GETDATE(),
	TIPO nvarchar(3) NOT NULL,/*DEP - RET*/
	VALOR decimal(10,2) NOT NULL,
	SALDO decimal(10,2) NOT NULL

)
GO

ALTER TABLE [BM_CLIENTE] WITH CHECK ADD CONSTRAINT [FK_BM_CLIENTE_BM_PERSONA] FOREIGN KEY ([ID_PERSONA]) REFERENCES [BM_PERSONA]([ID_PERSONA])
ON UPDATE CASCADE
GO
ALTER TABLE [BM_CLIENTE] CHECK CONSTRAINT [FK_BM_CLIENTE_BM_PERSONA]
GO

ALTER TABLE [BM_CUENTA] WITH CHECK ADD CONSTRAINT [FK_BM_CUENTA_BM_PERSONA] FOREIGN KEY ([ID_PERSONA]) REFERENCES [BM_PERSONA]([ID_PERSONA])
ON UPDATE CASCADE
GO
ALTER TABLE [BM_CUENTA] CHECK CONSTRAINT [FK_BM_CUENTA_BM_PERSONA]
GO

ALTER TABLE [BM_MOVIMIENTOS] WITH CHECK ADD CONSTRAINT [FK_BM_MOVIMIENTOS_BM_CUENTA] FOREIGN KEY ([ID_CUENTA]) REFERENCES [BM_CUENTA]([ID_CUENTA])
ON UPDATE CASCADE
GO
ALTER TABLE [BM_MOVIMIENTOS] CHECK CONSTRAINT [FK_BM_MOVIMIENTOS_BM_CUENTA]
GO

