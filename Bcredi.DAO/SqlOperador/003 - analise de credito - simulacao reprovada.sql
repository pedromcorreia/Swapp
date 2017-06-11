select 'Inclusao termo de aceite Aprovado'
USE [Bcredi]
GO


declare    @idTermoAceite uniqueidentifier,
           @statusTermoAceite tinyint,
           @nomeDocumento varchar(100),
           @idUsuario uniqueidentifier,
           @idItemGenerico uniqueidentifier,
           @descricaoContestacao  varchar(3000),
           @parecerContestacao  varchar(3000),
           @termoAceiteInicial  varchar(3000),
           @novoTermoAceite   varchar(3000),
           @idUsuarioInclusao uniqueidentifier,
		   @email varchar(200)

set		   @email = 'marcelo.labbati@bcredi.com.br'
set        @idTermoAceite = NEWID()
set        @statusTermoAceite = 0
set        @nomeDocumento ='Nome do documento'
set        @IdUsuario         = (select guidUsuario from USUARIO where email=@email)      
set        @idItemGenerico = ( select IdAnaliseCredito  from PL_ANALISE_CREDITO where IdUsuario=(select guidUsuario from USUARIO where email=@email))
set        @descricaoContestacao = 'descricaoContestacao'
set        @parecerContestacao   = 'parecerContestacao'
set        @termoAceiteInicial   = 'termoAceiteInicial'
set        @novoTermoAceite      = 'novoTermoAceite'
set 	   @IdUsuarioInclusao = (select guidUsuario from USUARIO where email=@email)

INSERT INTO [dbo].[PL_TERMO_ACEITE]
           ([idTermoAceite]
           ,[statusTermoAceite]
           ,[nomeDocumento]
           ,[idUsuario]
           ,[idItemGenerico]
           ,[descricaoContestacao]
           ,[parecerContestacao]
           ,[termoAceiteInicial]
           ,[novoTermoAceite]
           ,[idUsuarioInclusao]
           ,[dataInclusao]           
           ,[isAtivo])
     VALUES
           (
		   @idTermoAceite,
           @statusTermoAceite, 
           @nomeDocumento,
           @idUsuario, 
           @idItemGenerico,
           @descricaoContestacao,
           @parecerContestacao,
           @termoAceiteInicial,
           @novoTermoAceite, 
           @idUsuarioInclusao,
           getDate(),
           1)           
GO


