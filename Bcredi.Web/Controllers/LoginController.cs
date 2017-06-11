using Bcredi.DAO.Models;
using Bcredi.DAO.Service;
using Bcredi.Utils;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bcredi.Web.Controllers
{
    public class LoginController : Controller
    {
        UsuarioService usuarioService = new UsuarioService();
        LogAcessoService logAcessoService = new LogAcessoService();
        LogService logService = new DAO.Service.LogService();

        public bool isAutenticado()
        {
            return Session["usuario"] != null;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Logger.getLogger().Info("ABERTURA DE TELA DE LOGIN: " + System.DateTime.Now.ToString());

            #region [ Usuario Fake]
            Usuario usuario = new Usuario();

            string ipCliente = "";

            ipCliente = Bcredi.Utils.Utils.getUserIp(Request);

            //Marcelo
            if (ipCliente.Equals("10.25.13.181") || ipCliente.Equals("192.168.0.189"))
            {
                ViewBag.txtLogin = "marcelo.labbati@bcredi.com.br";
                ViewBag.txtPassword = "123456";
            }

            //Pedro
            if (ipCliente.Equals("10.25.13.215"))
            {
                //    ViewBag.txtLogin = "pedro.correia@bcredi.com.br";
                //    ViewBag.txtPassword = "1234";
                ViewBag.txtLogin = "bruno.duarte@bcredi.com.br";
                ViewBag.txtPassword = "123456";
            }

            //Mario
            if (ipCliente.Equals("10.25.13.211"))
            {
                ViewBag.txtLogin = "mario.souza@bcredi.com.br";
                ViewBag.txtPassword = "1234";
            }

            //Alexandre
            if (ipCliente.Equals("10.25.13.156"))
            {
                ViewBag.txtLogin = "alexandre.mussiat@bariguifinanceira.com.br";
                ViewBag.txtPassword = "1234";
            }

            //Bruno
            if (ipCliente.Equals("10.25.13.118"))
            {
                ViewBag.txtLogin = "bruno.duarte@bcredi.com.br";
                ViewBag.txtPassword = "123456";
            }

            //Michele
            if (ipCliente.Equals("10.25.13.392"))
            {
                ViewBag.txtLogin = "michele.strohscein@bcredi.com.br";
                ViewBag.txtPassword = "1234";
            }


            #endregion [ Usuario Fake]


            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginSistema(FormCollection form)
        {
            Usuario usuario = new Usuario();
            string hdnNovaUrl = form["hdnNovaUrl"];
            string login = form["txtLogin"];
            string password = form["txtPassword"];
            //TODo Pedro Alterar
            //usuario = usuarioService.getUsuarioByLoginSenha(login, password);
              usuario = usuarioService.getUsuarioByLoginSenha(login, password);

            // Se não encontrou usuario, exibe mensagem de aviso
            if (usuario == null)
            {
                ViewBag.txtLogin = login;
                ViewBag.txtPassword = password;
                ViewBag.MensagemLoginInvalido = "<strong>Atenção:</strong> Login ou senha inválidos!";
                return View("Login");
            }

            // Se encontrou usuario, verifica se está inativo
            if (usuario != null && !usuario.IsAtivo)
            {
                ViewBag.txtLogin = login;
                ViewBag.txtPassword = password;
                ViewBag.MensagemLoginInvalido = "Usuário está inativo.<br/>Entre em contato com o suporte!";
                return View("Login");
            }

            Session["usuario"] = usuario;
            Core.UsuarioAtual = usuario;

            string ipAcesso = Bcredi.Utils.Utils.getUserIp(Request);

            usuarioService.atualizarUltimoAcesso(usuario.Id, ipAcesso);

            Logger.getLogger().Info("LOGIN USUARIO " + Core.UsuarioAtual.Login.ToUpper() + " : " + System.DateTime.Now.ToString());

            // Gera novo UUID (universal unique identifier) para representar a sessão atual
            Core.SessionUUID = Guid.NewGuid().ToString().ToUpper();


            //Redirecionar para a view que foi invocada
            if (!string.IsNullOrEmpty(hdnNovaUrl) && !hdnNovaUrl.Equals("/"))
            {
                return RedirectPermanent(hdnNovaUrl);
            }
            else
            {
                    //TODO Verificar se ja existe assinatura digital


                    //return RedirectToAction("Mensagem", "Usuario", new { @login = login });
                    //return RedirectToAction("DadosComplementares", "DadosComplementares");
                    return RedirectToAction("Index", "Swap");
            }
        }

        public ActionResult LoginOKTeste()
        {
            return View();
        }

        /// <summary>
        /// Grava informações de logout no repositorio SYS_LOG_ACESSO, 
        /// grava log textual (bcredi.log), limpa dados da sessão
        /// e por fim redireciona para a tela de login
        /// </summary>
        /// <returns>Tela de login</returns>
        [ActionName("Logout")]
        public ActionResult LogoutSistema()
        {

            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.HttpContext.Current.User = null;

            return View("Login");
        }

        /// <summary>
        /// Método para enviar email
        /// </summary>
        /// <param name="emailDestinatario">Email do destinatário</param>
        /// <param name="assunto">Assunto do email</param>
        /// <param name="mensagem">mensagem do email</param>


        [AllowAnonymous]
        public ActionResult EsqueciSenha(string vLogin)
        {
            //string login = Request.QueryString["vLogin"];
            ViewBag.Title = "Redefinição de Senha";
            @ViewBag.txtLogin = vLogin;
            return View();
        }

        //[HttpPost]
        //public JsonResult EsqueciSenha(string vLogin)
        //{
        //    //ContratoService contratoService = new ContratoService();

        //    //if (!string.IsNullOrEmpty(idCarteira) && !string.IsNullOrEmpty(numeroContrato))
        //    //{
        //    //    Contrato contrato = contratoService.BuscarContratoByCarteiraENumero(idCarteira, numeroContrato);

        //    //    //Quando nao encontrar o contrato pelo numero e a carteira, criar um novo configurando estes dois atributos
        //    //    if (contrato.Id == 0)
        //    //    {
        //    //        contrato = contratoService.getNovoContrato();
        //    //        contrato.NumeroContrato = numeroContrato;
        //    //        Carteira carteira = new Carteira();
        //    //        carteira.Id = int.Parse(idCarteira);
        //    //        contrato.Carteira = carteira;
        //    //    }

        //    //    return Json(contrato, JsonRequestBehavior.AllowGet);
        //    //}

        //    //return Json("", JsonRequestBehavior.AllowGet);

        //    return Json();
        //}

        [HttpPost]
        [AllowAnonymous]
        public ActionResult EsqueciSenha(FormCollection form)
        {
            Usuario usuario = new Usuario();
            string hdnNovaUrl = form["hdnNovaUrl"];
            string login = form["txtLogin"];

            usuario = usuarioService.getUsuarioByEmail(login);

            // Se não encontrou usuario, exibe mensagem de aviso
            if (usuario == null)
            {
                ViewBag.txtLogin = login;
                ViewBag.MensagemLoginInvalido = "E-mail não encontrado.<br/>Entre em contato com o suporte!";
                return View("EsqueciSenha");
            }

            // Se encontrou usuario, verifica se está inativo
            if (usuario != null && !usuario.IsAtivo)
            {
                ViewBag.txtLogin = login;
                ViewBag.MensagemLoginInvalido = "Usuário está inativo.<br/>Entre em contato com o suporte!";
                return View("EsqueciSenha");
            }

            #region [GRAVACAO LOG]
            Log log = new Log();
            log.HashOperacao = Constantes.USUARIO_RESETAR_SENHA.ToString(); // RESETAR SENHA
            log.JsonOperacao = Bcredi.Utils.Utils.Serialize(usuario);
            log.UsuarioCriador = usuario;
            logService.create(log);
            #endregion [GRAVACAO LOG]

            //Cria a senha (cod seguranca enviado ao usuário via e-mail)
            string newpassword = Bcredi.Utils.Utils.CreatePassword();

            String tokenResetSenha = Guid.NewGuid().ToString();

            string newPasswordEncrypted = Bcredi.Utils.Utils.Encryption(newpassword);

            // Grava nova senha (cod seguranca), token e configura data de expiracao de senha
            usuarioService.iniciaProcessoUpdateSenha(usuario.Id, tokenResetSenha, newPasswordEncrypted);

            //Envia o email atraves ti.financeira@Bcredifinanceira.com.br para o e-mail necessario
            Bcredi.Utils.Utils.EnviarEmail("ti.financeira@Bcredifinanceira.com.br", login, "Redefinição de senha - Bcredi", getBodyEmail(newpassword, tokenResetSenha));

            //Email enviado com sucesso 
            return RedirectToAction("EmailEnviado", "Login", new { vlogin = login });
        }

        [AllowAnonymous]
        public ActionResult EmailEnviado(string vlogin)
        {
            ViewBag.Email = vlogin;
            return View();
        }

        [AllowAnonymous]
        public ActionResult RedefinirSenha()
        {
            Usuario usuario = new Usuario();
            
            //ViewBag.CodSeguranca = Request.QueryString["s"];
            string token = Request.QueryString["token"];


            if (token == null)
            {
                ViewBag.MensagemAviso = "Código de verificação inválido.<br/>Entre em contato com o suporte!";
                ViewBag.ReiniciarProcesso = false;
                return View();
            }

            // Recupera usuario por meio de tokenResetSenha
            usuario = usuarioService.getUsuarioByTokenResetSenha(token);
            usuario.Password = Request.QueryString["s"];

            // Se não encontrou usuario, exibe mensagem de aviso
            if (usuario == null)
            {
                ViewBag.MensagemAviso = "Código de verificação inválido.<br/>Entre em contato com o suporte!";
                ViewBag.ReiniciarProcesso = false;
                return View();
            }

            // Se encontrou usuario, verifica se está inativo
            if (usuario != null && !usuario.IsAtivo)
            {
                ViewBag.MensagemAviso = "Usuário está inativo.<br/>Entre em contato com o suporte!";
                ViewBag.ReiniciarProcesso = false;
                return View();
            }

            // Verifica se token ainda está válido
            if (!usuarioService.isValidDataExpiracaoSenha(usuario.Login))
            {
                ViewBag.MensagemAviso = "Prazo para alteração de senha expirou.<br/>Clique no botão abaixo para reiniciar o processo.";
                ViewBag.ReiniciarProcesso = true;
                return View();
            }

            return View(usuario);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RedefinirSenha(FormCollection form)
        {
            {
                Usuario usuario = new Usuario();
                //string hdnNovaUrl = form["hdnNovaUrl"];
                string login = form["hdnLogin"];
                //string txtCodSeguranca = form["txtCodSeguranca"];
                string txtCodSeguranca = form["hdnCodSeguranca"];
                //string txtCodSeguranca = ViewBag.CodSeguranca;
                string newpassword = form["txtNovaSenha"];

                // Verifica se código de segurança informado corresponse à senha temporária
                usuario = usuarioService.getUsuarioByLoginSenha(login, txtCodSeguranca);

                // Se não encontrou usuario, exibe mensagem de aviso
                if (usuario == null)
                {
                    ViewBag.MensagemAviso = "Código de segurança inválido.<br/>Entre em contato com o suporte!";
                    ViewBag.ReiniciarProcesso = false;
                    return View();
                }

                // Verifica se token ainda está válido
                if (!usuarioService.isValidDataExpiracaoSenha(usuario.Login))
                {
                    ViewBag.MensagemAviso = "Prazo para alteração de senha expirou.<br/>Clique no botão abaixo para reiniciar o processo.";
                    ViewBag.ReiniciarProcesso = true;
                    return View();
                }

                #region [GRAVACAO LOG]
                Log log = new Log();
                log.HashOperacao = Constantes.USUARIO_REDEFINIR_SENHA.ToString(); // REDEFINIR SENHA
                log.JsonOperacao = Bcredi.Utils.Utils.Serialize(usuario);
                log.UsuarioCriador = usuario;
                logService.create(log);
                #endregion [GRAVACAO LOG]

                string senhaCripto = Bcredi.Utils.Utils.Encryption(newpassword);

                usuarioService.updateSenha(login, senhaCripto);

                ViewBag.ReiniciarProcesso = false;

                return RedirectToAction("Login", "Login");
            }
        }

        [AllowAnonymous]
        public ActionResult SenhaRedefinidaSucesso()
        {
            return View();
        }

        public void loginByCookie(string nomeUsuario)
        {
            var model = new UserModel() { Password = "password", UserName = nomeUsuario, RememberMe = true };
            var serializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(model);

            var ticket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, DateTime.Now.AddHours(3), model.RememberMe, serializedUser);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var isSsl = Request.IsSecureConnection; // if we are running in SSL mode then make the cookie secure only

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true, // always set this to true!
                Secure = isSsl,
            };

            if (model.RememberMe) // if the user needs to persist the cookie. Otherwise it is a session cookie
                cookie.Expires = System.DateTime.Today.AddMonths(3); // currently hard coded to 3 months in the future

            Response.Cookies.Set(cookie);


            //FormsAuthentication.SetAuthCookie(model.UserName, false);
            FormsAuthentication.SetAuthCookie(model.UserName, false);
        }

        public Usuario getUsuarioLogado()
        {

            string login = System.Web.HttpContext.Current.User.Identity.Name;

            if (login == "")
            {
                return null;
            }
            else
            {
                Usuario usuario = new Usuario();
                usuario = usuario.getFake();
                return usuario;
            }
        }

        private string getBodyEmail(string senhaGerada, string token)
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
		<a href='http://barsf00019:2000/Login/RedefinirSenha?token=[TOKEN]&s=[SENHA_GERADA]' target='_blank'>Alterar senha</a>
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

            //< a href = '/Bcredi/LoginEmail/[SENHA_GERADA]' >< br >
            //bodyEmail = bodyEmail.Replace("[NOME_USUARIO]", "");
            bodyEmail = bodyEmail.Replace("[SENHA_GERADA]", senhaGerada);
            bodyEmail = bodyEmail.Replace("[TOKEN]", token);
            //TO DO gerar guid quando gerar senha nova bodyEmail = bodyEmail.Replace("[GUID]", Guid.NewGuid().ToString());

            return bodyEmail;
        }

    }
}
