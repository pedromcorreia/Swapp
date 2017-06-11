using Bcredi.DAO.Models;
using Bcredi.DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Service
{
    public class DocumentoService
    {
        DocumentoRepository documento = new DocumentoRepository();
        List<DocumentoLista> documentoLista = new List<DocumentoLista>();

        public List<Documento> CarregarDocumentos(string guidUsuario)
        {
            return documento.CarregarDocumentos(guidUsuario);
        }
        public bool salvarDocumentos(Documento obj)
        {
            return documento.SaveDocumento(obj);
        }

        public List<DocumentoLista> PesquisarDocumentoPosLogado(Guid idUsuario, Guid idItemGenerico, int idPagina, Guid idTipoDocumento)
        {
            return documento.PesquisarDocumentoPosLogado(idUsuario, idItemGenerico, idPagina, idTipoDocumento);
        }

        public List<Documento> PesquisarDocumentoTipo(Guid idUsuario, Guid idItemGenerico, int idPagina, Guid idTipoDocumento)
        {
            return documento.PesquisarDocumentoTipo(idUsuario, idItemGenerico, idPagina, idTipoDocumento);
        }

        public bool InserirDocumentosPosLogado(Documento doc)
        {
            return documento.InserirDocumentosPosLogado(doc);
        }

        public List<Documento> PesquisarDocumentosQuitacaoDivida(Guid idUsuario, Guid idQuitacaoDivida)
        {
            return documento.PesquisarDocumentosQuitacaoDivida(idUsuario, idQuitacaoDivida);
        }
        

    }
}