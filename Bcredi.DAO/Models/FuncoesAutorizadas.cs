using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Models
{
    public class FuncoesAutorizadas
    {
        /// <summary>
        /// Lista de Funcoes autorizadas.
        /// </summary>
        private ArrayList Funcoes;

        /// <summary>
        /// Criação do objeto. Deve ser passada a lista de funcoes autorizadas.
        /// </summary>
        /// <param name="_funcoesAutorizadas">Lista de funcoes, separadas por vírgulas e sem espaços.</param>
        public FuncoesAutorizadas(string _funcoesAutorizadas)
        {
            Funcoes = new ArrayList(_funcoesAutorizadas.ToLower().Split(','));
        }

        /// <summary>
        /// Cria o objeto com base em um ArrayList.
        /// </summary>
        /// <param name="_funcoesAutorizadas">ArrayList de Strings com os IDs das funcoes permitidas.</param>
        public FuncoesAutorizadas(ArrayList _funcoesAutorizadas)
        {
            Funcoes = _funcoesAutorizadas;
            for (int iCont = 0; iCont < Funcoes.Count; iCont++)
                Funcoes[iCont] = Funcoes[iCont].ToString().ToLower();
        }

        /// <summary>
        /// Verifica se a funcao é permitida.
        /// </summary>
        /// <param name="_funcao">Nome da funcao consultada.</param>
        /// <returns>Verdadeiro se a funcao é permitida.</returns>
        public bool TemAutorizacao(string _funcao)
        {
            return (Funcoes.IndexOf(_funcao.ToLower()) > -1);
        }
    }
}
