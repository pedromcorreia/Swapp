using Bcredi.DAO.Models;
using Bcredi.DAO.Repository;
using System;
using System.Collections.Generic;


namespace Bcredi.DAO.Service
{
    public class TipoDocumentoService
    {
        TipoDocumentoRepository tipoDocumentoRepository = new TipoDocumentoRepository();

        public Guid PesquisaTipoDocumento(string descricaoTipoDocumento)
        {
            return tipoDocumentoRepository.PesquisaTipoDocumento(descricaoTipoDocumento);
        }
    }
}