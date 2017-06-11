using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Models
{
    public class Ambiente
    {
        /// <summary>
        /// Código do ambiente.
        /// </summary>
        public virtual int ID
        {
            get;
            set;
        }

        /// <summary>
        /// Nome do ambiente.
        /// </summary>
        public virtual string Nome
        {
            get;
            set;
        }

        /// <summary>
        /// Título do ambiente.
        /// </summary>
        public virtual string Titulo
        {
            get;
            set;
        }

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

                // if (this.GetType() != obj.GetType()) return false;

                Ambiente outro = (Ambiente)obj;
                return outro.ID == this.ID;
            }
            catch
            {
                return false;
            }
        }
    }
}
