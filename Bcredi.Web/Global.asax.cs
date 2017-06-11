using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bcredi.Web.Controllers;
//using WebFiltros.Models;
using Bcredi.DAO.Models;
using Bcredi.Utils;
using Bcredi.DAO.Service;

namespace Bcredi.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        LogAcessoService logAcessoService = new LogAcessoService();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterGlobalFilters(GlobalFilters.Filters);

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SecurityAttribute());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            Server.ClearError();

            int statusCode = 0;

            if (lastError.GetType() == typeof(HttpException))
            {
                statusCode = ((HttpException)lastError).GetHttpCode();
            }
            else
            {
                // Não é um erro relacionado a HTTP (404), então é um erro no código. 
                // Sendo assim classificar o erro como código 500
                // 500 (internal server error)
                statusCode = 500;
            }

            HttpContextWrapper contextWrapper = new HttpContextWrapper(this.Context);

            RouteData routeData = new RouteData();

            // Atributos que serão utilizados pelo controller ErrorController
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("statusCode", statusCode);
            routeData.Values.Add("exception", lastError);
            routeData.Values.Add("isAjaxRequet", contextWrapper.Request.IsAjaxRequest());

            if (statusCode == 500)
            {
                //lastError.TargetSite.DeclaringType.Name
                routeData.Values.Add("controllerSourceError", lastError.TargetSite.DeclaringType.FullName);
                routeData.Values.Add("actionSourceError", lastError.TargetSite.Name);
            }

            if (HttpContext.Current != null)
            {
                var url = HttpContext.Current.Request.Url;
                var page = HttpContext.Current.Handler as System.Web.UI.Page;
                routeData.Values.Add("url", url);
                routeData.Values.Add("page", page);
            }

            // Gravar informações no log
            Logger.getLogger().Error(lastError);

            IController controller = new ErrorController();

            RequestContext requestContext = new RequestContext(contextWrapper, routeData);

            controller.Execute(requestContext);
            Response.End();
        }

        void Session_End(object sender, EventArgs e)
        {
            /* Session timeout - Grava informações de LOGOUT no repositório */
            if (this.Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)this.Session["usuario"];
                string sessionUUID = this.Session["sessionUUID"].ToString();

                LogAcesso logAcesso = new LogAcesso();
                logAcesso.HashOperacao = Constantes.LOGOUT_TIMEOUT.ToString(); // LOGOUT_TIMEOUT
                logAcesso.SessionUUID = sessionUUID;
                logAcesso.IpAcesso = usuario.IpUltimoAcesso;
                logAcesso.UsuarioCriador = usuario;
                logAcessoService.create(logAcesso);

                Logger.getLogger().Info("LOGOUT (TIMEOUT) USUARIO " + usuario.Login.ToUpper() + " : " + System.DateTime.Now.ToString());
            }

            this.Session.RemoveAll();

        }
    }
}
