using Bcredi.DAO.Models;
using Bcredi.DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Service
{
    public class LogAcessoService
    {
        private LogAcessoRepository logAcessoRepository = new LogAcessoRepository();

        public LogAcesso getLogAcessoById(int id, bool isFecharConexao = true)
        {
            return logAcessoRepository.getLogAcessoById(id, isFecharConexao);
        }

        public List<LogAcesso> getAllLogAcesso()
        {
            return logAcessoRepository.getAllLogAcesso();
        }

        public bool create(LogAcesso logAcesso)
        {
            bool response = false;

            response = logAcessoRepository.create(logAcesso);

            return response;
        }
    }
}
