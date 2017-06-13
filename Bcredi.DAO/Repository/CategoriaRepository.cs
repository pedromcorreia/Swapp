using Bcredi.DAO.Repository;
using Bcredi.Utils.Database;
using log4net;
using MySql.Data.MySqlClient;
using Swapp.DAO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Swapp.DAO.Repository
{
    public class CategoriaRepository
    {
        // Referencia para logar as informacoes da classe
        private static readonly ILog log = LogManager.GetLogger(typeof(UsuarioRepository));

        static string ambiente = ConfigurationManager.ConnectionStrings["Connection-Swapp"].ConnectionString;
        static string strConexao = ConfigurationManager.ConnectionStrings[ambiente].ConnectionString;
        MySql.Data.MySqlClient.MySqlConnection conn;
        private DBUtil dbUtil = new DBUtil();

        public List<Categoria> GetListaCategorias()
        {
            List<Categoria> ListaCategoria = new List<Categoria>();


            MySqlConnection Conexao = new MySqlConnection(strConexao); //Criar instancia de conexao strConexao -- minha string conexao
            MySqlCommand Query = new MySqlCommand(); //Cria comando de sql
            Query.Connection = Conexao;//Recebo a conection da conexao 

            //Crio minha sql query
            string sql = @"
                SELECT 
                    idTipoProduto,Descricao
                FROM 
                    tipoproduto
                ";

            try
            {

                Query.CommandText = sql;
                Conexao.Open();//Abre conexão
                MySqlDataReader MysqlConexao = Query.ExecuteReader();//Crie um objeto do tipo reader para ler os dados do banco


                while (MysqlConexao.Read())//Enquanto existir dados no select
                {
                    Categoria Categoria = new Categoria();
                    Categoria.Id = int.Parse(MysqlConexao["idTipoProduto"].ToString());
                    Categoria.Descricao = MysqlConexao["Descricao"].ToString();
                    ListaCategoria.Add(Categoria);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar lista de categoria", ex);
            }
            finally
            {
                Conexao.Close();//Fecha Conexao
            }

            return ListaCategoria;
        }
    }
}
