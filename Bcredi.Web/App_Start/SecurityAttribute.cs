using System;
using System.Web;
using System.Web.Mvc;

namespace Bcredi.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SecurityAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            //Validacao de usuario
            if (httpContext.Session["usuario"] != null)
            {
                return true;
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}
