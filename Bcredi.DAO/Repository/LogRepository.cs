using Bcredi.DAO.Models;
using Bcredi.Utils;
using Bcredi.Utils.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Repository
{
    class LogRepository
    {
        private DBUtil dbUtil = new DBUtil();

        public bool create(Log log)
        {
            bool response = false;

            int chaveGerada = 0;

            string sql = @"
                        INSERT INTO {0}.dbo.SYS_LOG (
                              hashOperacao
                            , jsonOperacao
                            , idUsuarioInclusao)
                        OUTPUT INSERTED.idLog 
                        VALUES (
                             @hashOperacao
                            ,@jsonOperacao
                            ,@idUsuarioInclusao
                        )
            ";

            SqlConnection conexao = null;
            SqlTransaction transacao = null;

            try
            {
                conexao = dbUtil.openConnection();
                transacao = conexao.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao, transacao);

                SqlParameter paramHashOperacao = new SqlParameter("@hashOperacao", System.Data.SqlDbType.VarChar);
                paramHashOperacao.Value = log.HashOperacao;

                SqlParameter paramJsonOperacao = new SqlParameter("@jsonOperacao", System.Data.SqlDbType.VarChar);
                paramJsonOperacao.Value = log.JsonOperacao;

                SqlParameter paramUsuarioInclusao = new SqlParameter("@idUsuarioInclusao", System.Data.SqlDbType.Int);
                paramUsuarioInclusao.Value = Core.UsuarioAtual != null ? Core.UsuarioAtual.Id : log.UsuarioCriador.Id;

                sqlCommand.Parameters.Add(paramHashOperacao);
                sqlCommand.Parameters.Add(paramJsonOperacao);
                sqlCommand.Parameters.Add(paramUsuarioInclusao);

                chaveGerada = dbUtil.executeQuery(sqlCommand);
                transacao.Commit();

                if (chaveGerada > 0)
                {
                    response = true;
                }

            }
            catch (Exception ex)
            {
                transacao.Rollback();
                throw new Exception("Erro ao realizar tentativa de criação de novo registro de log", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return response;
        }

        public Log getLogById(int id, bool fecharConexao)
        {
            Log Log = new Log();

            string sql = @"
                SELECT
                      L.idLog as Id
                    , L.hashOperacao
			        , L.isAtivo
                    , USR_INSERT.idUsuario as idUsuarioInclusao
                    , USR_INSERT.nome as nomeInclusao 
                    , CONVERT(VARCHAR(10), R.dataInclusao, 103) + ' ' + convert(VARCHAR(8), R.dataInclusao, 14) as dataInclusao
                FROM {0}.dbo.SYS_LOG L
				    INNER JOIN {0}.dbo.USUARIO USR_INSERT ON USR_INSERT.idUsuario = L.idUsuarioInclusao
                WHERE 
                    L.isAtivo = 1
                    AND L.idLog = @id
                ORDER BY L.idLog ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                SqlParameter parametroId = new SqlParameter("@id", System.Data.SqlDbType.Int);
                parametroId.Value = id;

                sqlCommand.Parameters.Add(parametroId);


                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                while (dados.Read())
                {
                    Usuario usuarioInclusao = new Usuario();
                    usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());

                    Log.setId(int.Parse(dados["Id"].ToString()));
                    Log.HashOperacao = dados["hashOperacao"].ToString().TrimEnd();
                    Log.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));
                    Log.setUsuarioCriador(usuarioInclusao);
                    Log.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar dados de log código " + id, ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao, fecharConexao);
            }

            return Log;

        }

        public List<Log> getAllLog()
        {
            List<Log> lista = new List<Log>();

            string sql = @"
                SELECT
                      L.idLog as Id
                    , L.hashOperacao
			        , L.isAtivo
                    , USR_INSERT.idUsuario as idUsuarioInclusao
                    , USR_INSERT.nome as nomeInclusao 
                    , CONVERT(VARCHAR(10), R.dataInclusao, 103) + ' ' + convert(VARCHAR(8), R.dataInclusao, 14) as dataInclusao
                FROM {0}.dbo.SYS_LOG L
				    INNER JOIN {0}.dbo.USUARIO USR_INSERT ON USR_INSERT.idUsuario = L.idUsuarioInclusao
                WHERE 
                    L.isAtivo = 1
                ORDER BY L.idLog ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                while (dados.Read())
                {
                    Log Log = new Log();
                    Usuario usuarioInclusao = new Usuario();
                    usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());

                    Log.setId(int.Parse(dados["Id"].ToString()));
                    Log.HashOperacao = dados["hashOperacao"].ToString().TrimEnd();
                    Log.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));
                    Log.setUsuarioCriador(usuarioInclusao);
                    Log.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));

                    lista.Add(Log);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar lista de logs", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return lista;
        }

    }
}
