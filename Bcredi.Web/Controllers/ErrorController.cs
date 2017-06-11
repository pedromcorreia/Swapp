using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bcredi.Models;


namespace Bcredi.Web.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(int statusCode, Exception exception, bool isAjaxRequet, string url, string controllerSourceError, string actionSourceError)
        {
            Response.StatusCode = statusCode;

            // Caso não seja uma requisição Ajax, retornar uma view com o erro (Model Error)
            if (!isAjaxRequet)
            {
                Error model = new Error
                {
                    HttpStatusCode = statusCode,
                    Exception = exception,
                    Url = url,
                    ControllerSourceError = controllerSourceError,
                    ActionSourceError = actionSourceError
                };

                return View(model);
            }
            else
            {
                // Caso seja uma requisição Ajax, retornar uma mensagem tipo Json
                var errorObjet = new
                {
                    message = exception.Message,
                    Url = url,
                    ControllerSourceError = controllerSourceError,
                    ActionSourceError = actionSourceError
                };
                return Json(errorObjet, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
