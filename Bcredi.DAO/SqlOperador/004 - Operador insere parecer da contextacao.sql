declare @email varchar(200)
set		   @email = 'marcelo.labbati@bcredi.com.br'

--configurando resposta do operador para uma contextacao
UPDATE PL_ANALISE_CREDITO set StatusOperador=4 
WHERE idUsuario = (select guidUsuario from USUARIO where email=@email)