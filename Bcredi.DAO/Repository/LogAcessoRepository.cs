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
    class LogAcessoRepository
    {
        private DBUtil dbUtil = new DBUtil();

        public bool create(LogAcesso logAcesso)
        {
            bool response = false;

            int chaveGerada = 0;

            string sql = @"
                        INSERT INTO {0}.dbo.SYS_LOG_ACESSO (
                              hashOperacao
                            , sessionUUID
                            , ipAcesso
                            , idUsuarioInclusao)
                        OUTPUT INSERTED.idLogAcesso 
                        VALUES (
                             @hashOperacao
                            ,@sessionUUID
                            ,@ipAcesso
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
                paramHashOperacao.Value = logAcesso.HashOperacao;

                // Exemplo: "07D3CCD4D9A6A9F3CF9CAD4F9A728F44"
                SqlParameter paramSessionUUID = new SqlParameter("@sessionUUID", System.Data.SqlDbType.VarChar);
                paramSessionUUID.Value = logAcesso.SessionUUID.ToUpper();

                SqlParameter paramIpAcesso = new SqlParameter("@ipAcesso", System.Data.SqlDbType.VarChar);
                paramIpAcesso.Value = logAcesso.IpAcesso;

                SqlParameter paramUsuarioInclusao = new SqlParameter("@idUsuarioInclusao", System.Data.SqlDbType.Int);
                paramUsuarioInclusao.Value = Core.UsuarioAtual != null ? Core.UsuarioAtual.Id : logAcesso.UsuarioCriador.Id;

                sqlCommand.Parameters.Add(paramHashOperacao);
                sqlCommand.Parameters.Add(paramSessionUUID);
                sqlCommand.Parameters.Add(paramIpAcesso);
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
                throw new Exception("Erro ao realizar tentativa de criação de novo registro de log Acesso", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return response;
        }

        public LogAcesso getLogAcessoById(int id, bool fecharConexao)
        {
            LogAcesso LogAcesso = new LogAcesso();

            string sql = @"
                SELECT
                      L.idLogAcesso as Id
                    , L.hashOperacao
                    , L.sessionUUID
                    , L.ipAcesso
			        , L.isAtivo
                    , USR_INSERT.idUsuario as idUsuarioInclusao
                    , USR_INSERT.nome as nomeInclusao 
                    , CONVERT(VARCHAR(10), R.dataInclusao, 103) + ' ' + convert(VARCHAR(8), R.dataInclusao, 14) as dataInclusao
                FROM {0}.dbo.SYS_LOG_ACESSO L
				    INNER JOIN {0}.dbo.USUARIO USR_INSERT ON USR_INSERT.idUsuario = L.idUsuarioInclusao
                WHERE 
                    L.isAtivo = 1
                    AND L.idLogAcesso = @id
                ORDER BY L.idLogAcesso ";

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

                    LogAcesso.setId(int.Parse(dados["Id"].ToString()));
                    LogAcesso.HashOperacao = dados["hashOperacao"].ToString().TrimEnd();
                    LogAcesso.SessionUUID = dados["sessionUUID"].ToString().TrimEnd();
                    LogAcesso.IpAcesso = dados["ipAcesso"].ToString().TrimEnd();
                    LogAcesso.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));
                    LogAcesso.setUsuarioCriador(usuarioInclusao);
                    LogAcesso.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar dados de log Acesso código " + id, ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao, fecharConexao);
            }

            return LogAcesso;

        }

        public List<LogAcesso> getAllLogAcesso()
        {
            List<LogAcesso> lista = new List<LogAcesso>();

            string sql = @"
                SELECT
                      L.idLogAcesso as Id
                    , L.hashOperacao
                    , L.sessionUUID
                    , L.ipAcesso
			        , L.isAtivo
                    , USR_INSERT.idUsuario as idUsuarioInclusao
                    , USR_INSERT.nome as nomeInclusao 
                    , CONVERT(VARCHAR(10), R.dataInclusao, 103) + ' ' + convert(VARCHAR(8), R.dataInclusao, 14) as dataInclusao
                FROM {0}.dbo.SYS_LOG_ACESSO L
				    INNER JOIN {0}.dbo.USUARIO USR_INSERT ON USR_INSERT.idUsuario = L.idUsuarioInclusao
                WHERE 
                    L.isAtivo = 1
                ORDER BY L.idLogAcesso ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                while (dados.Read())
                {
                    LogAcesso LogAcesso = new LogAcesso();
                    Usuario usuarioInclusao = new Usuario();
                    usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());

                    LogAcesso.setId(int.Parse(dados["Id"].ToString()));
                    LogAcesso.HashOperacao = dados["hashOperacao"].ToString().TrimEnd();
                    LogAcesso.SessionUUID = dados["sessionUUID"].ToString().TrimEnd();
                    LogAcesso.IpAcesso = dados["ipAcesso"].ToString().TrimEnd();
                    LogAcesso.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));
                    LogAcesso.setUsuarioCriador(usuarioInclusao);
                    LogAcesso.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));

                    lista.Add(LogAcesso);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar lista de log Acessos", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return lista;
        }
    }
}
