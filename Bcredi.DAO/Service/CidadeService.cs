using Bcredi.DAO.Models;
using Bcredi.DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Service
{
    public class CidadeService
    {
        CidadeRepository cidadeRepository = new CidadeRepository();
        public Cidade GetCidadeById(int id)
        {
            return cidadeRepository.GetCidadeById(id);
        }
        public Cidade GetCidadeByCidadeeEstado(string NomeCidade, int EstadoId)
        {
            return cidadeRepository.GetCidadeByCidadeeEstado(NomeCidade, EstadoId);
        }
    }
}