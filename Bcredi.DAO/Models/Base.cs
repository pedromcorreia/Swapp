using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Models
{
    public class Base
    {
        public Guid IdUsuarioInclusao { get; set; }

        public DateTime DataInclusao { get; set; }

        public Guid IdUsuarioAtualizacao { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public bool IsAtivo { get; set; }
    }
}
