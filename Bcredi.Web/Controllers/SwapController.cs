using Bcredi.DAO.Models;
using Bcredi.DAO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Globalization;
using System.Threading;


namespace Bcredi.Web.Controllers
{
    public class SwapController : Controller
    {
        // GET: Bcredi
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetUsuarioHeader()
        {
            string nome = Core.UsuarioAtual.Nome;
            var usuarioLogado = Session["usuario"] as Usuario;
            Usuario usuario = usuarioLogado;
            return View(usuario);
        }
    }
}