using System.Collections.Generic;
using Swapp.DAO.Models;
using Swapp.DAO.Repository;

namespace Swapp.DAO.Service
{
    public class ProdutoService
    {
        CategoriaRepository categoriaRepository = new CategoriaRepository();

        public List<Categoria> GetListaCategorias()
        {
            return categoriaRepository.GetListaCategorias();
        }
    }
}
