using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using log4net;
using Bcredi.Utils.Database;
using Bcredi.DAO.Models;
using System.Data.SqlClient;
using System.Configuration;
using Bcredi.Utils;

namespace Bcredi.DAO.Repository
{
    public class CidadeRepository
    {
        private DBUtil dbUtil = new DBUtil();
        public Cidade GetCidadeById(int id)
        {
            Cidade Cidade = new Cidade();
            Estado Estado = new Estado();

            string sql = @"
                select idCidade as IdCidade, C.descricao as NomeCidade, 
			    E.idEstado as IdEstado, E.descricao as DescricaoEstado, E.sigla as SiglaEstado
                from AL_CIDADE C 
                JOIN AL_ESTADO E ON E.idEstado = C.idEstado
                WHERE C.idCidade = @IdCidade
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();
                SqlCommand sqlCommand = new SqlCommand(string.Format(sql.ToString(), Core.SecurityDB), conexao);
                SqlParameter parametroId = new SqlParameter("@IdCidade", System.Data.SqlDbType.Int);
                parametroId.Value = id;
                sqlCommand.Parameters.Add(parametroId);
                SqlDataReader dados = dbUtil.getDados(sqlCommand);
                if (dados.Read())
                {
                    Cidade.Id = int.Parse(dados["IdCidade"].ToString());
                    Cidade.descricao = dados["NomeCidade"].ToString();
                    Estado.Descricao = dados["DescricaoEstado"].ToString();
                    Estado.Sigla = dados["SiglaEstado"].ToString();
                    Estado.id = int.Parse(dados["IdEstado"].ToString());
                    Cidade.Estado = Estado;
                }
            }
            catch (Exception excessao)
            {
                //Marcelo: Tratar essa excessao com logger
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return Cidade;

        }

        public Cidade GetCidadeByCidadeeEstado(string NomeCidade, int EstadoId)
        {
            Cidade Cidade = new Cidade();
            Estado Estado = new Estado();

            string sql = @"
                select idCidade as IdCidade, C.descricao as NomeCidade, 
                E.idEstado as IdEstado, E.descricao as DescricaoEstado, E.sigla as SiglaEstado
                from AL_CIDADE C 
                JOIN AL_ESTADO E ON E.idEstado = C.idEstado
                WHERE C.descricao = @NomeCidade AND E.idEstado = @EstadoId
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();
                SqlCommand sqlCommand = new SqlCommand(string.Format(sql.ToString(), Core.SecurityDB), conexao);
                
                SqlParameter parametroCidade = new SqlParameter("@NomeCidade", System.Data.SqlDbType.VarChar);
                SqlParameter parametroEstadoId = new SqlParameter("@EstadoId", System.Data.SqlDbType.Int);

                parametroCidade.Value = NomeCidade;
                parametroEstadoId.Value = EstadoId;

                sqlCommand.Parameters.Add(parametroEstadoId);
                sqlCommand.Parameters.Add(parametroCidade);

                SqlDataReader dados = dbUtil.getDados(sqlCommand);
                if (dados.Read())
                {
                    Cidade.Id = int.Parse(dados["IdCidade"].ToString());
                    Cidade.descricao = dados["NomeCidade"].ToString();
                    Estado.Descricao = dados["DescricaoEstado"].ToString();
                    Estado.Sigla = dados["SiglaEstado"].ToString();
                    Estado.id = int.Parse(dados["IdEstado"].ToString());
                    Cidade.Estado = Estado;
                }
                else
                {
                    Estado = GetEstadoById(EstadoId);
                    Cidade.Estado = Estado;
                }
            }
            catch (Exception excessao)
            {
                //Marcelo: Tratar essa excessao com logger
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return Cidade;

        }

        public Estado GetEstadoById(int EstadoId)
        {
            Estado Estado = new Estado();

            string sql = @"
                select sigla, descricao from AL_ESTADO 
                where idEstado = @EstadoId
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();
                SqlCommand sqlCommand = new SqlCommand(string.Format(sql.ToString(), Core.SecurityDB), conexao);

                SqlParameter parametroEstadoId = new SqlParameter("@EstadoId", System.Data.SqlDbType.Int);
                parametroEstadoId.Value = EstadoId;
                sqlCommand.Parameters.Add(parametroEstadoId);

                SqlDataReader dados = dbUtil.getDados(sqlCommand);
                if (dados.Read())
                {
                    Estado.Sigla = dados["sigla"].ToString();
                    Estado.Descricao = (dados["descricao"].ToString());
                }
            }
            catch (Exception excessao)
            {
                //Marcelo: Tratar essa excessao com logger
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return Estado;

        }
    }
}
