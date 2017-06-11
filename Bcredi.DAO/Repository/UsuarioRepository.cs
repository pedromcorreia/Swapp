using System;
using System.Collections.Generic;
using Dapper;
using System.Text;
using log4net;
using Bcredi.Utils.Database;
using Bcredi.DAO.Models;
using System.Data.SqlClient;
using Bcredi.Utils;
using Bcredi.DAO.Service;
using System.Configuration;
using System.Collections;
using System.Linq;

namespace Bcredi.DAO.Repository
{
    public class UsuarioRepository
    {
        // Referencia para logar as informacoes da classe
        private static readonly ILog log = LogManager.GetLogger(typeof(UsuarioRepository));

        static string ambiente = ConfigurationManager.ConnectionStrings["Connection-Swapp"].ConnectionString;
        static string strConexao = ConfigurationManager.ConnectionStrings[ambiente].ConnectionString;
        private DBUtil dbUtil = new DBUtil();

        public List<Usuario> getDescriptionPartial(string texto)
        {
            List<Usuario> lista = new List<Usuario>();

            string sql = @" select U.idUsuario, U.nome, U.isAtivo, U.login, U.email, 
            usuIns.idUsuario as idUsuarioInclusao,
            usuIns.nome as nomeInclusao, 
            CONVERT(VARCHAR(10), U.dataInclusao, 103) + ' ' + convert(VARCHAR(8), U.dataInclusao, 14) as dataInclusao,
            usuUpd.idUsuario as idUsuarioAtualizacao,
            usuUpd.nome as nomeAtualizacao,
            CONVERT(VARCHAR(10), U.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), U.dataAtualizacao, 14) as dataAtualizacao
            from {0}.dbo.USUARIO U

                LEFT JOIN {0}.dbo.USUARIO usuIns ON usuIns.idUsuario = U.idUsuarioInclusao
                LEFT JOIN {0}.dbo.USUARIO usuUpd ON usuUpd.idUsuario = U.idUsuarioAtualizacao
            WHERE upper(U.nome) like upper('%" + texto + "%') AND U.tipo = 'USUARIO' ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                while (dados.Read())
                {
                    Usuario idUsuarioInclusao = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                    {
                        idUsuarioInclusao = idUsuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());
                    }

                    Usuario usuarioAtualizador = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                    {
                        usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                    }

