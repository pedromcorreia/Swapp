using System;
using System.Collections.Generic;
using Dapper;
using log4net;
using Bcredi.Utils.Database;
using Bcredi.DAO.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace Bcredi.DAO.Repository
{
    public class TipoDocumentoRepository
    {

        // Referencia para logar as informacoes da classe
        private static readonly ILog log = LogManager.GetLogger(typeof(TipoDocumentoRepository));

        static string ambiente = ConfigurationManager.ConnectionStrings["ConnectionBcredi"].ConnectionString;
        static string strConexao = ConfigurationManager.ConnectionStrings[ambiente].ConnectionString;
        private DBUtil dbUtil = new DBUtil();
        
        //Essa repository foi criada para sempre buscar o tipo de documento inserido na tabela AL_DOCUMETO_TIPO
        public Guid PesquisaTipoDocumento(string descricaoTipoDocumento)
        {
            Documento documento = new Documento();
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                var sql = @"SELECT idTipoDocumento
                            FROM AL_DOCUMENTO_TIPO
                            WHERE descricaoTipoDocumento = @DescricaoTipoDocumento
                            AND isAtivo = 1
                            ";
                
                using (var resultado = conexaoBD.QueryMultiple(sql, new { DescricaoTipoDocumento = descricaoTipoDocumento }))
                {
                    List<Documento> lista = resultado.Read<Documento>().ToList<Documento>();
                    if (lista != null && lista.Count > 0)
                    {
                        foreach (var tipo in lista)
                        {
                            documento.IdTipoDocumento = tipo.IdTipoDocumento;
                        }
                    }
                }
            }

            return documento.IdTipoDocumento;
        }
    }
}
