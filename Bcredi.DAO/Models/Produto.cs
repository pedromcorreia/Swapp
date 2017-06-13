using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bcredi.DAO.Models;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Swapp.DAO.Models
{
    public class Produto : Base
    {
        public string Descricao { get; set; }

        public double Valor { get; set; }

        public string Estado { get; set; }

        public List<Categoria> ListaCategoria { get; set; }

        public List<Documento> ListaFotoProduto { get; set; }
    }
}
