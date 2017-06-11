using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Models
{
    public class FuncaoMenu
    {
        /// <summary>
        /// Caminho do icone.
        /// </summary>
        public string ArquivoIcone
        {
            get;
            set;
        }

        /// <summary>
        /// Nome interno da Funcao.
        /// </summary>
        public string Nome
        {
            get;
            set;
        }

        /// <summary>
        /// URL de acesso a Funcao.
        /// </summary>
        public string URL
        {
            get;
            set;
        }

        /// <summary>
        /// Titulo da Funcao.
        /// </summary>
        public string Titulo
        {
            get;
            set;
        }

        /// <summary>
        /// funcoes filhas.
        /// </summary>
        public IList<FuncaoMenu> Filhos
        {
            get;
            set;
        }

        /// <summary>
        /// Ambiente ao qual o menu pertence.
        /// </summary>
        public Ambiente Ambiente { get; set; }

        /// <summary>
        /// Palavras-chave da Funcao.
        /// </summary>
        public string PalavrasChave { get; set; }

        /// <summary>
        /// Breadcrumb da Funcao.
        /// </summary>
        public string BreadCrumb { get; set; }

        /// <summary>
        /// Compara dois modelos e retorna verdadeiro se o ID for coincidente.
        /// </summary>
        /// <param name="obj">Objeto a ser comparado.</param>
        /// <returns>Verdadeiro se é igual.</returns>
        public override bool Equals(object obj)
        {
            try
            {
                if (obj == null) return false;

                FuncaoMenu outro = (FuncaoMenu)obj;
                return outro.Nome == this.Nome;
            }
            catch
            {
                return false;
            }
        }
    }
}
