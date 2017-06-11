select 'Inclusao da analise de credito'
USE [Bcredi]
GO

declare @IdAnaliseCredito uniqueidentifier ,
        @StatusOperador tinyint,
        @IdUsuario  uniqueidentifier,
		@IdUsuarioInclusao uniqueidentifier,
		@email varchar(200)

set		   @email = 'marcelo.labbati@bcredi.com.br'

set     @IdAnaliseCredito  = NEWID()
set     @StatusOperador    = 1
set     @IdUsuario         = (select guidUsuario from USUARIO where email=@email)      
set 	@IdUsuarioInclusao = (select guidUsuario from USUARIO where email=@email)


INSERT INTO [dbo].[PL_ANALISE_CREDITO]
           ([IdAnaliseCredito]
           ,[StatusOperador]
           ,[IdUsuario]
           ,[IsAtivo]
           ,[IdUsuarioInclusao]           
           ,[DataInclusao]
           )
     VALUES
           (
			   @IdAnaliseCredito,
			   @StatusOperador,
			   @IdUsuario,
			   1,
			   @IdUsuarioInclusao,
			   getDate()
		   )
           
GO


