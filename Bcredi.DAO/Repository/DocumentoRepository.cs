using System;
using System.Collections.Generic;
using Dapper;
using log4net;
using Bcredi.Utils.Database;
using Bcredi.DAO.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Linq;

namespace Bcredi.DAO.Repository
{
    public class DocumentoRepository
    {
        // Referencia para logar as informacoes da classe

        static string ambiente = ConfigurationManager.ConnectionStrings["ConnectionBcredi"].ConnectionString;
        static string strConexao = ConfigurationManager.ConnectionStrings[ambiente].ConnectionString;
        private DBUtil dbUtil = new DBUtil();

        public List<Documento> CarregarDocumentos(string guidUsuario)
        {
            List<Documento> listaDocumento = new List<Documento>();
            listaDocumento = CarregaDocumentos(guidUsuario);
            return listaDocumento;
        }

        public List<Documento> PesquisarDocumentosQuitacaoDivida(Guid idUsuario, Guid idTipoDocumento)
        {
            List<Documento> listaDocumentos = new List<Documento>();
            IEnumerable lista = null;

            using (var conexao = new SqlConnection(strConexao))
            {
                try
                {
                    var sql = @"SELECT D.idDocumento, D.idUsuario, D.idTipoDocumento, D.caminhoDocumento, D.isAtivo, D.nomeDocumento, D.idItemGenerico, 
                                       D.dataRecusa, D.idDocumentoMotivo, T.descricaoTipoDocumento
                            FROM [dbo].[AL_DOCUMENTO] D, [dbo].[AL_DOCUMENTO_TIPO] T
                            WHERE D.idTipoDocumento = T.idTipoDocumento
                            AND D.idUsuario = @Id 
                            AND D.idTipoDocumento = @IdTipoDocumento
                            AND D.isAtivo = 1
                    ";

                    //O Guid informado no SQL para o campo idTipoDocumento está definido como sendo id para o tipo "Comprovante Pagamento Avaliacao"

                    lista = conexao.Query<Documento>(sql, new { Id = idUsuario, IdTipoDocumento = idTipoDocumento });

                    foreach (Documento usuarioDocumento in lista)
                    {
                        listaDocumentos.Add(usuarioDocumento);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return listaDocumentos;
        }

        //Método criado para buscar a lista de documentos de um determinado setor da regua
        //O método PesquisaDocumentoAvaliacaoImovel foi criado com o intuito de ser genérico e buscar documento
        public List<DocumentoLista> PesquisarDocumentoPosLogado(Guid idUsuario, Guid idItemGenerico, int idPagina, Guid idTipoDocumento)
        {
            List<DocumentoLista> listaDocumentos = new List<DocumentoLista>();
            IEnumerable lista = null;

            using (var conexao = new SqlConnection(strConexao))
            {
                try
                {
                    var sql = @"SELECT D.idDocumento, D.idUsuario, D.idTipoDocumento, D.caminhoDocumento, D.isAtivo, D.nomeDocumento, D.idItemGenerico, D.dataRecusa, D.idDocumentoMotivo
                            FROM [dbo].[AL_DOCUMENTO] D, [dbo].[AL_DOCUMENTO_TIPO] T
                            WHERE D.idTipoDocumento = T.idTipoDocumento
                            AND D.idUsuario = @Id 
                            AND D.idItemGenerico = @IdItemGenerico 
                            AND D.idTipoDocumento = @IdTipoDocumento
                            AND D.idPagina = @IdPagina
                            AND D.isAtivo = 1
                            ";

                    //O Guid informado no SQL para o campo idTipoDocumento está definido como sendo id para o tipo "Comprovante Pagamento Avaliacao"

                    lista = conexao.Query<DocumentoLista>(sql, new { Id = idUsuario, IdItemGenerico = idItemGenerico, IdPagina = idPagina, IdTipoDocumento = idTipoDocumento });

                    foreach (DocumentoLista usuarioDocumento in lista)
                    {
                        listaDocumentos.Add(usuarioDocumento);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return listaDocumentos;
        }

        public List<Documento> PesquisarDocumentoTipo(Guid idUsuario, Guid idItemGenerico, int idPagina, Guid idTipoDocumento)
        {
            List<Documento> listaDocumentos = new List<Documento>();
            Documento documentoMotivo = new Documento();
            IEnumerable lista = null;
            IEnumerable listaTipo = null;

            using (var conexao = new SqlConnection(strConexao))
            {
                try
                {
                    var sql = @"SELECT D.idDocumento, D.idUsuario, D.idTipoDocumento, D.caminhoDocumento, D.isAtivo, D.nomeDocumento, D.idItemGenerico, D.dataRecusa, D.idDocumentoMotivo
                            FROM [dbo].[AL_DOCUMENTO] D, [dbo].[AL_DOCUMENTO_TIPO] T
                            WHERE D.idTipoDocumento = T.idTipoDocumento
                            AND D.idUsuario = @Id 
                            AND D.idItemGenerico = @IdItemGenerico 
                            AND D.idTipoDocumento = @IdTipoDocumento
                            AND D.idPagina = @IdPagina
                            AND D.isAtivo = 1
                            ";

                    lista = conexao.Query<Documento>(sql, new { Id = idUsuario, IdItemGenerico = idItemGenerico, IdPagina = idPagina, IdTipoDocumento = idTipoDocumento });

                    //foreach utilizado para reconhecer se o documento tem um Motivo de Recusa(idDocumentoMotivo)
                    foreach (Documento usuarioDocumento in lista)
                    {
                        documentoMotivo = usuarioDocumento;
                    }

                    //Se ele tiver um motivo, ele vai buscar qual é a descrição do motivo informado pelo operador
                    if (!documentoMotivo.idDocumentoMotivo.Equals(new Guid("00000000-0000-0000-0000-000000000000")))
                    {
                        var sqlM = @"SELECT descricaoTipoDocumento
                            FROM [dbo].[AL_DOCUMENTO_MOTIVO]
                            WHERE idDocumentoMotivo = @IdDocumentoMotivo
                            ";

                        listaTipo = conexao.Query<Documento>(sqlM, new { IdDocumentoMotivo = documentoMotivo.idDocumentoMotivo });

                    }
                    //Adiciona a descrição no objeto e adiciona na lista de retorno
                    if (listaTipo != null)
                    {

                        foreach (Documento usuarioDocumento in listaTipo)
                        {
                            foreach (Documento Documento in lista)
                            {
                                Documento.DescricaoTipoDocumento = usuarioDocumento.DescricaoTipoDocumento;
                                listaDocumentos.Add(Documento);
                            }
                        }
                    }
                    else
                    {
                        foreach (Documento usuarioDocumento in lista)
                        {
                            listaDocumentos.Add(usuarioDocumento);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return listaDocumentos;
        }

        public bool SalvarDocumentosPosLogado(List<Documento> documentoLista)
        {

            var retorno = false;

            foreach (Documento itemDocumento in documentoLista)
            {
                InserirDocumentosPosLogado(itemDocumento);
            }

            return retorno;
        }

        public bool InserirDocumentosPosLogado(Documento doc)
        {
            var retorno = false;
            try
            {
                using (var conexaoBD = new SqlConnection(strConexao))
                {
                    doc.idUsuarioInclusao = Core.UsuarioAtual.Id;
                    conexaoBD.Execute(@"INSERT INTO dbo.AL_DOCUMENTO(idDocumento, idUsuario, idTipoDocumento, caminhoDocumento, isAtivo, dataInclusao, idUsuarioInclusao, nomeDocumento, nomeBcrediDocumento, idItemGenerico)
                                        VALUES ( @IdDocumento, @idUsuario, @idTipoDocumento, @caminhoDocumento, 1, GETDATE(), @idUsuarioInclusao, @NomeDocumento, @NomeBcrediDocumento, @idItemGenerico)", doc);
                }
                retorno = true;
            }
            catch (Exception ex)
            {
                retorno = false;
                throw new Exception("Não foi possível inserir novo Documento", ex);
                throw;
            }

            return retorno;
        }

        public List<Documento> CarregaDocumentos(string guidUsuario)
        {
            List<Documento> listaDocumentos = new List<Documento>();
            IEnumerable lista = null;

            using (var conexao = new SqlConnection(strConexao))
            {
                try
                {
                    var sql = @"SELECT idDocumento, idUsuario, idTipoDocumento, caminhoDocumento, isAtivo, nomeDocumento, idItemGenerico, dataRecusa, idDocumentoMotivo
                            FROM [dbo].[AL_DOCUMENTO]
                            WHERE idUsuario = @Id
                            AND isAtivo = 1
                    ";

                    lista = conexao.Query<Documento>(sql, new { Id = guidUsuario });

                    foreach (Documento usuarioDocumento in lista)
                    {
                        listaDocumentos.Add(usuarioDocumento);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return listaDocumentos;
        }

        public bool SaveDocumento(Documento obj)
        {

            var retorno = false;
            try
            {
                using (var conexaoBD = new SqlConnection(strConexao))
                {
                    //Verifica se o conjuge existe ou não no banco
                    if (obj.IdUsuario != "")
                    {
                        obj.idUsuarioInclusao = Core.UsuarioAtual.Id;
                        conexaoBD.Execute(@"INSERT INTO dbo.AL_DOCUMENTO(idDocumento, idUsuario, idTipoDocumento, caminhoDocumento, isAtivo, dataInclusao, idUsuarioInclusao, nomeDocumento, idItemGenerico, dataRecusa, idDocumentoMotivo)
                                                    values ( @idDocumento, @idUsuario, @idTipoDocumento, @caminhoDocumento, 1, GETDATE(), @idUsuarioInclusao, @NomeDocumento, null, null, null)", obj);
                    }
                }
                retorno = true;
            }
            catch (Exception ex)
            {
                retorno = false;
                throw new Exception("Não foi possível atualizar novo Documento", ex);
                throw;
            }

            return retorno;
        }
    }
}
