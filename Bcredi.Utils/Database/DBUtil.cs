using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.Utils.Database
{
    public class DBUtil
    {
        private static SqlConnection conexao = null;

        /// <summary>
        /// Abrir a conexao com o banco de dados
        /// </summary>
        /// <returns></returns>
        public SqlConnection openConnection()
        {
            //TODO Marcelo: Adicionar esta string em um arquivo de properties

            String connectionString = ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings["Connection-Swapp"].ToString()].ToString();

            if (conexao == null)
            {
                conexao = new SqlConnection(connectionString);
            }

            if (conexao != null && conexao.State == ConnectionState.Closed)
            {
                conexao.Open();
            }


            return conexao;
        }

        /// <summary>
        /// Fechar a conexao com o banco de dados
        /// </summary>
        public void closeConnection(SqlConnection conexao)
        {
            if (conexao != null && conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        /// <summary>
        /// Fechar a conexao com o banco de dados
        /// </summary>
        public void closeConnection(SqlConnection conexao, bool fecharConexao)
        {
            if (conexao != null && conexao.State == ConnectionState.Open && fecharConexao)
            {
                conexao.Close();
            }
        }

        /// <summary>
        /// Executar um comando de atualização
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="sqlCommand"></param>
        public int executeQuery(String sqlQuery, SqlConnection conexao)
        {
            SqlCommand comandoSQL = new SqlCommand(sqlQuery, conexao);
            return comandoSQL.ExecuteNonQuery();
        }

        /// <summary>
        /// Executar um comando DDL
        /// </summary>
        /// <param name="sqlQuery">comando sql</param>
        /// <param name="sqlCommand">Classe sql command</param>
        /// <param name="conexao">conexao com a base de dados</param>
        /// <returns></returns>
        public int executeQuery(String sqlQuery, SqlCommand sqlCommand, SqlConnection conexao)
        {
            return sqlCommand.ExecuteNonQuery();
        }


        /// <summary>C:\Projetos\SystemOne\Barigui.Utils\packages.config
        /// Executar um comando de atualização
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="sqlCommand"></param>
        public int executeQuery(SqlCommand comandoSQL)
        {
            int chaveGerada = 0;
            chaveGerada = (int)comandoSQL.ExecuteScalar();
            return chaveGerada;
        }

        /// <summary>
        /// Executa um comando DML e retorna o número de linhas afetadas
        /// </summary>
        /// <param name="comandoSQL">SqlCommand</param>
        /// <returns>Número de linhas afetadas</returns>
        public int executeNonQuery(SqlCommand comandoSQL)
        {
            return comandoSQL.ExecuteNonQuery();
        }

        /// <summary>
        /// Recuperar os dados do servidor atraves de uma query
        /// </summary>
        /// <param name="sqlQuery">consulta sql</param>
        /// <param name="conexao">conexão com a base de dados</param>
        /// <returns></returns>
        public SqlDataReader getDados(string sqlQuery, SqlConnection conexao)
        {
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, conexao);
            SqlDataReader dados = sqlCommand.ExecuteReader();
            return dados;
        }

        /// <summary>
        /// Recuperar os dados do servidor atraves de uma query
        /// </summary>
        /// <param name="sqlCommand">comando com uma consulta sql</param>
        /// <param name="conexao">conexão com a base de dados</param>
        /// <returns></returns>
        public SqlDataReader getDados(SqlCommand sqlCommand)
        {
            SqlDataReader dados = sqlCommand.ExecuteReader();
            return dados;
        }
    }
}
