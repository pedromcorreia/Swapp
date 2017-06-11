declare @email varchar(200)
set		   @email = 'marcelo.labbati@bcredi.com.br'

DELETE FROM [PL_TERMO_ACEITE] WHERE idUsuario = (select guidUsuario from USUARIO where email=@email)
DELETE FROM PL_ANALISE_CREDITO WHERE idUsuario = (select guidUsuario from USUARIO where email=@email)


