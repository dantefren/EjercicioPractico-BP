USE [BDD_MOVIMIENTOS]
GO
INSERT INTO [dbo].[BM_PERSONA]
           ([NOMBRE]
           ,[GENERO]
           ,[EDAD]
           ,[IDENTIFICACION]
           ,[DIRECCION]
           ,[TELEFONO])
     VALUES
           ('Danilo'
           ,'M'
           ,30
           ,'1718342585'
           ,'Monjas'
           ,'26018888')
GO
INSERT INTO [dbo].[BM_CLIENTE]
           ([ID_PERSONA]
           ,[CONTRASENIA]
           ,[ESTADO])
     VALUES
           (1
           ,'123456'
           ,0)
GO
INSERT INTO [dbo].[BM_CUENTA]
           ([ID_PERSONA]
           ,[NUMERO_CUENTA]
           ,[TIPO_CUENTA]
           ,[SALDO_INICIAL]
           ,[ESTADO])
     VALUES
           (1
           ,2377116
           ,'AHO'
           ,0
           ,1)
GO
INSERT INTO [dbo].[BM_MOVIMIENTOS]
           ([ID_CUENTA]
           ,[FECHA]
           ,[TIPO]
           ,[VALOR]
           ,[SALDO])
     VALUES
           (1
		   ,GETDATE()
           ,'DEP'
           ,100
           ,100)
GO