using Bcredi.DAO.Models;
using Bcredi.DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Service
{
    public class BaseVOService
    {
        BaseVORepository baseVORepository = new BaseVORepository();

        public void deleteObject(BaseVO objeto, string tableName)
        {
            baseVORepository.deleteObject(objeto, tableName);
        }

        public List<BaseVO> getAll(string tableName, string orderBy = "1")
        {
            return baseVORepository.getAll(tableName, orderBy);
        }

        public List<BaseVO> getDescriptionPartial(string tableName, string texto)
        {
            return baseVORepository.getDescriptionPartial(tableName, texto);
        }

        public BaseVO getObjectById(int id, string tableName)
        {
            return baseVORepository.getObjectById(id, tableName);
        }

        public int insertObject(BaseVO objeto, string tableName)
        {
            return baseVORepository.insertObject(objeto, tableName);

        }

        public int updateObject(BaseVO objeto, string tableName)
        {
            return baseVORepository.updateObject(objeto, tableName);
        }

        public int PesquisaCliente(string txtNumeroCliente, int txtCPFCNPJ, int txtContrato)
        {
            return PesquisaCliente(txtNumeroCliente, txtCPFCNPJ, txtContrato);
        }

        public BaseVO getObjectByDescription(string tableName, string texto)
        {
            return baseVORepository.getObjectByDescription(tableName, texto);
        }
    }
}
