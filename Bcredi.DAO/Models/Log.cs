using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Models
{
    public class Log : BaseVO
    {
        [DisplayName("Operação")]
        [DataMember]
        public string HashOperacao { get; set; }

        [IgnoreDataMember]
        public string JsonOperacao { get; set; }

        public Log getFake()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 10000);

            Log fake = new Log();
            fake.HashOperacao = "RATING_PARAMETRO_PESQUISAR";
            fake.setId(6);
            fake.setDescricao("fake " + randomNumber);

            return fake;
        }
    }
}
