using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bcredi.DAO.Models
{
    public class Core
    {
        private Usuario usuario;

        public Usuario Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        /// <summary>
        /// Retorna o usuário atual logado.
        /// </summary>
        public static Usuario UsuarioAtual
        {
            get
            {
                return HttpContext.Current != null ? (Usuario)HttpContext.Current.Session["usuario"] : null;
            }

            set
            {
                HttpContext.Current.Session.Add("usuario", value);
            }
        }

        /// <summary>
        /// Retorna o UUID (Universal Unique IDentifier) da sessão atual.
        /// </summary>
        public static String SessionUUID
        {
            get
            {
                return HttpContext.Current != null ? (String)HttpContext.Current.Session["sessionUUID"] : null;
            }

            set
            {
                HttpContext.Current.Session.Add("sessionUUID", value.ToUpper());
            }
        }

        /// <summary>
        /// Ambiente em que o usuário se encontra atualmente.
        /// </summary>
        public static Ambiente AmbienteAtual
        {
            get
            {
                return (Ambiente)HttpContext.Current.Session["Ambiente"];
            }

            set
            {
                HttpContext.Current.Session.Add("Ambiente", value);
            }
        }

        /// <summary>
        /// funcoes permitidas com hierarquia.
        /// </summary>
        public static IList<FuncaoMenu> FuncoesMenu
        {
            get
            {
                return (IList<FuncaoMenu>)HttpContext.Current.Session["FuncoesMenu"];
            }

            set
            {
                HttpContext.Current.Session.Add("FuncoesMenu", value);
            }
        }

        /// <summary>
        /// Lista de funcoes-menu permitidas, sem hierarquia.
        /// </summary>
        public static IList<FuncaoMenu> ListaFuncoesMenu
        {
            get
            {
                return (IList<FuncaoMenu>)HttpContext.Current.Session["ListaFuncoesMenu"];
            }

            set
            {
                HttpContext.Current.Session.Add("ListaFuncoesMenu", value);
            }
        }

        /// <summary>
        /// Nome do banco de dados Bcredi.
        /// </summary>
        public static string BcrediDB
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["BcrediDB"].ToString();
            }
        }

        /// <summary>
        /// Nome do banco de dados Security.
        /// </summary>
        public static string SecurityDB
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SecurityDB"].ToString();
            }
        }

        /// <summary>
        /// Busca se o usuário logado tem permissão em terminada funcao.
        /// </summary>
        /// <param name="_nomeFuncao">Nome da funcao.</param>
        /// <returns>Verdadeiro se o usuário tiver permissão.</returns>
        public static bool TemPermissao(string _nomeFuncao)
        {
            if (HttpContext.Current == null)
                return false;
            if (HttpContext.Current.Session["FuncoesAutorizadas"] == null)
                return false;
            return (((FuncoesAutorizadas)HttpContext.Current.Session["FuncoesAutorizadas"]).TemAutorizacao(_nomeFuncao));
        }

        /// <summary>
        /// Busca o caminho da funcao atual.
        /// </summary>
        /// <param name="_nomeFuncaoAtual">Nome da funcaoi atual.</param>
        /// <returns>Retorna uma lista separada por vírgula do caminho atual.</returns>
        public static IList<string> ListaParaBread(string _nomeFuncaoAtual)
        {
            IList<string> funcoes = new List<string>();
            // Se não conseguir apenas não montará o bread
            try
            {
                string novoNome = _nomeFuncaoAtual;
                if (Core.AmbienteAtual != null)
                    novoNome = Core.AmbienteAtual.Nome + "." + _nomeFuncaoAtual;
                IList<FuncaoMenu> funcoesmenu = ListaFuncoesMenu;
                FuncaoMenu funcao = funcoesmenu.Where(r => r.Nome.Equals(novoNome)).LastOrDefault();
                if (funcao == null)
                    funcao = funcoesmenu.Where(r => r.Nome.Equals(_nomeFuncaoAtual)).LastOrDefault();

                foreach (string nomeFuncao in funcao.BreadCrumb.Split(new char[] { ',' }))
                    funcoes.Add(nomeFuncao);
                funcoes.Add(funcao.Titulo);
            }
            catch (Exception)
            {
                funcoes.Add(_nomeFuncaoAtual);
            }

            return funcoes;
        }

        /// <summary>
        /// Monta o título da página a partir do BreadCrumb.
        /// </summary>
        /// <param name="_breadCrumb">BreadCrumb da tela.</param>
        /// <returns>Título da tela.</returns>
        public static string BreadCrumbParaTitulo(string _breadCrumb)
        {
            string sRetorno = String.Empty;
            // Se não conseguir apenas não montará o título
            try
            {
                string novoNome = _breadCrumb;
                if (Core.AmbienteAtual != null)
                    novoNome = Core.AmbienteAtual.Nome + "." + _breadCrumb;
                IList<FuncaoMenu> funcoesmenu = ListaFuncoesMenu;
                FuncaoMenu funcao = funcoesmenu.Where(r => r.Nome.Equals(novoNome)).LastOrDefault();
                if (funcao == null)
                    funcao = funcoesmenu.Where(r => r.Nome.Equals(_breadCrumb)).LastOrDefault();

                foreach (string nomeFuncao in funcao.BreadCrumb.Split(new char[] { ',', ' ' }))
                    sRetorno += nomeFuncao + " > ";
                sRetorno += funcao.Titulo + " - ";

            }
            catch (Exception)
            {
            }

            sRetorno += "Portal";

            return sRetorno;
        }

        /// <summary>
        /// Envia uma mensagem de e-mail.
        /// </summary>
        /// <param name="_mensagem">Mensagem a ser enviada.</param>
        /// <remarks>
        /// Para que esta função funcione, quatro parâmetros precisam ser configurados no web.config (seção AppSettings).
        /// <ul>
        /// <li><b>SMTPHost</b>: Endereço de SMTP </li>
        /// <li><b>SMTPPort</b>: Porta do SMTP (exemplo: 21).</li>
        /// <li><b>SMTPUser</b>: Usuário de autenticação do SMTP .</li>
        /// <li><b>SMTPPass</b>: Senha do usuário para autenticação.</li>
        /// </ul>
        /// </remarks>
        /// <returns>Verdadeiro se o e-mail foi enviado.</returns>
        public static bool EnviaEmail(MailMessage _mensagem)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["SMTPHost"];
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUser"], ConfigurationManager.AppSettings["SMTPPass"]);

                smtp.Send(_mensagem);

                smtp.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
