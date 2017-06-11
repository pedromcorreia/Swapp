using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Models
{
    internal class UserModel
    {
        public UserModel()
        {
        }

        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string UserName { get; set; }
    }
}
