using Bcredi.DAO.Models;
using Bcredi.DAO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;

namespace Bcredi.Web.Controllers
{
    public class DocumentosController : Controller
    {
        //DadosPessoaisService dadosPessoaisService = new DadosPessoaisService();
        DocumentoService documentoService = new DocumentoService();
        //RendaService rendaService = new RendaService();
        TipoDocumentoService tipoDocumentoService = new TipoDocumentoService();
        public ActionResult Documentos()
        {
            var usuarioLogado = Session["usuario"] as Usuario;
            ViewBag.GuidUsuario = usuarioLogado.GuidUsuario;

            List<Documento> listaDocumentos = documentoService.CarregarDocumentos(usuarioLogado.GuidUsuario);
            ViewBag.listaDocumentos = listaDocumentos;
          

            return View();
        }
        [HttpGet]
        public JsonResult DocumentosJson(string guidUsuario)
        {

            VM_UsuarioComplementoDadosBancarios json = new VM_UsuarioComplementoDadosBancarios();
            string sjson = Utils.Utils.Serialize<VM_UsuarioComplementoDadosBancarios>(json);

            //verificar se o email do cliente esta na lista de signatarios e ja foi assinado            
            if (!string.IsNullOrEmpty(guidUsuario))
            {

                List<Documento> listaDocumentos = new List<Documento>();

                return Json(new
                {
                    Success = true,
                    Message = "Sucesso",
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Error = true, Message = "Usuário não encontrado" }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Documentos(string dadosPessoaisJson)
        {

            VM_UsuarioComplementoDadosBancarios dadosPessoais = new VM_UsuarioComplementoDadosBancarios();

            dadosPessoais = Utils.Utils.Deserialize<VM_UsuarioComplementoDadosBancarios>(dadosPessoaisJson);

            string Proposta = "proposta";
            string GuidUsuario = "";
            //Salvar Arquivo
            if (true)
            {
                HttpPostedFileBase file = Request.Files["File"];
                //Adiciona o server mais o nome da pasta
                string arquivo = ("C:\\fakepath\\ea648b600ebb59ad83cfd9de71e549d1.jpg");
                string tipoArquivo = arquivo.Substring(arquivo.IndexOf('.') + 1);
                arquivo = arquivo.Substring(arquivo.IndexOf('\\') + 1);
                string newPath = Path.Combine(Proposta, GuidUsuario);
                //string newPath = Path.Combine(Bcredi.Utils.Constantes.SERVER_PATH_FILE, Bcredi.Utils.Constantes.NOME_PASTA_CARTEIRA, idcarteirareferencia, Bcredi.Utils.Constantes.NOME_PASTA_PLANILHA);
                //string newPath = Bcredi.Utils.Constantes.SERVER_PATH_FILE;
                //Melhoria - alterar assim que souber o caminho correto
                //Caminho no banco
                newPath = newPath + "C:\\Temp\\" + Proposta + "\\" + GuidUsuario;
                //Cria a pasta
                Directory.CreateDirectory(newPath);
                //Concatena diretorio com a arquivo
                string fname = Path.Combine(newPath, arquivo);
                //string mimetype = Path.GetExtension(fname);

                //FileInfo fi = new FileInfo(fname);
                file.SaveAs(fname);

                long tamanhoLong = file.ContentLength;

                if ((tipoArquivo == "jpg") && (tipoArquivo == "png"))
                {
                    file.SaveAs(fname);
                }

            }

            //comunicação com banco
            bool isValid = new bool();

            if (isValid)
            {
                return Json(new { Success = true, Message = "Sucesso", Data = dadosPessoaisJson }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Error = true, Message = "Usuário não encontrado" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Doc(HttpPostedFileBase[] files, FormCollection _form)
        {
            var usuarioLogado = Session["usuario"] as Usuario;

            StringBuilder mensagemInconsistenciaFoto = new StringBuilder();
            int flag = 0;

            //VM_DadosPessoais dadosPessoais = dadosPessoaisService.PesquisaUsuarioComplemento(usuarioLogado.GuidUsuario);
            //string GuidConjuge = dadosPessoais.IdConjugeRelacional;
            List<Usuario> ListaPreponente = new List<Usuario>();
           
            foreach (var file in files)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {

                        var fileName = Path.GetFileName(file.FileName);
                        string mimetype = Path.GetExtension(fileName);
                        mimetype = mimetype.ToLower();

                        long tamanhoLong = file.ContentLength;

                        if ((mimetype != ".jpg") && (mimetype != ".png") && (mimetype != ".pdf"))
                        {
                            flag = 1;
                        }
                        else if (tamanhoLong > 25165824)
                        {
                            flag = 1;
                        }
                    }
                }
            }

            if (flag == 0)
            {
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            Documento documento = new Documento();

                            var fileName = Path.GetFileName(file.FileName);
                            Guid GuidDocumento = Guid.NewGuid();
                            string GuidDocumentoString = GuidDocumento.ToString();
                            string newPath = "C:\\Temp\\";
                            newPath = newPath + "\\Bcredi\\Documento\\" + GuidDocumentoString + "\\" + usuarioLogado.GuidUsuario + "\\";
                            Directory.CreateDirectory(newPath);
                            var path = Path.Combine(newPath, fileName);

                            file.SaveAs(path);
                            documento.IdUsuario = usuarioLogado.GuidUsuario;
                            string descricaoTipoDocumento = "CPF";
                            documento.IdTipoDocumento = tipoDocumentoService.PesquisaTipoDocumento(descricaoTipoDocumento);
                            documento.IdDocumento = GuidDocumento;
                            documento.CaminhoDocumento = path;
                            documento.NomeDocumento = file.FileName;
                            documentoService.salvarDocumentos(documento);
                        }
                    }
                }
            }
            if (flag == 1)
            {
                mensagemInconsistenciaFoto.Append("Erro");
                ViewBag.mensagemInconsistenciaFoto = mensagemInconsistenciaFoto;
                return View("Documentos");
            }

            return RedirectToAction("FichaProposta", "FichaProposta");
        }

        [HttpGet]
        public JsonResult DocumentosStatusJson(string guidUsuario)
        {
            bool status = false;
            //Mario Criar metodo que retorna se existe idUsuario = guidUsuario
            List<Documento> listaDocumentos = documentoService.CarregarDocumentos(guidUsuario);
            if (listaDocumentos.Count > 0)
            {
                status = true;

                if (!string.IsNullOrEmpty(guidUsuario))
                {
                    //String dadosJson = Utils.Utils.Serialize<bool>(status);

                    return Json(new
                    {
                        Success = true,
                        Message = "Sucesso",
                        Data = 1
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Error = true, Message = "Usuário não encontrado", Data = 0 }, JsonRequestBehavior.AllowGet);
        }

    }
}