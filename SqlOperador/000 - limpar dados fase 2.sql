use Bcredi

declare @email varchar(200)
set		   @email = 'marcelo.labbati@bcredi.com.br'

delete from PL_TERMO_ACEITE where IdUsuarioInclusao= (select guidUsuario from USUARIO where email=@email) and iditemgenerico ='8238a442-2cb2-49de-8b88-9601786749ff'
DELETE FROM [PL_TERMO_ACEITE] WHERE idUsuario = (select guidUsuario from USUARIO where email=@email)
DELETE FROM PL_ANALISE_CREDITO WHERE idUsuario = (select guidUsuario from USUARIO where email=@email)
DELETE FROM PL_AGENDAMENTO_AVALIACAO_IMOVEL WHERE idUsuario = (select guidUsuario from USUARIO where email=@email)
DELETE FROM PL_AVALIACAO_IMOVEL WHERE idUsuario = (select guidUsuario from USUARIO where email=@email)
DELETE FROM PL_ANALISE_JURIDICA WHERE idUsuario = (select guidUsuario from USUARIO where email=@email)
DELETE  from [dbo].[AL_DOCUMENTO] where idusuario =  (select guidUsuario from USUARIO where email=@email)
DELETE  from [dbo].[PL_EMISSAO_CONTRATO] where idusuario =  (select guidUsuario from USUARIO where email=@email)
DELETE  from [dbo].PL_CARTORIO where idusuario =  (select guidUsuario from USUARIO where email=@email)




