using Bcredi.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Models
{
    public class LogAcesso : BaseVO
    {
        [DisplayName("Operação")]
        [DataMember]
        public string HashOperacao { get; set; }

        [DisplayName("ID da Sessão")]
        [DataMember]
        public string SessionUUID { get; set; }

        [DisplayName("Endereço IP")]
        [DataMember]
        public string IpAcesso { get; set; }

        [IgnoreDataMember]
        public string JsonOperacao { get; set; }

        public LogAcesso getFake()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 10000);

            LogAcesso fake = new LogAcesso();
            fake.HashOperacao = Constantes.LOGIN.ToString();
            fake.SessionUUID = "07D3CCD4D9A6A9F3CF9CAD4F9A728F44";
            fake.IpAcesso = "127.0.0.1";
            fake.setId(7);
            fake.setDescricao("fake " + randomNumber);

            return fake;
        }

    }
}
