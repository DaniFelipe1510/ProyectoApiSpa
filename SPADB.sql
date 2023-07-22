USE [SPADB]
GO

/****** Object:  Table [dbo].[Atiende]    Script Date: 7/21/2023 4:50:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Atiende](
	[ID_Empleado] [bigint] NOT NULL,
	[ID_Cliente] [bigint] NOT NULL,
	[ID_Reserva] [bigint] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Atiende]  WITH CHECK ADD  CONSTRAINT [FK_Atiende_Cliente] FOREIGN KEY([ID_Cliente])
REFERENCES [dbo].[Cliente] ([IDCliente])
GO

ALTER TABLE [dbo].[Atiende] CHECK CONSTRAINT [FK_Atiende_Cliente]
GO

ALTER TABLE [dbo].[Atiende]  WITH CHECK ADD  CONSTRAINT [FK_Atiende_Empleado] FOREIGN KEY([ID_Empleado])
REFERENCES [dbo].[Empleado] ([IDEmpleado])
GO

ALTER TABLE [dbo].[Atiende] CHECK CONSTRAINT [FK_Atiende_Empleado]
GO

ALTER TABLE [dbo].[Atiende]  WITH CHECK ADD  CONSTRAINT [FK_Atiende_Reserva] FOREIGN KEY([ID_Reserva])
REFERENCES [dbo].[Reserva] ([IDReserva])
GO

ALTER TABLE [dbo].[Atiende] CHECK CONSTRAINT [FK_Atiende_Reserva]
GO


