using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Swapp.DAO.Models;
using Swapp.DAO.Service;

namespace Swapp.Web.Controllers
{
    public class ProdutoController : Controller
    {
        ProdutoService produtoService = new ProdutoService();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CriarProduto()
        {
            return View();
        }

        public JsonResult GetListaCategorias()
        {
            try
            {
                List<Categoria> ListaCategoria = produtoService.GetListaCategorias();
                return Json(new { Success = true, Message = "Usuário não encontrado" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Error = true, Message = "Usuário não encontrado" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}