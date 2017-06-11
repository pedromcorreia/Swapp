using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bcredi.DAO.Models;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Bcredi.DAO.Models
{
    /// <summary>
    /// Representa a entidade Estado
    /// </summary>
    [DataContract]
    public class Estado : Base
    {
        /// <summary>
        /// Name:Sigla
        /// </summary>
        [DataMember]
        public string Sigla { get; set; }

        /// <summary>
        /// Name:id
        /// </summary>
        [DataMember]
        public int id { get; set; }

        /// <summary>
        /// Name:Descricao
        /// </summary>
        [DataMember]
        public string Descricao { get; set; }
    }
}