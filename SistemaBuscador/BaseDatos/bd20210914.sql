USE [cib4023600db]
GO
/****** Object:  Table [dbo].[roles]    Script Date: 14-09-2021 20:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 14-09-2021 20:47:04 ******/
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
	[password] [varchar](100) NOT NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[roles] ON 
GO
INSERT [dbo].[roles] ([id], [nombre]) VALUES (1, N'Supervisor ventas')
GO
INSERT [dbo].[roles] ([id], [nombre]) VALUES (2, N'administrador')
GO
INSERT [dbo].[roles] ([id], [nombre]) VALUES (3, N'Vendedor')
GO
SET IDENTITY_INSERT [dbo].[roles] OFF
GO
SET IDENTITY_INSERT [dbo].[usuarios] ON 
GO
INSERT [dbo].[usuarios] ([id], [nombres], [apellidos], [nombreUsuario], [rolId], [password]) VALUES (1, N'administrador', N'Administrador', N'admin', 1, N'e0dec1dd4418cc0c23baf81812d8b70135ea4510c49159fc7f')
GO
INSERT [dbo].[usuarios] ([id], [nombres], [apellidos], [nombreUsuario], [rolId], [password]) VALUES (3, N'Hans', N'Lopez', N'hlopez', 1, N'f671654d03f6c7c4723d86d9cc46e0647a9a3a70a3ba44b978')
GO
INSERT [dbo].[usuarios] ([id], [nombres], [apellidos], [nombreUsuario], [rolId], [password]) VALUES (4, N'Juan Enrique', N'Perez Mella', N'jperez', 3, N'f671654d03f6c7c4723d86d9cc46e0647a9a3a70a3ba44b978')
GO
INSERT [dbo].[usuarios] ([id], [nombres], [apellidos], [nombreUsuario], [rolId], [password]) VALUES (5, N'Francisco', N'Nuñez', N'fnunez', 3, N'73edd33c4a945b0f863957f74ffeea0cd8c1e7c553a555df1c')
GO
SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_actualiza_password]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_actualiza_password] @password varchar(50) , @id int
as
UPDATE usuarios set password = @password
where id = @id
GO
/****** Object:  StoredProcedure [dbo].[sp_actualiza_usuario]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_actualiza_usuario]
@id int,
@nombres varchar(50),
@apellidos varchar(50),
@rolId int
as
update usuarios
set 
nombres = @nombres,
apellidos = @apellidos,
rolId = @rolId
where id = @id
GO
/****** Object:  StoredProcedure [dbo].[sp_acutalizar_rol]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_acutalizar_rol]
@id int,
@nombre varchar(50)
as
update roles set nombre = @nombre 
where id=@id
GO
/****** Object:  StoredProcedure [dbo].[sp_buscar_rol_por_id]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_buscar_rol_por_id]
@id int
as
select id,nombre from roles
where id = @id
GO
/****** Object:  StoredProcedure [dbo].[sp_check_nombre_usuario]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_check_nombre_usuario] @nombreUsuario varchar(50)
as
select count(*) from usuarios 
where nombreUsuario=@nombreUsuario
GO
/****** Object:  StoredProcedure [dbo].[sp_check_usuario]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_check_usuario] @nombreUsuario varchar(50),@password varchar(50)
as
select count(*) from usuarios 
where nombreUsuario=@nombreUsuario and password=@password
GO
/****** Object:  StoredProcedure [dbo].[sp_elimiar_rol]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_elimiar_rol]
@id int
as
delete roles 
where id = @id
GO
/****** Object:  StoredProcedure [dbo].[sp_eliminar_usuario]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_eliminar_usuario] @id int
as
delete usuarios
where id=@id
GO
/****** Object:  StoredProcedure [dbo].[sp_insertar_rol]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_insertar_rol]
@nombre varchar(50)
as
insert into roles(nombre)
values (@nombre)
GO
/****** Object:  StoredProcedure [dbo].[sp_insertar_usuario]    Script Date: 14-09-2021 20:47:04 ******/
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
@password
)
GO
/****** Object:  StoredProcedure [dbo].[sp_listar_roles]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_listar_roles]
as
select id,nombre from roles
GO
/****** Object:  StoredProcedure [dbo].[sp_obtener_usuario_por_id]    Script Date: 14-09-2021 20:47:04 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_obtener_usuarios]    Script Date: 14-09-2021 20:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_obtener_usuarios]
AS
SELECT id,nombres,apellidos,nombreUsuario,rolId 
FROM usuarios
GO
