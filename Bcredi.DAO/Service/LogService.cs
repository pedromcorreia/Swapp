using Bcredi.DAO.Models;
using Bcredi.DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Service
{
    public class LogService
    {
        private LogRepository logRepository = new LogRepository();

        public Log getLogById(int id, bool isFecharConexao = true)
        {
            return logRepository.getLogById(id, isFecharConexao);
        }

        public List<Log> getAllLog()
        {
            return logRepository.getAllLog();
        }

        public bool create(Log log)
        {
            bool response = false;

            response = logRepository.create(log);

            return response;
        }
    }
}
