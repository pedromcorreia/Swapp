using Bcredi.DAO.Models;
using System;
using System.Web;
using System.Web.Mvc;
using Bcredi.DAO.Service;
using System.IO;
using System.Collections.Generic;

namespace Bcredi.Web.Controllers
{
    public class FileController : Controller
    {
        DocumentoService documentoService = new DocumentoService();
        TipoDocumentoService tipoDocumentoService = new TipoDocumentoService();

        /// <summary>
        /// Metodo para salvar os arquivos
        /// Receber o arquivo via parametro tipo HttpPostedFileBase files e string name
        /// Salva o arquivo 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="GuidUsuario"></param>
        /// <param name="guidProposta"></param>
        /// <param name="GuidTipoDocumento"></param>
        /// <param name="idstatusdocumento"></param>
        public void SaveFile(HttpPostedFileBase file, string GuidUsuario, string guidProposta, string GuidTipoDocumento, string idstatusdocumento, string campoGenerico)
        {
            if (file != null && GuidTipoDocumento != null)
            {
                if (file.ContentLength > 0)
                {
                    Documento documento = new Documento();

                    var fileNameOriginal = Path.GetFileName(file.FileName);
                    string GuidDocumentoString = Guid.NewGuid().ToString();
                    Guid GuidDocumento = Guid.NewGuid();
                    string FileNameBcredi = BuildFileName(fileNameOriginal, GuidDocumentoString);
                    string newPath = "C:\\AreaLogada\\";
                    newPath = newPath + "\\Documento\\Proposta\\" + guidProposta + "\\Usuario\\" + GuidUsuario + "\\TipoDocumento\\" + GuidTipoDocumento + "\\GuidDocumento\\" + GuidDocumentoString + "\\";
                    Directory.CreateDirectory(newPath);
                    var path = Path.Combine(newPath, FileNameBcredi);
                    file.SaveAs(path);
                    documento.IdTipoDocumento = new Guid(GuidTipoDocumento);
                    documento.IdDocumento = GuidDocumento;
                    documento.CaminhoDocumento = path;
                    documento.NomeDocumento = file.FileName;
                    documento.NomeBcrediDocumento = FileNameBcredi;
                    
                    documento.IdUsuario = GuidUsuario;

                    documentoService.InserirDocumentosPosLogado(documento);

                    
                }
            }
        }

        [HttpPost]
        public JsonResult SaveAjaxFile(string guidUsuario, string guidProposta, string idtipodocumento, string idstatusdocumento, string campoGenerico)
        {
            var flag = Request.Files[0] as HttpPostedFileBase;

            int quantidadeArquivos = Request.Files.Count;

            try
            {
                for (int i = 0; i < quantidadeArquivos; i++)
                {
                    var file = Request.Files[i] as HttpPostedFileBase;
                    var GuidDocumento = Request.Files.AllKeys[i];

                    //##<METODO PARA SALVAR ARQUIVOS NA BASE E PASTA>
                    SaveFile(file, guidUsuario, guidProposta, idtipodocumento, idstatusdocumento, campoGenerico);
                }
                return Json(new { Success = true, Message = "Sucesso", Data = "" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = "Erro ao inserir o Imóvel {" + ex + "}" }, JsonRequestBehavior.AllowGet);
            }
        }

        public string BuildFileName(string fileNameOriginal, string GuidDocumentoString)
        {
            string Extensao = Path.GetExtension(fileNameOriginal);

            string NewFileName = GuidDocumentoString + Extensao;

            return NewFileName;
        }
    }

}