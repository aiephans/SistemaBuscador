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
SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_check_usuario]    Script Date: 04-09-2021 10:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_check_usuario] @nombreUsuario varchar(50),@password varchar(50)
as
select count(*) from usuarios 
where nombreUsuario=@nombreUsuario and CONVERT(VARCHAR(50),DECRYPTBYPASSPHRASE('clavesistema123!!',password))=@password
GO
USE [master]
GO
ALTER DATABASE [cib4023600db] SET  READ_WRITE 
GO
