using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Models
{
    public class Documento
    {
        public Guid IdDocumento { get; set; }

        public string IdUsuario { get; set; }

        public Guid IdTipoDocumento { get; set; }

        public string CaminhoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public int IsAtivo { get; set; }

        public List<Documento> DocumentoLista { get; set; }

        public int idUsuarioInclusao { get; set; }

        public string dataInclusao { get; set; }

        public int idUsuarioAtualizacao { get; set; }

        public string dataAtualizacao { get; set; }

        public string NomeDocumento { get; set; }

        public Guid idItemGenerico { get; set; }

        public DateTime dataRecusa { get; set; }

        public Guid idDocumentoMotivo { get; set; }

        public string NomeBcrediDocumento { get; set; }

    }

    public class DocumentoLista
    {
        public List<Documento> DocumentosLista { get; set; }
    }
}
