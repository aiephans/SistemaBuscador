USE [cib4023600db]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 07-09-2021 21:56:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombres] [varchar](50) NOT NULL,
	[apellidos] [varchar](50) NOT NULL,
	[nombreUsuario] [varchar](50) NOT NULL,
	[rolId] [int] NOT NULL,
	[password] [varbinary](255) NOT NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[usuarios] ON 
GO
INSERT [dbo].[usuarios] ([id], [nombres], [apellidos], [nombreUsuario], [rolId], [password]) VALUES (4, N'Hans', N'López', N'hlopez', 1, 0x0200000087F3DCF6C3DF64E6361DC67948EFB5D0B71AF389D1591F44DD40C4C79E18410C5C9853ED260F7B429CFC2ECECA009934)
GO
INSERT [dbo].[usuarios] ([id], [nombres], [apellidos], [nombreUsuario], [rolId], [password]) VALUES (1004, N'Juan', N'Perez', N'jperez', 3, 0x02000000D4CDCD3E05CD8369A93D8C832045FC846054B52D7D7D3593CA2061CCA7107060CA6D461D34685D8BFC1C519C990017E2)
GO
INSERT [dbo].[usuarios] ([id], [nombres], [apellidos], [nombreUsuario], [rolId], [password]) VALUES (1005, N'Francisco', N'Nuñez', N'fnunez', 3, 0x02000000222644D2C5DC2A458DC6F1588F582ADB23B3EDFD00A38D2C0586662761F3CB4B7E883937A1D0F42F36FEE45F4DD99217)
GO
INSERT [dbo].[usuarios] ([id], [nombres], [apellidos], [nombreUsuario], [rolId], [password]) VALUES (1006, N'Juan', N'Perez', N'jperezp', 3, 0x02000000C51C710EDCA7B4FEEEFA390D4DE090BD2FBE710925A913AA33742BA4B52E90A9B119C1C67B321DF3507311AF5F832CFA)
GO
INSERT [dbo].[usuarios] ([id], [nombres], [apellidos], [nombreUsuario], [rolId], [password]) VALUES (1007, N'Alejandra', N'Vasquez', N'avasquez', 2, 0x02000000DFB9732C48D19D9F6FD05C18882D9EDBC7B131E6A728CB8C18D8502EFA785F5D83EF5E914D42FF8D2A6A873A56DFFA1B)
GO
SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_actualiza_usuario]    Script Date: 07-09-2021 21:56:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_actualiza_usuario]
@id int,
@nombres varchar(50),
@apellidos varchar(50),
@rolId int,
@password varchar(50)
as
update usuarios
set 
nombres = @nombres,
apellidos = @apellidos,
rolId = @rolId,
password = ENCRYPTBYPASSPHRASE('clavesistema123!!',@password)
where id = @id
GO
/****** Object:  StoredProcedure [dbo].[sp_check_nombre_usuario]    Script Date: 07-09-2021 21:56:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_check_nombre_usuario] @nombreUsuario varchar(50)
as
select count(*) from usuarios 
where nombreUsuario=@nombreUsuario
GO
/****** Object:  StoredProcedure [dbo].[sp_check_usuario]    Script Date: 07-09-2021 21:56:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_check_usuario] @nombreUsuario varchar(50),@password varchar(50)
as
select count(*) from usuarios 
where nombreUsuario=@nombreUsuario and CONVERT(VARCHAR(50),DECRYPTBYPASSPHRASE('clavesistema123!!',password))=@password
GO
/****** Object:  StoredProcedure [dbo].[sp_insertar_usuario]    Script Date: 07-09-2021 21:56:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_insertar_usuario]
@nombres VARCHAR(50),
@apellidos VARCHAR(50),
@nombreUsuario	VARCHAR(50),
@rolId INT,
@password VARCHAR(50)
AS
INSERT INTO usuarios (nombres,apellidos,nombreUsuario,rolId,password)
VALUES
(
@nombres,
@apellidos,
@nombreUsuario,
@rolId,
ENCRYPTBYPASSPHRASE('clavesistema123!!',@password)
)
GO
/****** Object:  StoredProcedure [dbo].[sp_obtener_usuario_por_id]    Script Date: 07-09-2021 21:56:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_obtener_usuario_por_id] @id int
as
select 
id,
nombres,
apellidos,
nombreUsuario,
rolId,
convert(varchar(50), DECRYPTBYPASSPHRASE('clavesistema123!!',password) ) as password
from usuarios
where id=@id
GO
/****** Object:  StoredProcedure [dbo].[sp_obtener_usuarios]    Script Date: 07-09-2021 21:56:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_obtener_usuarios]
AS
SELECT id,nombres,apellidos,nombreUsuario,rolId 
FROM usuarios
GO
