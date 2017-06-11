using System;
using Dapper;
using log4net;
using Bcredi.Utils.Database;
using Bcredi.DAO.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace Bcredi.DAO.Repository
{
    public class TokenRepository
    {
        // Referencia para logar as informacoes da classe
        private static readonly ILog log = LogManager.GetLogger(typeof(TokenRepository));

        static string ambiente = ConfigurationManager.ConnectionStrings["ConnectionBcredi"].ConnectionString;
        static string strConexao = ConfigurationManager.ConnectionStrings[ambiente].ConnectionString;
        private DBUtil dbUtil = new DBUtil();
        
        public bool VerificarToken(string token)
        {
            var tokenAtivo = false;

            using (var conexaoBD = new SqlConnection(strConexao))
            {
                var sql = @"SELECT dataValidadeToken
                            FROM AL_TOKEN_SITE_TO_APP
                            WHERE token = @Token
                            AND dataValidadeToken >= GETDATE()";
                conexaoBD.Open();
                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexaoBD);
                SqlParameter paramToken = new SqlParameter("@Token", token);
                sqlCommand.Parameters.Add(paramToken);

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                if (dados.Read())
                {
                    tokenAtivo = true;
                }
                conexaoBD.Close();
                return tokenAtivo;
            }
        }

        public Usuario RetornarToken(string email)
        {
            Usuario usuario = null;

            using (var conexaoBD = new SqlConnection(strConexao))
            {
                var sql = @"SELECT password, tokenResetSenha
                            FROM USUARIO
                            WHERE email = @Email
                            ";
                using (var resultado = conexaoBD.QueryMultiple(sql, new { Email = email }))
                {
                    List<Usuario> lista = resultado.Read<Usuario>().ToList<Usuario>();
                    if (lista != null && lista.Count > 0)
                    {
                        usuario = lista.Single();
                    }
                }
            }

            return usuario;
        }
    }
}