                    Usuario usuario = new Usuario();
                    usuario.setId(int.Parse(dados["idUsuario"].ToString()));
                    usuario.setNome(dados["nome"].ToString());
                    usuario.setEmail(dados["email"].ToString());
                    usuario.setLogin(dados["login"].ToString());
                    usuario.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));

                    if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                    {
                        usuario.setUsuarioCriador(idUsuarioInclusao);
                        usuario.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                    }

                    usuario.setUsuarioAtualizador(usuarioAtualizador);
                    if (!string.IsNullOrEmpty(dados["dataAtualizacao"].ToString()))
                    {
                        usuario.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                    }

                    lista.Add(usuario);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar dados do usuário", ex);
            }
            finally
            {

                dbUtil.closeConnection(conexao);
            }

            return lista;
        }

        public Usuario setUsuarioSalvar(Usuario usuario)
        {
            int RegistroInserido = 0;
            SqlConnection conexao = null;
            StringBuilder sql = new StringBuilder()

            .Append("  INSERT INTO {1}.dbo[USUARIO]   ")
            .Append("([nome] ,[login],[email], [password], [isAtivo], [dataInclusao], [dataExpiracaoSenha], [isSenhaTemporaria], ")
            .Append("[idSetor], [idPerfil], [idResponsavelDireto], [endereco], [fone], [cpf_cnpj])")
            .Append(" values(")
            .Append("@nome")
            .Append(",")
            .Append("@login")
            .Append(",")
            .Append("@email")
            .Append(",")
            .Append("@password")
            .Append(",")
            .Append("1")
            .Append(",")
            .Append("getDate()")
            .Append(",")
            .Append("@DataExpiracaoSenha")
            .Append(",")
            .Append("1")
            .Append(",")
            .Append("1")
            .Append(",")
            .Append("1")
            .Append(",")
            .Append("1")
            .Append(",")
            .Append("@endereco")
            .Append(",")
            .Append("@fone")
            .Append(",")
            .Append("@cpf_cnpj")
            .Append(")");

            try
            {
                conexao = dbUtil.openConnection();
                SqlCommand sqlCommand = new SqlCommand(string.Format(sql.ToString(), Core.BcrediDB, Core.SecurityDB), conexao);

                //SqlParameter parametroIdUsuarioInclusao = new SqlParameter("@idUsuarioInclusao", System.Data.SqlDbType.Int);
                //parametroIdUsuarioInclusao.Value = Core.UsuarioAtual.Id;
                string newpassword = usuario.Password;
                string cripto = usuario.Criptograf;
                sqlCommand.Parameters.Add("@nome", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@nome"].Value = usuario.Nome;

                sqlCommand.Parameters.Add("@login", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@login"].Value = usuario.Email;

                sqlCommand.Parameters.Add("@email", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@email"].Value = usuario.Email;

                sqlCommand.Parameters.Add("@password", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@password"].Value = cripto;

                sqlCommand.Parameters.Add("@DataExpiracaoSenha", System.Data.SqlDbType.DateTime);
                sqlCommand.Parameters["@DataExpiracaoSenha"].Value = usuario.DataExpiracaoSenha;

                sqlCommand.Parameters.Add("@endereco", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@endereco"].Value = usuario.Endereco;

                sqlCommand.Parameters.Add("@fone", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@fone"].Value = usuario.Fone;

                sqlCommand.Parameters.Add("@cpf_cnpj", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@cpf_cnpj"].Value = usuario.Cpf_cnpj;

                //sqlCommand.Parameters.Add(parametroIdUsuarioInclusao);

                RegistroInserido = dbUtil.executeQuery(sqlCommand);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar usuário", ex);
            }

            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return usuario;
        }

        public Usuario getUsuarioById(int idUsuario)
        {
            Usuario usuario = new Usuario();

            string sql = @"
            SELECT U.idUsuario, U.nome, U.login, U.email, U.password,
            U.isAtivo,             
            usuIns.idUsuario as idUsuarioInclusao,
            usuIns.nome as nomeInclusao, 
            CONVERT(VARCHAR(10), U.dataInclusao, 103) + ' ' + convert(VARCHAR(8), U.dataInclusao, 14) as dataInclusao,
            usuUpd.idUsuario as idUsuarioAtualizacao,
            usuUpd.nome as nomeAtualizacao,
            CONVERT(VARCHAR(10), U.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), U.dataAtualizacao, 14) as dataAtualizacao
            FROM {0}.dbo.USUARIO U
                LEFT JOIN {0}.dbo.USUARIO usuIns ON usuIns.idUsuario = U.idUsuarioInclusao
                LEFT JOIN {0}.dbo.USUARIO usuUpd ON usuUpd.idUsuario = U.idUsuarioAtualizacao
             WHERE U.idUsuario = @idUsuario AND U.tipo = 'USUARIO' ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramIdUsuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
                paramIdUsuario.Value = idUsuario;

                sqlCommand.Parameters.Add(paramIdUsuario);

                #endregion [PARAMETROS]

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                if (dados.Read())
                {
                    Usuario usuarioInclusao = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                    {
                        usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());
                    }

                    Usuario usuarioAtualizador = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                    {
                        usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                    }

                    usuario.setId(int.Parse(dados["idUsuario"].ToString()));
                    usuario.Nome = dados["nome"].ToString();
                    usuario.Login = dados["login"].ToString();
                    usuario.Email = dados["email"].ToString();
                    usuario.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));

                    if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                    {
                        usuario.setUsuarioCriador(usuarioInclusao);
                        usuario.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                    }

                    if (!string.IsNullOrEmpty(dados["dataAtualizacao"].ToString()))
                    {
                        usuario.setUsuarioAtualizador(usuarioAtualizador);
                        usuario.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar dados do usuário", ex);
            }
            finally
            {

                dbUtil.closeConnection(conexao);
            }

            return usuario;
        }

        public Usuario getUsuarioAtivoById(int idUsuario, bool boolIsAtivo)
        {
            Usuario usuario = new Usuario();
            //Atualizar aqui
            string sql = @"
            SELECT U.idUsuario, U.nome, U.login, U.email, U.password,
            U.isAtivo,             
            usuIns.idUsuario as idUsuarioInclusao,
            usuIns.nome as nomeInclusao, 
            CONVERT(VARCHAR(10), U.dataInclusao, 103) + ' ' + convert(VARCHAR(8), U.dataInclusao, 14) as dataInclusao,
            usuUpd.idUsuario as idUsuarioAtualizacao,
            usuUpd.nome as nomeAtualizacao,
            CONVERT(VARCHAR(10), U.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), U.dataAtualizacao, 14) as dataAtualizacao
            FROM {0}.dbo.USUARIO U
                LEFT JOIN {0}.dbo.USUARIO usuIns ON usuIns.idUsuario = U.idUsuarioInclusao
                LEFT JOIN {0}.dbo.USUARIO usuUpd ON usuUpd.idUsuario = U.idUsuarioAtualizacao
             WHERE U.idUsuario = @idUsuario AND U.tipo = 'USUARIO' ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramIdUsuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
                paramIdUsuario.Value = idUsuario;

                sqlCommand.Parameters.Add(paramIdUsuario);

                #endregion [PARAMETROS]

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                if (dados.Read())
                {
                    Usuario usuarioInclusao = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                    {
                        usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());
                    }

                    Usuario usuarioAtualizador = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                    {
                        usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                    }

                    usuario.setId(int.Parse(dados["idUsuario"].ToString()));
                    usuario.Nome = dados["nome"].ToString();
                    usuario.Login = dados["login"].ToString();
                    usuario.Email = dados["email"].ToString();
                    usuario.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));

                    if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                    {
                        usuario.setUsuarioCriador(usuarioInclusao);
                        usuario.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                    }

                    if (!string.IsNullOrEmpty(dados["dataAtualizacao"].ToString()))
                    {
                        usuario.setUsuarioAtualizador(usuarioAtualizador);
                        usuario.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar status do usuário", ex);
            }
            finally
            {

                dbUtil.closeConnection(conexao);
            }

            return usuario;
        }

        public bool IsLoginOrEmailExist(string login, string email)
        {
            bool IsEmailorLoginUsuarioExistente = false;

            string sql = @"
            SELECT 1
            FROM {0}.dbo.USUARIO U                
             WHERE U.login = @login
            AND U.email = @email";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramLogin = new SqlParameter("@login", System.Data.SqlDbType.VarChar);
                paramLogin.Value = login;

                SqlParameter paramemail = new SqlParameter("@email", System.Data.SqlDbType.VarChar);
                paramemail.Value = email;

                sqlCommand.Parameters.Add(paramemail);
                sqlCommand.Parameters.Add(paramLogin);

                #endregion [PARAMETROS]


                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                if (dados.Read())
                {
                    IsEmailorLoginUsuarioExistente = false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar existência de usuário", ex);
            }
            finally
            {

                dbUtil.closeConnection(conexao);
            }

            return IsEmailorLoginUsuarioExistente;
        }

        /// <summary>
        /// Recuperar a lista de usuarios por Hierarquia
        /// </summary>
        /// <param name="idHierarquia">Código da hierarquia</param>
        /// <returns>lista de usuarios por Hierarquia</returns>
        public List<Usuario> getUsuariosByHierarquia(int idHierarquia)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            string sql = @"
            SELECT U.idUsuario, U.nome, U.login, U.email, U.password,
            U.isAtivo, U.idPerfil,
            usuIns.idUsuario as idUsuarioInclusao,
            usuIns.nome as nomeInclusao, 
            CONVERT(VARCHAR(10), U.dataInclusao, 103) + ' ' + convert(VARCHAR(8), U.dataInclusao, 14) as dataInclusao,
            usuUpd.idUsuario as idUsuarioAtualizacao,
            usuUpd.nome as nomeAtualizacao,
            CONVERT(VARCHAR(10), U.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), U.dataAtualizacao, 14) as dataAtualizacao
            FROM {0}.dbo.USUARIO U                
                LEFT JOIN {0}.dbo.USUARIO usuIns ON usuIns.idUsuario = U.idUsuarioInclusao
                LEFT JOIN {0}.dbo.USUARIO usuUpd ON usuUpd.idUsuario = U.idUsuarioAtualizacao
             WHERE U.idPerfil = @idHierarquia AND U.tipo = 'USUARIO' 
            ORDER BY U.nome
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramIdHierarquia = new SqlParameter("@idHierarquia", System.Data.SqlDbType.Int);
                paramIdHierarquia.Value = idHierarquia;

                sqlCommand.Parameters.Add(paramIdHierarquia);

                #endregion [PARAMETROS]

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                while (dados.Read())
                {
                    Usuario usuario = new Usuario();

                    Usuario usuarioInclusao = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                    {
                        usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());
                    }

                    Usuario usuarioAtualizador = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                    {
                        usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                    }

                    usuario.setId(int.Parse(dados["idUsuario"].ToString()));
                    usuario.Nome = dados["nome"].ToString();
                    usuario.Login = dados["login"].ToString();
                    usuario.Email = dados["email"].ToString();
                    usuario.IdPerfil = int.Parse(dados["idPerfil"].ToString());
                    usuario.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));

                    if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                    {
                        usuario.setUsuarioCriador(usuarioInclusao);
                        usuario.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                    }

                    if (!string.IsNullOrEmpty(dados["dataAtualizacao"].ToString()))
                    {
                        usuario.setUsuarioAtualizador(usuarioAtualizador);
                        usuario.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                    }

                    listaUsuarios.Add(usuario);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar lista de usuários", ex);
            }
            finally
            {

                dbUtil.closeConnection(conexao);
            }

            return listaUsuarios;
        }

        /// <summary>
        /// Recuperar a lista de usuarios subordinados
        /// </summary>
        /// <param name="idSuperiorHierarquico">Codigo do superior hierquico</param>
        /// <returns>Lista de usuarios subordinados</returns>
        public List<Usuario> getUsuariosBySuperiorHierarquico(int idSuperiorHierarquico)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            string sql = @"
            SELECT U.idUsuario, U.nome, U.login, U.email, U.password,
            U.isAtivo, U.idPerfil,
            usuIns.idUsuario as idUsuarioInclusao,
            usuIns.nome as nomeInclusao, 
            CONVERT(VARCHAR(10), U.dataInclusao, 103) + ' ' + convert(VARCHAR(8), U.dataInclusao, 14) as dataInclusao,
            usuUpd.idUsuario as idUsuarioAtualizacao,
            usuUpd.nome as nomeAtualizacao,
            CONVERT(VARCHAR(10), U.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), U.dataAtualizacao, 14) as dataAtualizacao
            FROM {0}.dbo.USUARIO U                
                LEFT JOIN {0}.dbo.USUARIO usuIns ON usuIns.idUsuario = U.idUsuarioInclusao
                LEFT JOIN {0}.dbo.USUARIO usuUpd ON usuUpd.idUsuario = U.idUsuarioAtualizacao
             WHERE U.isAtivo = 1 and U.idResponsavelDireto = @idSuperiorHierarquico 
             ORDER BY U.nome";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramIdSuperiorHierarquico = new SqlParameter("@idSuperiorHierarquico", System.Data.SqlDbType.Int);
                paramIdSuperiorHierarquico.Value = idSuperiorHierarquico;

                sqlCommand.Parameters.Add(paramIdSuperiorHierarquico);

                #endregion [PARAMETROS]

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                while (dados.Read())
                {
                    Usuario usuario = new Usuario();

                    Usuario usuarioInclusao = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                    {
                        usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());
                    }

                    Usuario usuarioAtualizador = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                    {
                        usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                    }

                    usuario.setId(int.Parse(dados["idUsuario"].ToString()));
                    usuario.Nome = dados["nome"].ToString();
                    usuario.Login = dados["login"].ToString();
                    usuario.Email = dados["email"].ToString();
                    usuario.IdPerfil = int.Parse(dados["idPerfil"].ToString());
                    usuario.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));

                    if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                    {
                        usuario.setUsuarioCriador(usuarioInclusao);
                        usuario.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                    }

                    if (!string.IsNullOrEmpty(dados["dataAtualizacao"].ToString()))
                    {
                        usuario.setUsuarioAtualizador(usuarioAtualizador);
                        usuario.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                    }

                    listaUsuarios.Add(usuario);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar lista de usuários por hierarquia", ex);
            }
            finally
            {

                dbUtil.closeConnection(conexao);
            }
            return listaUsuarios;
        }

        public List<Usuario> getAll(bool isAtivo = true)
        {
            List<Usuario> lista = new List<Usuario>();

            string sql = @"
            select U.idUsuario, U.nome, U.isAtivo,
            usuIns.idUsuario as idUsuarioInclusao,
            usuIns.nome as nomeInclusao, 
            CONVERT(VARCHAR(10), U.dataInclusao, 103) + ' ' + convert(VARCHAR(8), U.dataInclusao, 14) as dataInclusao,
            usuUpd.idUsuario as idUsuarioAtualizacao,
            usuUpd.nome as nomeAtualizacao,
            CONVERT(VARCHAR(10), U.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), U.dataAtualizacao, 14) as dataAtualizacao
            from {0}.dbo.USUARIO U
                JOIN {0}.dbo.USUARIO usuIns ON usuIns.idUsuario = U.idUsuarioInclusao
                LEFT JOIN {0}.dbo.USUARIO usuUpd ON usuUpd.idUsuario = U.idUsuarioAtualizacao                
             WHERE 1=1 
            ";

            if (isAtivo)
            {
                sql += " AND U.isAtivo = 1 AND U.tipo = 'USUARIO'  ";
            }
            else
            {
                sql += "AND U.isAtivo = 0  AND U.tipo = 'USUARIO' ";
            }

            sql += " ORDER BY U.nome ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                while (dados.Read())
                {
                    Usuario usuario = new Usuario();

                    Usuario usuarioInclusao = new Usuario();
                    usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());
                    usuario.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));

                    Usuario usuarioAtualizador = new Usuario();
                    if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                    {
                        usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                        usuario.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                    }

                    usuario.Id = int.Parse(dados["idUsuario"].ToString());
                    usuario.Nome = dados["nome"].ToString();
                    usuario.IsAtivo = dados["isAtivo"].ToString().Equals(Constantes.ATIVO);
                    usuario.UsuarioCriador = usuarioInclusao;
                    usuario.UsuarioAtualizador = usuarioAtualizador;
                    lista.Add(usuario);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar lista de usuários usuários", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return lista;
        }
        public Usuario getUsuarioByLoginSenha(string login, string password)
        {
            Usuario usuario = new Usuario();

            string sql = @"
                            
                            SELECT U.idUsuario, U.nome, U.login, U.email, U.password, U.guidUsuario, U.chaveDocumentoAssinatura,
                            U.isAtivo, U.tokenResetSenha, U.ipUltimoAcesso, U.cpf_cnpj as cpf_cnpj, U.dataNascimento as dataNascimento, 
                            usuIns.idUsuario as idUsuarioInclusao,
                            usuIns.nome as nomeInclusao, 
                            CONVERT(VARCHAR(10), U.dataInclusao, 103) + ' ' + convert(VARCHAR(8), U.dataInclusao, 14) as dataInclusao,
                            usuUpd.idUsuario as idUsuarioAtualizacao,
                            usuUpd.nome as nomeAtualizacao,
                            CONVERT(VARCHAR(10), U.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), U.dataAtualizacao, 14) as dataAtualizacao,            
                            CONVERT(VARCHAR(10), U.dataUltimoAcesso, 103) + ' ' + CONVERT(VARCHAR(8), U.dataUltimoAcesso, 108) as dataUltimoAcesso
                            FROM USUARIO U
                            LEFT JOIN USUARIO usuIns ON usuIns.idUsuario = U.idUsuarioInclusao
                            LEFT JOIN USUARIO usuUpd ON usuUpd.idUsuario = U.idUsuarioAtualizacao
                            WHERE  U.login = @login AND U.isAtivo = 1
                ";
            

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramLogin = new SqlParameter("@login", System.Data.SqlDbType.VarChar);
                paramLogin.Value = login;

                sqlCommand.Parameters.Add(paramLogin);

                #endregion [PARAMETROS]

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                if (dados.HasRows)
                {
                    if (dados.Read())
                    {
                        if (!Bcredi.Utils.Utils.Decryption(dados["password"].ToString()).Equals(password))
                        {
                            // senha inválida, retornar null
                            return null;
                        }

                        Usuario usuarioInclusao = new Usuario();
                        if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                        {
                            usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());
                        }

                        Usuario usuarioAtualizador = new Usuario();
                        if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                        {
                            usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                        }

                        usuario.setId(int.Parse(dados["idUsuario"].ToString()));
                        usuario.Login = dados["login"].ToString();
                        usuario.Nome = dados["nome"].ToString();
                        //usuario.DataAniversario = dados["nome"].ToString();
                        usuario.Email = dados["email"].ToString();
                        usuario.Password = dados["password"].ToString();
                        //usuario.IdPerfil = int.Parse(dados["idPerfil"].ToString());
                        usuario.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));
                        usuario.setUsuarioCriador(usuarioInclusao);
                        usuario.TokenResetSenha = dados["tokenResetSenha"].ToString();
                        usuario.DataAniversario = (Convert.ToDateTime(dados["dataNascimento"]));
                        usuario.GuidUsuario = dados["guidUsuario"].ToString();
                        usuario.Cpf_cnpj = dados["cpf_cnpj"].ToString();
                        usuario.ChaveDocumentoAssinatura = dados["chaveDocumentoAssinatura"].ToString();
                        if (!string.IsNullOrEmpty(dados["dataInclusao"].ToString()))
                        {
                            usuario.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                            usuario.setUsuarioAtualizador(usuarioAtualizador);
                        }

                        if (!string.IsNullOrEmpty(dados["dataAtualizacao"].ToString()))
                        {
                            usuario.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                        }

                        usuario.DataUltimoAcesso = dados["dataUltimoAcesso"].ToString();
                        usuario.IpUltimoAcesso = dados["ipUltimoAcesso"].ToString();

                    }
                }
                else
                {
                    // Registro não encontrado, retornar sempre null
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar dados do usuário", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return usuario;
        }

        public int updateSenha(string login, string senha)
        {
            int linhasAlteradas = 0;

            string sql = @"

            UPDATE {0}.dbo.USUARIO 
            SET password = @password
                , isSenhaTemporaria = null
                , dataExpiracaoSenha = null
            WHERE login = @login

            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramPassword = new SqlParameter("@password", System.Data.SqlDbType.VarChar);
                paramPassword.Value = senha;

                SqlParameter paramLogin = new SqlParameter("@login", System.Data.SqlDbType.VarChar);
                paramLogin.Value = login;

                sqlCommand.Parameters.Add(paramPassword);
                sqlCommand.Parameters.Add(paramLogin);

                #endregion [PARAMETROS]

                dbUtil.executeNonQuery(sqlCommand);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar senha do usuário", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return linhasAlteradas;
        }

        public int insertUsuario(Usuario usuario)
        {
            int idUsuario = 0;
            

            string sql = @"
            INSERT INTO {0}.dbo.[USUARIO]([nome], [email], [login], [fone], [cpf_cnpj], [tokenResetSenha], [isAtivo], [dataInclusao], [password], [cep], 
                                          [dataNascimento],[melhorHorario],[melhorDia],[telefonePreferencial],[guidUsuario])
                                    VALUES(@nome, @email, @login, @telefone, @cpfCnpj, @Token, 0, GETDATE(), @Password, @Cep, 
                                          @DataAniversario, @melhorHorario, @melhorDia, @telefonePreferencial, @guidusuario);
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramNome = new SqlParameter("@nome", System.Data.SqlDbType.VarChar);
                paramNome.Value = usuario.Nome;

                SqlParameter paramLogin = new SqlParameter("@login", System.Data.SqlDbType.VarChar);
                paramLogin.Value = usuario.Login;

                SqlParameter paramEmail = new SqlParameter("@email", System.Data.SqlDbType.VarChar);
                paramEmail.Value = usuario.Email;

                SqlParameter paramTelefone = new SqlParameter("@telefone", System.Data.SqlDbType.VarChar);
                paramTelefone.Value = usuario.Telefone;

                SqlParameter paramCpfCnpj = new SqlParameter("@cpfCnpj", System.Data.SqlDbType.VarChar);
                paramCpfCnpj.Value = usuario.Cpf_cnpj;

                SqlParameter paramToken = new SqlParameter("@Token", System.Data.SqlDbType.VarChar);
                paramToken.Value = usuario.TokenResetSenha;

                SqlParameter paramPassword = new SqlParameter("@Password", System.Data.SqlDbType.VarChar);
                paramPassword.Value = usuario.Password;

                SqlParameter paramCep = new SqlParameter("@Cep", System.Data.SqlDbType.VarChar);
                paramCep.Value = usuario.Cep;

                SqlParameter paramDataAniversario = new SqlParameter("@DataAniversario", System.Data.SqlDbType.DateTime);
                paramDataAniversario.Value = usuario.DataAniversario;
               
                SqlParameter paramGuidUsuario = new SqlParameter("@guidusuario", System.Data.SqlDbType.VarChar);
                paramGuidUsuario.Value = usuario.GuidUsuario;

                sqlCommand.Parameters.Add(paramNome);
                sqlCommand.Parameters.Add(paramLogin);
                sqlCommand.Parameters.Add(paramEmail);
                sqlCommand.Parameters.Add(paramTelefone);
                sqlCommand.Parameters.Add(paramCpfCnpj);
                sqlCommand.Parameters.Add(paramToken);
                sqlCommand.Parameters.Add(paramPassword);
                sqlCommand.Parameters.Add(paramCep);
                sqlCommand.Parameters.Add(paramDataAniversario);
                sqlCommand.Parameters.Add(paramGuidUsuario);


                #endregion [PARAMETROS]

                dbUtil.executeNonQuery(sqlCommand);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar usuário", ex);
            }
            finally
            {

                dbUtil.closeConnection(conexao);
            }

            return idUsuario;
        }

        public int updateUsuario(Usuario usuario)
        {
            int idUsuario = 0;
            return idUsuario;
        }

        //TO DO - Pedro olhar o codigo de adicao ao usuario
        public int saveOrUpdate(Usuario usuario)
        {
            int idUsuario = 0;

            if (usuario.Id > 0)
            {
                updateUsuario(usuario);
            }
            else
            {
                insertUsuario(usuario);
            }

            return idUsuario;
        }

        /// <summary>
        /// Verifica se token ainda é valido
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Retorna true se data de expiração de senha ainda é válida</returns>
        public bool isValidDataExpiracaoSenha(string login)
        {

            string sql = @"
            
            SELECT 
                idUsuario
            FROM {0}.dbo.USUARIO
            WHERE 
                [dataExpiracaoSenha] IS NOT NULL
                AND GETDATE() > [dataExpiracaoSenha]
                AND login = @login;
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramLogin = new SqlParameter("@login", System.Data.SqlDbType.VarChar);
                paramLogin.Value = login;

                sqlCommand.Parameters.Add(paramLogin);

                #endregion [PARAMETROS]

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                if (dados.HasRows)
                {
                    // Expirou
                    return false;
                }
                else
                {
                    // Ainda é válida
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar data de expiração de senha do usuário", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

        }

        //TO DO BLOQUEAR LOGIN
        /*public int BloqueiaLogin(string login)
        {
            int DataexpiradaLogin = 0;

            string sql = @"
            
            UPDATE {0}.dbo.USUARIO 
            SET isAtivo = 0
            WHERE GETDATE() > [dataExpiracaoSenha]
            AND login = @login;

            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();
                
                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                //SqlParameter paramdataExpiracaoSenha = new SqlParameter("@dataSenhaExpirada", System.Data.SqlDbType.VarChar);
                //paramdataExpiracaoSenha.Value = dataSenhaExpirada;

                SqlParameter paramLogin = new SqlParameter("@login", System.Data.SqlDbType.VarChar);
                paramLogin.Value = login;

                //sqlCommand.Parameters.Add(paramdataExpiracaoSenha);
                sqlCommand.Parameters.Add(paramLogin);

                #endregion [PARAMETROS]

                dbUtil.executeNonQuery(sqlCommand);

            }
            catch (Exception excessao)
            {
                //Marcelo: Tratar essa excessao com logger
            }
            finally
            {

                dbUtil.closeConnection(conexao);
            }

            return DataexpiradaLogin;
        }
        */

        public Usuario getUsuarioByEmail(string email)
        {
            Usuario usuario = new Usuario();

            string sql = @"
            SELECT U.idUsuario, U.nome, U.login, U.email,
            U.isAtivo,             
            usuIns.idUsuario as idUsuarioInclusao,
            usuIns.nome as nomeInclusao, 
            CONVERT(VARCHAR(10), U.dataInclusao, 103) + ' ' + convert(VARCHAR(8), U.dataInclusao, 14) as dataInclusao,
            usuUpd.idUsuario as idUsuarioAtualizacao,
            usuUpd.nome as nomeAtualizacao,
            CONVERT(VARCHAR(10), U.dataAtualizacao, 103) + ' ' + convert(VARCHAR(8), U.dataAtualizacao, 14) as dataAtualizacao
            FROM {0}.dbo.USUARIO U
                LEFT JOIN {0}.dbo.USUARIO usuIns ON usuIns.idUsuario = U.idUsuarioInclusao
                LEFT JOIN {0}.dbo.USUARIO usuUpd ON usuUpd.idUsuario = U.idUsuarioAtualizacao
             WHERE U.login = @login and U.isAtivo = 1                    
                ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramLogin = new SqlParameter("@login", System.Data.SqlDbType.VarChar);
                paramLogin.Value = email;

                sqlCommand.Parameters.Add(paramLogin);

                #endregion [PARAMETROS]

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                if (dados.HasRows)
                {
                    if (dados.Read())
                    {
                        Usuario usuarioInclusao = new Usuario();
                        if (!string.IsNullOrEmpty(dados["idUsuarioInclusao"].ToString()))
                        {
                            usuarioInclusao = usuarioInclusao.buildUsuario(Int32.Parse(dados["idUsuarioInclusao"].ToString()), dados["nomeInclusao"].ToString());
                        }

                        Usuario usuarioAtualizador = new Usuario();
                        if (!string.IsNullOrEmpty(dados["idUsuarioAtualizacao"].ToString()))
                        {
                            usuarioAtualizador = usuarioAtualizador.buildUsuario(Int32.Parse(dados["idUsuarioAtualizacao"].ToString()), dados["nomeAtualizacao"].ToString());
                        }

                        usuario.setId(int.Parse(dados["idUsuario"].ToString()));
                        usuario.Login = dados["login"].ToString();
                        usuario.Nome = dados["nome"].ToString();
                        usuario.Email = dados["email"].ToString();
                        usuario.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));
                        usuario.setUsuarioCriador(usuarioInclusao);

                        if (!string.IsNullOrEmpty(dados["dataInclusao"].ToString()))
                        {
                            usuario.setDataCriacao(Convert.ToDateTime(dados["dataInclusao"]));
                            usuario.setUsuarioAtualizador(usuarioAtualizador);
                        }

                        if (!string.IsNullOrEmpty(dados["dataAtualizacao"].ToString()))
                        {
                            usuario.setDataAtualizacao(Convert.ToDateTime(dados["dataAtualizacao"]));
                        }

                    }
                }
                else
                {
                    // Registro não encontrado, retornar sempre null
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar dados do usuário", ex);
            }
            finally
            {

                dbUtil.closeConnection(conexao);
            }

            return usuario;
        }

        Usuario CreateUsuario(string nome, string login, string email, string password, int usuarioinclusao)
        {
            throw new NotImplementedException();
        }

        public static implicit operator UsuarioRepository(UsuarioService v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Atualizar a data e o último acesso do usuário
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="ip"></param>
        /// 
        public void atualizarUltimoAcesso(int idUsuario, string ip)
        {

            string sql = @"
            UPDATE {0}.dbo.USUARIO
            SET dataUltimoAcesso=GETDATE(), ipUltimoAcesso = @ipUltimoAcesso
            where idUsuario =@idUsuario 
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramIp = new SqlParameter("@ipUltimoAcesso", System.Data.SqlDbType.VarChar);
                paramIp.Value = ip;

                SqlParameter paramIdUsuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
                paramIdUsuario.Value = idUsuario;

                sqlCommand.Parameters.Add(paramIp);
                sqlCommand.Parameters.Add(paramIdUsuario);

                #endregion [PARAMETROS]

                dbUtil.executeNonQuery(sqlCommand);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar data de último acesso do usuário", ex);
            }
            finally
            {

                dbUtil.closeConnection(conexao);
            }
        }

        /// <summary>
        /// Recupera informações do usuário por meio de token que será recebido no seguinte padrão:
        /// localhost:50380/OneSystem/RedefinirSenha?token=456EABA5-94D7-4ADB-9B6F-852568F9C3C7
        /// </summary>
        /// <param name="token">"456EABA5-94D7-4ADB-9B6F-852568F9C3C7";</param>
        /// <returns>Usuario</returns>
        /// <author>Rodrigo Bortolon</author>
        public Usuario getUsuarioByTokenResetSenha(string token)
        {
            Usuario usuario = new Usuario();

            string sql = @"
                SELECT
                    U.idUsuario
                    , U.nome
                    , U.login
                    , U.email
                    , U.isAtivo
                FROM {0}.dbo.USUARIO U
                WHERE
	                U.tokenResetSenha = @token
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);

                #region [PARAMETROS]

                SqlParameter paramToken = new SqlParameter("@token", System.Data.SqlDbType.VarChar);
                paramToken.Value = token;

                sqlCommand.Parameters.Add(paramToken);

                #endregion [PARAMETROS]

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                if (dados.HasRows)
                {
                    if (dados.Read())
                    {
                        usuario.setId(int.Parse(dados["idUsuario"].ToString()));
                        usuario.Login = dados["login"].ToString();
                        usuario.Nome = dados["nome"].ToString();
                        usuario.Email = dados["email"].ToString();
                        usuario.setAtivo(dados["isAtivo"].ToString().Equals(Constantes.ATIVO));
                    }
                }
                else
                {
                    // Registro não encontrado, retornar sempre null
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar dados do usuário", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return usuario;
        }

        public int iniciaProcessoTrocaDeSenha(int idUsuario, string token, string codSeguranca)
        {
            int linhasAlteradas = 0;

            string sql = @"
                UPDATE {0}.dbo.USUARIO SET 
                    tokenResetSenha = @token 
                    , password = @codSeguranca
                    , [dataExpiracaoSenha] = DATEADD(MINUTE, 30, GETDATE())
                    , isSenhaTemporaria = 1
                WHERE idUsuario = @idUsuario
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramIdUsuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.VarChar);
                paramIdUsuario.Value = idUsuario;

                SqlParameter paramCodSeguranca = new SqlParameter("@codSeguranca", System.Data.SqlDbType.VarChar);
                paramCodSeguranca.Value = codSeguranca;

                SqlParameter paramToken = new SqlParameter("@token", System.Data.SqlDbType.VarChar);
                paramToken.Value = token;

                sqlCommand.Parameters.Add(paramIdUsuario);
                sqlCommand.Parameters.Add(paramCodSeguranca);
                sqlCommand.Parameters.Add(paramToken);

                #endregion [PARAMETROS]

                dbUtil.executeNonQuery(sqlCommand);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar dados sobre o início de processo de troca de senha do usuário", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return linhasAlteradas;
        }

        public int AtivarCadastroUsuarioRepository(string login)
        {
            int executou = 0;

            string sql = @"
            UPDATE {1}.dbo.USUARIO 
            SET [isAtivo] = 1
                , isSenhaTemporaria = null
                , dataExpiracaoSenha = null
            WHERE login = @login
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramLogin = new SqlParameter("@login", System.Data.SqlDbType.VarChar);
                paramLogin.Value = login;

                sqlCommand.Parameters.Add(paramLogin);

                #endregion [PARAMETROS]

                dbUtil.executeNonQuery(sqlCommand);
                executou = 1;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ativar usuário", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }

            return executou;
        }

        public int getIdUsuarioByLogin(string login)
        {
            int id = 0;
            string sql = @"";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);
                #region [PARAMETROS]

                SqlParameter paramlogin = new SqlParameter("@idUsuario", System.Data.SqlDbType.VarChar);
                paramlogin.Value = login;

                sqlCommand.Parameters.Add(paramlogin);

                #endregion [PARAMETROS]

                dbUtil.executeNonQuery(sqlCommand);

                SqlDataReader dados = dbUtil.getDados(sqlCommand);

                if (dados.Read())
                {

                    id = (int.Parse(dados["idUsuario"].ToString()));

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível recuperar o usuário", ex);
            }
            finally
            {
                dbUtil.closeConnection(conexao);
            }
            return id;
        }

        public VM_UsuarioComplementoDadosBancarios PesquisaUsuarioComplementoDadosBancarios(string guidUsuario)
        {
            var vmUsuarioComplementoDadosBancarios = new VM_UsuarioComplementoDadosBancarios();

            using (var conexaoBD = new SqlConnection(strConexao))
            {
                //var sql = @"SELECT * FROM [USUARIO]                WHERE [guidUsuario] = @Id
                //            SELECT * FROM [AL_USUARIO_COMPLEMENTO] WHERE [guidUsuario] = @Id
                //            SELECT * FROM [AL_DADOS_BANCARIOS]     WHERE [guidUsuario] = @Id";
                var sql = @"SELECT * FROM USUARIO as U
                            LEFT JOIN AL_DADOS_BANCARIOS as D ON u.guidUsuario = d.guidUsuario
                            LEFT JOIN AL_USUARIO_COMPLEMENTO as C ON d.guidUsuario = c.guidUsuario
                            WHERE U.guidUsuario = @Id
                            AND D.idItemGenerico is null";
                using (var resultado = conexaoBD.QueryMultiple(sql, new { Id = guidUsuario }))
                {
                    vmUsuarioComplementoDadosBancarios = resultado.Read<VM_UsuarioComplementoDadosBancarios>().Single();
                }
            }

            return vmUsuarioComplementoDadosBancarios;
        }

        public bool AlterarSenhaUpdate(Usuario usuario)
        {
            var retorno = false;
            try
            {
                using (var conexaoBD = new SqlConnection(strConexao))
                {
                    var atualizarBD = @"UPDATE [USUARIO] 
                                           SET Password = @Password 
                                         WHERE Email = @Email";
                    conexaoBD.Execute(atualizarBD, new
                    {
                        Password = usuario.Password,
                        Email = usuario.Email
                    });
                }
                retorno = true;
            }
            catch (Exception)
            {
                retorno = false;
            }

            return retorno;
        }

        public bool InsertOrUpdateUsuarioComplementoDadosBancariosRepository(VM_UsuarioComplementoDadosBancarios obj)
        {
            //var vmUsuarioComplementoDadosBancarios = new VM_UsuarioComplementoDadosBancarios();

            var retorno = false;
            try
            {
                using (var conexaoBD = new SqlConnection(strConexao))
                {
                    //if (String.IsNullOrEmpty(obj.IdDadosBancarios))
                    if (obj.IdDadosBancarios == Guid.Empty)
                    {
                        var atualizarBD = @"Update dbo.USUARIO Set cep = @cep, fone = @telefone, email = @email, telefonePreferencial = @telefonePreferencial
                                            Where guidUsuario = @guidUsuario";
                        conexaoBD.Execute(atualizarBD, new
                        {
                            cep = obj.CEP,
                            telefone = obj.Fone,
                            email = obj.Email,
                            telefonePreferencial = obj.TelefonePreferencial,
                            guidUsuario = obj.GuidUsuario
                        });
                        conexaoBD.Execute(@"insert dbo.AL_USUARIO_COMPLEMENTO(idUsuario, guidUsuario, endereco, numero, complemento, bairro, cidade, estado, foto, isAtivo)
                                            values ( @idUsuario, @GuidUsuario, @endereco, @numero, @complemento, @bairro, @cidade, @estado, @foto, 1)", obj);
                        conexaoBD.Execute(@"insert dbo.AL_DADOS_BANCARIOS(idUsuario, idDadosBancarios, guidUsuario, nomeBanco, agencia, tipoConta, conta, nomeTitular, isAtivo)
                                            values ( @idUsuario, NEWID(), @guidUsuario, @nomeBanco, @agencia, @tipoConta, @conta, @nomeTitular, 1)", obj);
                    }
                    else
                    {
                        var sqlUpdateUsuario = @"Update dbo.USUARIO Set cep = @cep, fone = @telefone, email = @email, telefonePreferencial = @telefonePreferencial
                                   Where guidUsuario = @guidUsuario";
                        conexaoBD.Execute(sqlUpdateUsuario, new
                        {
                            cep = obj.CEP,
                            telefone = obj.Fone,
                            email = obj.Email,
                            telefonePreferencial = obj.TelefonePreferencial,
                            guidUsuario = obj.GuidUsuario
                        });
                        var sqlUpdateAl_Usuario_Complemento = @"Update dbo.AL_USUARIO_COMPLEMENTO Set endereco = @endereco, numero = @numero, complemento = @complemento, bairro = @bairro, cidade = @cidade, estado = @estado, foto = @foto
                                   Where guidUsuario = @guidUsuario";
                        conexaoBD.Execute(sqlUpdateAl_Usuario_Complemento, new
                        {
                            endereco = obj.Endereco,
                            numero = obj.Numero,
                            complemento = obj.Complemento,
                            bairro = obj.Bairro,
                            cidade = obj.Cidade,
                            estado = obj.Estado,
                            foto = obj.Foto,
                            guidUsuario = obj.GuidUsuario
                        });
                        var sqlUpdateAl_DadosBancarios = @"Update dbo.AL_DADOS_BANCARIOS Set nomeBanco = @nomeBanco, agencia = @agencia, tipoConta = @tipoConta, conta = @conta, nomeTitular = @nomeTitular
                                   Where guidUsuario = @guidUsuario";
                        conexaoBD.Execute(sqlUpdateAl_DadosBancarios, new
                        {
                            nomeBanco = obj.NomeBanco,
                            agencia = obj.Agencia,
                            tipoConta = obj.TipoConta,
                            conta = obj.Conta,
                            nomeTitular = obj.NomeTitular,
                            guidUsuario = obj.GuidUsuario
                        });
                    }
                }
                retorno = true;
            }
            catch (Exception ex)
            {
                retorno = false;
            }

            return retorno;
        }

        public bool UpdateUsuarioAssinatura(string guidUsuario, string guidDocumentoClickSign)
        {
            var retorno = false;

            try
            {
                using (var conexaoBD = new SqlConnection(strConexao))
                {
                    var atualizarBD = @"Update dbo.USUARIO Set indicadorAssinatura = 1, chaveDocumentoAssinatura = @guidDocumentoClickSign
                                            Where guidUsuario = @guidUsuario";
                        conexaoBD.Execute(atualizarBD, new
                        {
                            guidUsuario = guidUsuario,
                            guidDocumentoClickSign = guidDocumentoClickSign
                        });
                 }
                retorno = true;
            }
            catch (Exception ex)
            {
                retorno = false;
            }

            return retorno;
        }

        public bool UpdateUsuarioAssinaturaById(int idUsuario, string guidDocumentoClickSign)
        {
            var retorno = false;

            try
            {
                using (var conexaoBD = new SqlConnection(strConexao))
                {
                    var atualizarBD = @"Update dbo.USUARIO Set indicadorAssinatura = 1, chaveDocumentoAssinatura = @guidDocumentoClickSign
                                            Where idUsuario = @idUsuario";
                    conexaoBD.Execute(atualizarBD, new
                    {
                        idUsuario = idUsuario,
                        guidDocumentoClickSign = guidDocumentoClickSign
                    });
                }
                retorno = true;
            }
            catch (Exception ex)
            {
                retorno = false;
            }

            return retorno;
        }

        public bool UpdateDadosPessoais(VM_DadosPessoais usuario)
        {
            var retorno = false;

            try
            {
                using (var conexaoBD = new SqlConnection(strConexao))
                {
                    var atualizarBD = @"Update dbo.USUARIO_COMPLEMENTO 
                                            SET rg = @rg, orgaoExpedidor = @orgaoExpedidor, dataExpedicaoRg = @dataExpedicaoRg, indicadorConjuge = @indicadorConjuge
                                            WHERE guidUsuario = @guidUsuario";
                    conexaoBD.Execute(atualizarBD, new
                    {
                        guidUsuario = usuario.GuidUsuario,
                        rg = usuario.Rg,
                        orgaoExpedidor = usuario.OrgaoExpedidor,
                        dataExpedicaoRg = usuario.DataExpedicaoRG,
                        indicadorConjuge = usuario.IndicadorConjuge
                    });
                }
                retorno = true;
            }
            catch (Exception ex)
            {
                retorno = false;
            }

            return retorno;
        }


    }
}
