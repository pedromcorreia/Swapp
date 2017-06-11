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
    /// Representa a entidade Cidade
    /// </summary>    public class Cidade : BaseVO
    [DataContract]
    public class Cidade : Base
    {
        /// <summary>
        /// Name:Id
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Name:MunicIbge
        /// </summary>
        [DataMember]
        public int MunicIbge { get; set; }

        /// <summary>
        /// Name:ChaveMun
        /// </summary>
        [DataMember]
        public string ChaveMun { get; set; }

        /// <summary>
        /// Name:MunicdiMob
        /// </summary>
        [DataMember]
        public int MunicdiMob { get; set; }

        /// <summary>
        /// Name:descricao
        /// </summary>
        [DataMember]
        public string descricao { get; set; }

        /// <summary>
        /// Name:ufMun
        /// </summary>
        [DataMember]
        public int ufMun { get; set; }

        /// <summary>
        /// Name:Estado
        /// </summary>
        [DataMember]
        public Estado Estado { get; set; }

    }
}