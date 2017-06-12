using Bcredi.DAO.Models;
using Bcredi.DAO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.Globalization;


namespace Bcredi.Web.Controllers
{
    public class UsuarioController : Controller
    {
        BaseVOService baseVOService = new BaseVOService();
        UsuarioService usuarioService = new UsuarioService();

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CriarUsuario()
        {

            Usuario usuario = new Usuario();

            return View(usuario);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult CriarUsuario(FormCollection _form)
        {

            List<String> mensagensErro = new List<String>();
            int idUsuario = 0;
            string retorno = "CriarUsuario";
            Usuario usuario = new Usuario();
            DateTime dataNascimento = new DateTime();

            DateTime dataMinima = DateTime.Parse("01/01/1900");

            string nasc = _form["txtDataNascimento"];
            if (DateTime.TryParse(nasc, out dataNascimento))
            {
                dataNascimento = DateTime.Parse(_form["txtDataNascimento"]);
            }
            StringBuilder mensagemInconsistencia = new StringBuilder();

            if (string.IsNullOrEmpty(_form["txtUsuario"]))
            {
                mensagemInconsistencia.Append("Usuário não preenchido. ");
            }
            if (string.IsNullOrEmpty(_form["txtCpf"]))
            {
                mensagemInconsistencia.Append("Cpf não preenchido. ");
            }

            if (string.IsNullOrEmpty(_form["txtTelefone"]))
            {
                mensagemInconsistencia.Append("Telefone celular não preenchido. ");
            }

            if (string.IsNullOrEmpty(_form["txtDataNascimento"]))
            {
                mensagemInconsistencia.Append("Data de nascimento não preenchida. ");
            }
            // Verifica se o cliente tem mais de 18 anos
            else if ((new DateTime(1, 1, 1) + (DateTime.Today - dataNascimento)).Year - 1 < 18)
            {
                mensagemInconsistencia.Append("Data de nascimento (Idade mínima 18 anos). ");
            }

            if (string.IsNullOrEmpty(_form["txtEmail"]))
            {
                mensagemInconsistencia.Append("E-mail não preenchido. ");
            }
            if (string.IsNullOrEmpty(_form["txtPassword"]) || (_form["txtPassword"]).Length < 6)
            {
                mensagemInconsistencia.Append("Senha não preenchida. ");
            }

            else
            {
                string txtUsuario = _form["txtUsuario"];
                string txtLogin = _form["txtEmail"];
                string txtEmail = _form["txtEmail"];
                string txtPassword = _form["txtPassword"];
                string txtDataNascimento = _form["txtDataNascimento"];
                string txtTelefone = _form["txtTelefone"];
                txtTelefone = txtTelefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                string txtCpf = _form["txtCpf"];
                txtCpf = txtCpf.Replace(".", "").Replace("-", "");

                usuario.Nome = txtUsuario;
                usuario.Login = txtLogin;
                usuario.Email = txtEmail;

                nasc = _form["txtDataNascimento"];
                if (DateTime.TryParse(nasc, out dataNascimento))
                {
                    usuario.DataAniversario = DateTime.Parse(_form["txtDataNascimento"]);
                }
                usuario.Telefone = txtTelefone;
                usuario.Cpf_cnpj = txtCpf;
                usuario.Password = txtPassword;

                if ((mensagemInconsistencia.Length == 0))
                {
                    
                    try
                    {


                        string newPasswordEncrypted = Bcredi.Utils.Utils.Encryption(txtPassword);
                        usuario.Password = newPasswordEncrypted;
                        idUsuario = usuarioService.saveOrUpdate(usuario);

                        ViewBag.TxtLogin = usuario.Login;
                        ViewBag.TxtPassword = txtPassword;
                        ViewBag.TokenResetSenha = usuario.TokenResetSenha;
                        TempData["TempDataLogin"] = usuario.Login;
                        TempData["TempDataPassword"] = txtPassword;
                        TempData["TempDataResetSenha"] = usuario.TokenResetSenha;

                        ViewBag.MensagemUsuarioCriado = "Usuário criado com sucesso!";
                        retorno = "CadastroRealizado";


                        //return RedirectToAction("CadastroRealizado", "Usuario");
                    }
                    catch (Exception ex)
                    {
                        mensagensErro.Add("Não foi possível criar o usuário, tente novamente. " + ex);

                    }

                    return View(retorno);
                }
                else
                {
                    ViewBag.mensagemInconsistencia = mensagemInconsistencia;
                    return View(usuario);

                }
            }
            return View(usuario);

        }


        [AllowAnonymous]
        [HttpPost]
        public JsonResult ReenviarLink()
        {
            try
            {
                string TxtLogin = TempData["TempDataLogin"].ToString();
                string Password = TempData["TempDataPassword"].ToString();
                string TokenResetSenha = TempData["TempDataResetSenha"].ToString();

                Bcredi.Utils.Utils.EnviarEmail("ti.financeira@bariguifinanceira.com.br", TxtLogin, "Criar usuário - Bcredi", getBodyEmailCriarUsuario(Password, TokenResetSenha));

                return Json(new { Success = true, Message = "Sucesso", Data = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = "Erro ao reenviar link {" + ex + "}", Data = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult CadastroConfirmado()
        {
            return View();
        }

        private string getBodyEmailCriarUsuario(string senhaGerada, string token)
        {

            string bodyEmail = @"

                    
<!DOCTYPE html>
<html>
<head>
<meta name='robots' content='noindex'>
<meta name='googlebot' content='noindex'>
  <title>Criação de usuário {{Criação de usuário}}</title>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
<meta name='viewport' content='width=device-width, initial-scale=1'>
<meta http-equiv='X-UA-Compatible' content='IE=edge' />
<style type='text/css'>
	@import url('https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800');
	body {	
		font-family:'Open Sans';
		color:#333333; 
		font-size: 18px; 
		line-height: 145%; 
		font-weight: 300;
		text-align:left;
	}
	
	.conteudo {
		padding: 0 25px 15px 25px;
		text-align:left;
	}
	
	.classe-botao {
		margin: 30px 0 40px 0;
		
	}
	.classe-botao a {
		background: #FCDE02; 
		color:#304A7B; 
		font-size: 18px; 
		font-weight: 700; 
		font-family:'Open Sans'; 
		width: 350px; height:50px; 
		text-decoration: none; 
		display: block; 
		line-height:50px;
	}
	.imagem-bg {height: 250px;}
	a {
		color:#3F63A5; 
		font-weight: 600; 
		font-family:'Open Sans';
		text-decoration: underline;
	}

	@media screen and (max-width: 600px) {
	  .responsive-table {
		display: block;
		width: 100% !important;
	  }

	  .responsive-image {
		height: auto;
		max-width: 100% !important;
	  }
</style>
</head>
<body style='background: #F6F6F6; padding: 25px 0;'>
<center>
<div style='background: #fff; border:2px solid #E1ECF2; width: 80%; overflow: hidden; display: block;'>
	<div style='padding: 25px 30px; overflow: hidden;'>
		<div style='width:153px; float: left;'><img src='https://www.bcredi.com.br/img/email-apresentacao/logo.png' alt='Bcredi'></div>
		<div style='width: 130px; float: right; margin-top: 15px;'>
			<a href='https://www.facebook.com/bcredi' target='_blank'><img src='https://www.bcredi.com.br/img/email-apresentacao/ico-facebook.png' alt='Facebook' style='float:lef; margin-right: 8px;'></a>
			<a href='https://twitter.com/bcredi_' target='_blank'><img src='https://www.bcredi.com.br/img/email-apresentacao/ico-twitter.png' alt='Twitter' style='float:lef; margin-right: 8px;'></a>
			<a href='https://www.linkedin.com/company/bcredi' target='_blank'><img src='https://www.bcredi.com.br/img/email-apresentacao/ico-linkedin.png' alt='Linkedin' style='float:lef;'></a>
		</div>
	</div>

	<div class='conteudo'>
	
		<strong>Olá</strong>!
		<br><br>
		Para confirmar seu cadastro no site da Bcredi clique no botão abaixo:
		
	</div>
	
	<div class='classe-botao'>
		<a href='http://barsf00019:2000/Usuario/AtivarCadastroUsuario?token=[TOKEN]' target='_blank'>Confirmar cadastro</a>
	</div>

	<div class='conteudo'>

		Caso tenha alguma dúvida entre em contato conosco!
		<br><br>
		Obrigado :)
		<br>
		<strong>Bcredi</strong> - <a href='http://bcredi.com.br/' target='_blank'>www.bcredi.com.br</a>
		<br><br>
		

    </div>

	<div class='imagem-bg'><img src='https://www.bcredi.com.br/img/email-apresentacao/bg.jpg'></div>
</div>
</center>
</body>
</html>

                    "
                    ;



            //< a href = '/OneSystem/LoginEmail/[SENHA_GERADA]' >< br >
            //bodyEmail = bodyEmail.Replace("[NOME_USUARIO]", "");
            bodyEmail = bodyEmail.Replace("[SENHA_GERADA]", senhaGerada);
            bodyEmail = bodyEmail.Replace("[TOKEN]", token);
            //TO DO gerar guid quando gerar senha nova bodyEmail = bodyEmail.Replace("[GUID]", Guid.NewGuid().ToString());



            return bodyEmail;
        }

        private string getBodyEmail(string senhaGerada)
        {

            string bodyEmail = @"

                    
<!DOCTYPE html>
<html>
<head>
<meta name='robots' content='noindex'>
<meta name='googlebot' content='noindex'>
<title>Criar nova senha {{Criar nova senha}}</title>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
<meta name='viewport' content='width=device-width, initial-scale=1'>
<meta http-equiv='X-UA-Compatible' content='IE=edge' />
<style type='text/css'>
	@import url('https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800');
	body {	
		font-family:'Open Sans';
		color:#333333; 
		font-size: 18px; 
		line-height: 145%; 
		font-weight: 300;
		text-align:left;
	}
	
	.conteudo {
		padding: 0 25px 15px 25px;
		text-align:left;
	}
	
	.classe-botao {
		margin: 30px 0 40px 0;
		
	}
	.classe-botao a {
		background: #FCDE02; 
		color:#304A7B; 
		font-size: 18px; 
		font-weight: 700; 
		font-family:'Open Sans'; 
		width: 350px; height:50px; 
		text-decoration: none; 
		display: block; 
		line-height:50px;
	}
	.imagem-bg {height: 250px;}
	a {
		color:#3F63A5; 
		font-weight: 600; 
		font-family:'Open Sans';
		text-decoration: underline;
	}
	@media screen and (max-width: 600px) {
	  .responsive-table {
		display: block;
		width: 100% !important;
	  }

	  .responsive-image {
		height: auto;
		max-width: 100% !important;
	  }
</style>
</head>

<body style='background: #F6F6F6; padding: 25px 0;'>
<center>
<div style='background: #fff; border:2px solid #E1ECF2; width: 80%; overflow: hidden; display: block;'>
	<div style='padding: 25px 30px; overflow: hidden;'>
		<div style='width:153px; float: left;'><img src='https://www.bcredi.com.br/img/email-apresentacao/logo.png' alt='Bcredi'></div>
		<div style='width: 130px; float: right; margin-top: 15px;'>
			<a href='https://www.facebook.com/bcredi' target='_blank'><img src='https://www.bcredi.com.br/img/email-apresentacao/ico-facebook.png' alt='Facebook' style='float:lef; margin-right: 8px;'></a>
			<a href='https://twitter.com/bcredi_' target='_blank'><img src='https://www.bcredi.com.br/img/email-apresentacao/ico-twitter.png' alt='Twitter' style='float:lef; margin-right: 8px;'></a>
			<a href='https://www.linkedin.com/company/bcredi' target='_blank'><img src='https://www.bcredi.com.br/img/email-apresentacao/ico-linkedin.png' alt='Linkedin' style='float:lef;'></a>
		</div>
	</div>
    <div class='conteudo'>
		<strong>Olá</strong>!
		<br><br>
		Recebemos um pedido para alteração de senha, se você solicitou clique no botão abaixo:
	</div>		
	
	<div class='classe-botao'>
		<a href='http://barsf00019:9000/OneSystem/RedefinirSenha' target='_blank'>Criar nova senha</a>
	</div>
    
     <div class='conteudo'>
		Caso não tenha solicitado esta alteração desconsidere este email, sua senha não será alterada.
		<br><br>
		Obrigado :)
		<br>
		<strong>Bcredi</strong> - <a href='http://bcredi.com.br/' target='_blank'>www.bcredi.com.br</a>
		<br><br>
       </div>

		<div class='imagem-bg'><img src='https://www.bcredi.com.br/img/email-apresentacao/bg.jpg'></div>
</div>
</center>
</body>
</html>

                    "
                    ;



            //< a href = '/OneSystem/LoginEmail/[SENHA_GERADA]' >< br >
            //bodyEmail = bodyEmail.Replace("[NOME_USUARIO]", "");
            bodyEmail = bodyEmail.Replace("[SENHA_GERADA]", senhaGerada);
            //TO DO gerar guid quando gerar senha nova bodyEmail = bodyEmail.Replace("[GUID]", Guid.NewGuid().ToString());



            return bodyEmail;
        }

        public ActionResult Editar(string id)
        {
            Usuario usuario = new Usuario();
            if (id != null)
            {
                usuario = usuarioService.getUsuarioById(int.Parse(id));
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Editar(Usuario varUsuario)
        {
            int Id = varUsuario.Id;
            bool boolIsAtivo = varUsuario.IsAtivo;
            Usuario usuario = new Usuario();
            usuario = usuarioService.getUsuarioAtivoById(Id, boolIsAtivo);
            //if (Id != 0)
            //{
            //    usuario = usuarioService.getUsuarioAtivoById(Id, boolIsAtivo);
            //}
            return View();
        }
        public ActionResult Listar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Listar(FormCollection formulario)
        {
            string busca = formulario["txtBusca"];
            List<Usuario> lista = usuarioService.getDescriptionPartial(busca);
            return View(lista);
        }

        [AllowAnonymous]
        public ActionResult AtivarCadastroUsuario(string token)
        {
            Usuario usuario = new Usuario();

            if (token == null)
            {
                ViewBag.MensagemAviso = "Código de verificação inválido.<br/>Entre em contato com o suporte!";
                ViewBag.ReiniciarProcesso = false;
                return View();
            }

            // Recupera usuario por meio de tokenResetSenha
            usuario = usuarioService.getUsuarioByTokenResetSenha(token);

            // Se não encontrou usuario, exibe mensagem de aviso
            if (usuario == null)
            {
                ViewBag.MensagemAviso = "Código de verificação inválido.<br/>Entre em contato com o suporte!";
                ViewBag.ReiniciarProcesso = false;
                return View();
            }

            //Se encontrou usuario, verifica se está inativo
            //if (usuario != null && !usuario.IsAtivo)
            //{
            //    ViewBag.MensagemAviso = "Usuário está inativo.<br/>Entre em contato com o suporte!";
            //    ViewBag.ReiniciarProcesso = false;
            //    return View();
            //}

            // Verifica se token ainda está válido
            if (!usuarioService.isValidDataExpiracaoSenha(usuario.Login))
            {
                ViewBag.MensagemAviso = "Prazo para alteração de senha expirou.<br/>Clique no botão abaixo para reiniciar o processo.";
                ViewBag.ReiniciarProcesso = true;
                return View();
            }

            usuarioService.AtivarCadastroUsuarioService(usuario.Login);

            //return View("CadastroConfirmado");
            return RedirectToAction("CadastroConfirmado", "Usuario");
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult AlterarSenha(Usuario usuario)
        {
            var usuarioLogado = Session["usuario"] as Usuario;
            //ViewBag.usuario = usuario;
            return View(usuarioLogado);
        }

        [HttpPost]
        public ActionResult AlterarSenha(FormCollection _form)
        {
            Usuario usuario = new Usuario();
            var usuarioLogado = Session["usuario"] as Usuario;
            string txtSenhaAtual = _form["txtSenhaAtual"];
            string txtSenhaNova = _form["txtSenha"];
            string txtSenhaConfirmar = _form["txtSenhaConfirmar"];
            if (txtSenhaAtual == "" || txtSenhaNova == "" || txtSenhaConfirmar == "")
            {
                ViewBag.MensagemAlterarSenhaFail = "Preencha todos os campos";

                usuario = usuarioLogado;

                return View(usuario);
            }


            else if (txtSenhaConfirmar != txtSenhaNova)
            {
                ViewBag.MensagemAlterarSenhaFail = "Senha nova não é igual ao confirmar senha";

                usuario = usuarioLogado;

                return View(usuario);
            }
            else
            {
                if (Bcredi.Utils.Utils.Decryption(usuarioLogado.Password).Equals(txtSenhaAtual))
                {

                    if (txtSenhaAtual == txtSenhaNova)
                    {
                        ViewBag.MensagemAlterarSenhaFail = "A senha nova não pode ser igual a senha antiga";

                        usuario = usuarioLogado;

                        return View(usuario);
                    }
                    usuario.Password = Bcredi.Utils.Utils.Encryption(_form["txtSenha"]);
                    usuario.Email = usuarioLogado.Email;

                    bool response = usuarioService.AlterarSenhaUpdate(usuario);

                    if (response == true)
                    {
                        ViewBag.MensagemAlterarSenhaOK = "Sua senha foi alterada com sucesso!";

                    }
                    else
                    {
                        ViewBag.MensagemAlterarSenhaFail = "Não foi possível alterar sua senha!";
                    }
                }
                else
                {
                    usuario.Email = usuarioLogado.Email;
                    ViewBag.MensagemAlterarSenhaFail = "Senha atual incorreta!";
                }
            }
            usuario = usuarioLogado;

            return View(usuario);
        }


        [AllowAnonymous]
        [HttpPost]
        public JsonResult EmailExiste(string emailJson)
        {
            string email = emailJson;
            Usuario usuario = new Usuario();

            //bool emailExistente = dadosPessoaisService.VerificarEmailCadastrado(email);

            //if (emailExistente)
            //{
            //    return Json(new { Success = true, Data = emailExistente, Message = "Sucesso" }, JsonRequestBehavior.AllowGet);
            //}
            bool emailExistente = true;
            return Json(new { Error = true, Data = emailExistente, Message = "Nao assinado" }, JsonRequestBehavior.AllowGet);

        }
    }
}