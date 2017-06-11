using System;

namespace Bcredi.DAO.Models.ViewModel

{
    /// <summary>
    /// Tipo de documento enviado
    /// </summary>
    public class TipoDocumentoViewModel
    {
        /// <summary>
        /// Tipo do documento
        /// </summary>
        public int IdTipoDocumento { get; set; }

        /// <summary>
        /// Descricao do tipo de documento
        /// </summary>
        public string TipoDocumentoDescricao { get; set; }

        /// <summary>
        /// Serviço de upload
        /// </summary>
        public string UrlServicoUpload { get; set; }

        /// <summary>
        /// Status do documento
        /// </summary>
        public int IdStatusDocumento { get; set; }

        public TipoDocumentoViewModel()
        {
        }

        public string CampoGenerico { get; set; }

        /// <summary>
        /// Guid do Usuario
        /// </summary>
        public string GuidUsuario { get; set; }

        /// <summary>
        /// Guid da Proposta 
        /// </summary>
        public string GuidProposta { get; set; }

        /// <summary>
        /// GuidTipoDocumento
        /// </summary>
        public string GuidTipoDocumento { get; set; }

        public TipoDocumentoViewModel(int TipoDocumento, string TipoDocumentoDescricao, string UrlServicoUpload, int IdStatusDocumento)
        {
            this.IdTipoDocumento = TipoDocumento;
            this.TipoDocumentoDescricao = TipoDocumentoDescricao;
            this.UrlServicoUpload = UrlServicoUpload;
            this.IdStatusDocumento = IdStatusDocumento;
        }
        /// <summary>
        /// Metodo para salvar o tipo do documento  
        /// </summary>
        /// <param name="GuidTipoDocumento"></param>
        /// <param name="GuidUsuario"></param>
        /// <param name="TipoDocumentoDescricao"></param>
        /// <param name="UrlServicoUpload"></param>
        /// <param name="IdStatusDocumento"></param>
        public TipoDocumentoViewModel(string GuidTipoDocumento, string GuidProposta, string GuidUsuario, string TipoDocumentoDescricao, string UrlServicoUpload, int IdStatusDocumento)
        {
            this.GuidTipoDocumento = GuidTipoDocumento;
            this.GuidProposta = GuidProposta;
            this.GuidUsuario = GuidUsuario;
            this.TipoDocumentoDescricao = TipoDocumentoDescricao;
            this.UrlServicoUpload = UrlServicoUpload;
            this.IdStatusDocumento = IdStatusDocumento;
        }

        public TipoDocumentoViewModel(string GuidTipoDocumento, string GuidProposta, string GuidUsuario, string TipoDocumentoDescricao, string UrlServicoUpload, int IdStatusDocumento, string campoGenerico)
        {
            this.GuidTipoDocumento = GuidTipoDocumento;
            this.GuidProposta = GuidProposta;
            this.GuidUsuario = GuidUsuario;
            this.TipoDocumentoDescricao = TipoDocumentoDescricao;
            this.UrlServicoUpload = UrlServicoUpload;
            this.IdStatusDocumento = IdStatusDocumento;
            this.CampoGenerico = campoGenerico;
        }
    }
}
