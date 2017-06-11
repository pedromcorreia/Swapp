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
    public class BaseVORepository
    {
        private DBUtil dbUtil = new DBUtil();

        public List<BaseVO> getAll(string tableName, string orderBy = " 1 ", bool isAtivo = true)
        {
            List<BaseVO> lista = new List<BaseVO>();

            StringBuilder sql = new StringBuilder()
            .Append(" SELECT * ")
            .Append(" FROM {0}.dbo.")
            .Append(tableName);

            if (isAtivo)
            {
                sql.Append(" WHERE IsAtivo = 1 ");
            }

            sql.Append(" ORDER BY ")
            .Append(orderBy);

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();
                SqlDataReader dados = dbUtil.getDados(string.Format(sql.ToString(), Core.BcrediDB, Core.SecurityDB), conexao);

                while (dados.Read())
                {
                    BaseVO baseVO = new BaseVO();
                    baseVO.setId(int.Parse(dados["id" + tableName].ToString()));
                    baseVO.setDescricao(dados["descricao"].ToString().TrimEnd());
                    lista.Add(baseVO);
                }
            }
            catch (Exception ex)
            {
                //Marcelo: Tratar essa excessao com logger
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return lista;
        }

        public BaseVO getObjectById(int id, string tableName)
        {

            BaseVO baseVO = new BaseVO();

            string sql = @"
            select id" + tableName + " as Id, descricao, G.isAtivo, ";

            sql += @"
            usuIns.idUsuario as idUsuarioInclusao,
            usuIns.nome as nomeInclusao, 
            CONVERT(VARCHAR(10), G.dataInclusao, 103) + ' ' + convert(VARCHAR(8), G.dataInclusao, 14) as dataInclusao,
            usuUpd.idUsuario as idUsuarioAtualizacao,
            usuUpd.nome as nomeAtualizacao,
            CONVERT(VARCHAR(10), G.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), G.dataAtualizacao, 14) as dataAtualizacao
            from {0}.dbo." + tableName + " G ";

            sql += @"

                JOIN {0}.dbo.USUARIO usuIns ON usuIns.idUsuario = G.idUsuarioInclusao

                LEFT JOIN {0}.dbo.USUARIO usuUpd ON usuUpd.idUsuario = G.idUsuarioAtualizacao
            WHERE id" + tableName + "= @id";


            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql.ToString(), Core.BcrediDB, Core.SecurityDB), conexao);
                SqlParameter parametroId = new SqlParameter("@id", System.Data.SqlDbType.Int);
                parametroId.Value = id;

                sqlCommand.Parameters.Add(parametroId);


                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                while (dados.Read())
                {
                    Usuario usuarioInclusao = new Usuario();
                    usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());

                    Usuario usuarioAtualizador = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                    {
                        usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                    }

                    baseVO.setId(int.Parse(dados["Id"].ToString()));
                    baseVO.setDescricao(dados["descricao"].ToString().TrimEnd());
                    baseVO.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));
                    baseVO.setUsuarioCriador(usuarioInclusao);
                    baseVO.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                    baseVO.setUsuarioAtualizador(usuarioAtualizador);
                    if (!string.IsNullOrEmpty(dados["dataAtualizacao"].ToString()))
                    {
                        baseVO.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                    }
                }
            }
            catch (Exception ex)
            {
                //Marcelo: Tratar essa excessao com logger
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return baseVO;
        }

        public int insertObject(BaseVO objeto, string tableName)
        {
            int chaveGerada = 0;

            StringBuilder sql = new StringBuilder()

            .Append(" INSERT INTO {0}.dbo.")
            .Append(tableName)
            .Append(" (descricao,isAtivo,idUsuarioInclusao,dataInclusao)")
            .Append(" OUTPUT INSERTED.ID" + tableName)
            .Append(" values(")
            .Append("@descricao")
            .Append(",")
            .Append("1,")
            .Append("@idUsuarioInclusao,")
            .Append("getDate()")
            .Append(")");

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();
                SqlCommand sqlCommand = new SqlCommand(string.Format(sql.ToString(), Core.BcrediDB, Core.SecurityDB), conexao);

                SqlParameter parametroIdUsuarioInclusao = new SqlParameter("@idUsuarioInclusao", System.Data.SqlDbType.Int);
                parametroIdUsuarioInclusao.Value = Core.UsuarioAtual.Id;

                sqlCommand.Parameters.Add("@descricao", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@descricao"].Value = objeto.getDescricao().TrimEnd();

                sqlCommand.Parameters.Add(parametroIdUsuarioInclusao);

                chaveGerada = dbUtil.executeQuery(sqlCommand);

            }
            catch (Exception ex)
            {
                //Marcelo: Tratar essa excessao  com logger              
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return chaveGerada;

        }

        public int updateObject(BaseVO objeto, string tableName)
        {
            int linhasAfetadas = 0;

            StringBuilder sql = new StringBuilder()
            .Append(" UPDATE {0}.dbo.")
            .Append(tableName)
            .Append(" SET descricao = ")
            .Append("@descricao,")
            .Append("isAtivo = ")
            .Append("@isAtivo,")
            .Append("idUsuarioAtualizacao = @idUsuarioAtualizacao,dataAtualizacao=getDate()  WHERE ID" + tableName)
            .Append(" = @id");

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();
                SqlCommand sqlCommand = new SqlCommand(string.Format(sql.ToString(), Core.BcrediDB, Core.SecurityDB), conexao);

                SqlParameter parametroId = new SqlParameter("@id", System.Data.SqlDbType.Int);
                parametroId.Value = objeto.getId();

                SqlParameter parametroidUsuarioAtualizacao = new SqlParameter("@idUsuarioAtualizacao", System.Data.SqlDbType.Int);
                parametroidUsuarioAtualizacao.Value = Core.UsuarioAtual.Id;

                sqlCommand.Parameters.Add("@descricao", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@descricao"].Value = objeto.getDescricao().TrimEnd();

                sqlCommand.Parameters.Add("@isAtivo", System.Data.SqlDbType.Bit);
                sqlCommand.Parameters["@isAtivo"].Value = objeto.IsAtivo;

                sqlCommand.Parameters.Add(parametroidUsuarioAtualizacao);
                sqlCommand.Parameters.Add(parametroId);

                linhasAfetadas = dbUtil.executeNonQuery(sqlCommand);
            }
            catch (Exception ex)
            {
                //Marcelo: Tratar essa excessao  com logger              
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return linhasAfetadas;
        }

        public int deleteObject(BaseVO objeto, string tableName)
        {
            int linhasAfetadas = 0;

            StringBuilder sql = new StringBuilder()
            .Append(" UPDATE {0}.dbo.")
            .Append(tableName)
            .Append(" SET isAtivo= ")
            .Append(0)
            .Append(" WHERE ID" + tableName)
            .Append("= @id");

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();
                //Alteracao para Evitar SQLInject
                SqlCommand sqlComando = new SqlCommand(string.Format(sql.ToString(), Core.BcrediDB, Core.SecurityDB), conexao);
                SqlParameter parametroId = new SqlParameter("@id", objeto.getId());
                sqlComando.Parameters.Add(parametroId);

                linhasAfetadas = dbUtil.executeQuery(sqlComando);

            }
            catch (Exception ex)
            {
                //Marcelo: Tratar essa excessao  com logger              
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return linhasAfetadas;
        }

        public List<BaseVO> getDescriptionPartial(string tableName, string texto)
        {
            List<BaseVO> lista = new List<BaseVO>();

            StringBuilder sql = new StringBuilder()
            .Append(" SELECT * ")
            .Append(" FROM {0}.dbo.")
            .Append(tableName)
            .Append(" WHERE upper(descricao) like upper('%")
            .Append(texto)
            .Append("%')");

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql.ToString(), Core.BcrediDB, Core.SecurityDB), conexao);
                sqlCommand.Parameters.Add("@descricao", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@descricao"].Value = texto.TrimEnd();

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                while (dados.Read())
                {
                    BaseVO baseVO = new BaseVO();
                    baseVO.setId(int.Parse(dados["id" + tableName].ToString()));
                    baseVO.setDescricao(dados["descricao"].ToString().TrimEnd());
                    lista.Add(baseVO);
                }
            }
            catch (Exception ex)
            {
                //Marcelo: Tratar essa excessao com logger
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return lista;
        }
        public BaseVO getObjectByDescription(string tableName, string texto)
        {
            BaseVO baseVO = new BaseVO();

            string sql = @"
            select id" + tableName + " as Id, G.descricao, G.isAtivo, ";

            sql += @"
            usuIns.idUsuario as idUsuarioInclusao,
            usuIns.nome as nomeInclusao, 
            CONVERT(VARCHAR(10), G.dataInclusao, 103) + ' ' + convert(VARCHAR(8), G.dataInclusao, 14) as dataInclusao,
            usuUpd.idUsuario as idUsuarioAtualizacao,
            usuUpd.nome as nomeAtualizacao,
            CONVERT(VARCHAR(10), G.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), G.dataAtualizacao, 14) as dataAtualizacao
            from {0}.dbo." + tableName + " G ";

            sql += @"

                JOIN {0}.dbo.USUARIO usuIns ON usuIns.idUsuario = G.idUsuarioInclusao

                LEFT JOIN {0}.dbo.USUARIO usuUpd ON usuUpd.idUsuario = G.idUsuarioAtualizacao
             WHERE upper(descricao) = upper(@descricao)";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql.ToString(), Core.BcrediDB, Core.SecurityDB), conexao);
                sqlCommand.Parameters.Add("@descricao", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@descricao"].Value = texto.TrimEnd();

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                if (dados.Read())
                {
                    Usuario usuarioInclusao = new Usuario();
                    usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());

                    Usuario usuarioAtualizador = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                    {
                        usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                    }

                    baseVO.setId(int.Parse(dados["Id"].ToString()));
                    baseVO.setDescricao(dados["descricao"].ToString().TrimEnd());
                    baseVO.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));
                    baseVO.setUsuarioCriador(usuarioInclusao);
                    baseVO.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                    baseVO.setUsuarioAtualizador(usuarioAtualizador);
                    if (!string.IsNullOrEmpty(dados["dataAtualizacao"].ToString()))
                    {
                        baseVO.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                    }
                }


            }
            catch (Exception ex)
            {
                //Marcelo: Tratar essa excessao com logger
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return baseVO;
        }



        public BaseVO PesquisaCliente(string txtNumeroCliente, int txtCPFCNPJ, int txtContrato)
        {
            BaseVO baseVO = new BaseVO();
            ////TODO FAZER COMUNICACAO BANCO
            string sql = @"
            select id" + txtNumeroCliente + " as Id, G.descricao, G.isAtivo, ";

            //sql += @"
            //usuIns.idUsuario as idUsuarioInclusao,
            //usuIns.nome as nomeInclusao, 
            //CONVERT(VARCHAR(10), G.dataInclusao, 103) + ' ' + convert(VARCHAR(8), G.dataInclusao, 14) as dataInclusao,
            //usuUpd.idUsuario as idUsuarioAtualizacao,
            //usuUpd.nome as nomeAtualizacao,
            //CONVERT(VARCHAR(10), G.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), G.dataAtualizacao, 14) as dataAtualizacao
            //from " + tableName + " G ";

            //sql += @"

            //    JOIN USUARIO usuIns ON usuIns.idUsuario = G.idUsuarioInclusao

            //    LEFT JOIN USUARIO usuUpd ON usuUpd.idUsuario = G.idUsuarioAtualizacao
            // WHERE upper(descricao) = upper(@descricao)";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(sql.ToString(), conexao);
                sqlCommand.Parameters.Add("@descricao", System.Data.SqlDbType.VarChar);
                //sqlCommand.Parameters["@descricao"].Value = texto.TrimEnd();

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                //if (dados.Read())
                //{
                //    Usuario usuarioInclusao = new Usuario();
                //    usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());

                //    Usuario usuarioAtualizador = new Usuario();
                //    if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                //    {
                //        usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                //    }

                //    baseVO.setId(int.Parse(dados["Id"].ToString()));
                //    baseVO.setDescricao(dados["descricao"].ToString().TrimEnd());
                //    baseVO.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));
                //    baseVO.setUsuarioCriador(usuarioInclusao);
                //    baseVO.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                //    baseVO.setUsuarioAtualizador(usuarioAtualizador);
                //    if (!string.IsNullOrEmpty(dados["dataAtualizacao"].ToString()))
                //    {
                //        baseVO.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                //    }
                //}


            }
            catch (Exception ex)
            {
                //Marcelo: Tratar essa excessao com logger
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return baseVO;
        }

    }
}
