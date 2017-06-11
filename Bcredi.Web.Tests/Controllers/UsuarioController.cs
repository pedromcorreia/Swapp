using Bcredi.DAO.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Bcredi.DAO.Service;
using System.Diagnostics;
using System.Data.SqlClient;
using Bcredi.Utils.Database;
using System.Configuration;


namespace Bcredi.Web.Tests.Controllers
{
    [TestClass]
    public class UsuarioController
    {
        DocumentoService documentoService = new DocumentoService();

        static string ambiente = ConfigurationManager.ConnectionStrings["ConnectionBcredi"].ConnectionString;
        static string strConexao = ConfigurationManager.ConnectionStrings[ambiente].ConnectionString;
        private DBUtil dbUtil = new DBUtil();

        private const string GUID_PADRAO = "00000000-0000-0000-0000-000000000000";

        #region [GERADOR DE GUID]

        [TestMethod]
        public void testeGerarGuid()
        {
            var guid = Guid.NewGuid();
        }

        #endregion

        #region [Teste para ANALISE JURIDICA]



        #endregion



        #region [Testes para DOCUMENTO]

        [TestMethod]
        public void testePesquisarDocumentoPosLogado()
        {
            var idUsuario = new Guid("e56577b0-8b58-46c7-a560-9100cb81476f");
            var idItemGenerico = new Guid("1DFE2BD3-1450-4C2E-9907-47897A547D68");
            var idPagina = 1;
            Guid idTipoDocumento = new Guid("468207ef-ff91-4f22-af0f-5ce33098c87b");
            documentoService.PesquisarDocumentoPosLogado(idUsuario, idItemGenerico, idPagina, idTipoDocumento);
            //return guid;
        }

        [TestMethod]
        public void testePesquisarDocumentoLista()
        {
            var idUsuario = new Guid("e56577b0-8b58-46c7-a560-9100cb81476f");
            var idItemGenerico = new Guid("1DFE2BD3-1450-4C2E-9907-47897A547D68");
            var idPagina = 1;
            Guid idTipoDocumento = new Guid("468207ef-ff91-4f22-af0f-5ce33098c87b");
            documentoService.PesquisarDocumentoTipo(idUsuario, idItemGenerico, idPagina, idTipoDocumento);
            //return guid;
        }

        [TestMethod]
        public void testePesquisaTipoDocumento()
        {
            TipoDocumentoService tipoDocumentoService = new TipoDocumentoService();
            string descricaoTipoDocumento = "Analise Credito";
            tipoDocumentoService.PesquisaTipoDocumento(descricaoTipoDocumento);
            //return guid;
        }

        [TestMethod]
        public void testeListaDocumento()
        {
            DocumentoService documentoService = new DocumentoService();
            string guidUsuarioTest = "AVB5ECC8-0854-44C9-B2AF-AC71192E4F0B";

            List<Documento> documentoLista = documentoService.CarregarDocumentos(guidUsuarioTest);
        }

        #endregion

        #region [Testes para TERMO DE ACEITE]

        #endregion

        #region [Teste para REGUA BCREDI]

        #endregion


        #region [Teste para Renda]

        [TestMethod]
        public void testeInserirDadosnaMao()
        {
            Guid idProposta = new Guid("D0196ED0-7A6D-41BD-8CB5-F00F53C59BFE");
            string sql = @"
            
            INSERT INTO [dbo].[AL_RENDA]
           ([idRenda]
           ,[idUsuario]
           ,[idProposta]
           ,[tipoComprovacaoRenda]
           ,[holerite]
           ,[valorHolerite]
           ,[impostoRenda]
           ,[valorImpostoRenda]
           ,[extratoBancario]
           ,[valorExtratoBancario]
           ,[descricaoRenda]
           ,[isAtivo])
            VALUES
            (
                NEWID(), 'AVB5ECC8-0854-44C9-B2AF-AC71192E4F0B', 'd0196ed0-7a6d-41bd-8cb5-f00f53c59bfe', 
                4, 1, 1111, 1, 2222, 1, 3333, 'DESCRICAO USUARIO MARIO SOUZA 3', 1
            )
            ";

            SqlConnection conexao = null;

            try
            {
                conexao = dbUtil.openConnection();

                SqlCommand sqlCommand = new SqlCommand(string.Format(sql, Core.BcrediDB, Core.SecurityDB), conexao);

                dbUtil.executeNonQuery(sqlCommand);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar lembrete", ex);
            }
        }

        #endregion

        #region [Teste para Usuario]


        [TestMethod]
        public void testeUsuario()
        {
            Usuario usuario = new Usuario();
            usuario = usuario.getFake();

            string usuarioSerializado = Utils.Utils.Serialize(usuario);

            Usuario usuarioDeserealizado = Utils.Utils.Deserialize<Usuario>(usuarioSerializado);

            Assert.AreEqual(usuario.Id, usuarioDeserealizado.Id);

        }

        private static VM_DadosPessoais getUsuarioTesteNovo()
        {
            //## Usuario ########################################################################
            VM_DadosPessoais obj1 = new VM_DadosPessoais();
            obj1.IdUsuario = 1;
            obj1.GuidUsuario = string.Empty;
            obj1.Nome = "Saci Perere";
            obj1.CPF_CNPJ = "68412251717";
            obj1.Rg = "622547812";
            obj1.OrgaoExpedidor = "SSP";
            obj1.EstadoRG = 0;
            obj1.DataExpedicaoRG = Convert.ToDateTime("28/03/2015");
            obj1.CEP = "81550000";
            obj1.Endereco = "Rua do Saci";
            obj1.Numero = "17 B";
            obj1.Complemento = "Beco 77";
            obj1.Bairro = "Selva";
            obj1.Cidade = "Sacilândia";
            obj1.Estado = 1;
            obj1.Fone = "41998989898";
            obj1.TelefonePreferencial = "4135251585";
            obj1.Email = "saciperere@gmail.com";
            obj1.DataNascimento = Convert.ToDateTime("17/12/1977");
            obj1.IndicadorConjuge = true;
            obj1.IdDadosBancarios = Guid.NewGuid();
            obj1.NomeBanco = "Banco da Amazonia";
            obj1.Agencia = "7541";
            obj1.Conta = "171717";
            obj1.TipoConta = "1";
            obj1.TipoUsuario = 0;
            obj1.NomeTitular = "Saci";
            obj1.IdUsuarioRelacional = Guid.NewGuid().ToString();
            obj1.IdConjugeRelacional = Guid.NewGuid().ToString();
            obj1.EnderecoDiferente = false;
            obj1.IndicadorComprovaRenda = true;
            obj1.IsAtivo = true;

            //Conjuge do Usuario
            var obj2 = new VM_DadosPessoais();
            obj2.IdUsuario = 1;
            obj2.GuidUsuario = Guid.NewGuid().ToString();
            obj2.Nome = "Conjuge do Usuario";
            obj2.CPF_CNPJ = "68412251717";
            obj2.Rg = "622547812";
            obj2.OrgaoExpedidor = "SSP";
            obj2.EstadoRG = 0;
            obj2.DataExpedicaoRG = Convert.ToDateTime("28/03/2015");
            obj2.CEP = "81550000";
            obj2.Endereco = "Rua do Saci";
            obj2.Numero = "17 B";
            obj2.Complemento = "Beco 77";
            obj2.Bairro = "Selva";
            obj2.Cidade = "Sacilândia";
            obj2.Estado = 1;
            obj2.Fone = "41998989898";
            obj2.TelefonePreferencial = "4135251585";
            obj2.Email = "saciperere@gmail.com";
            obj2.DataNascimento = Convert.ToDateTime("17/12/1977");
            obj2.IndicadorConjuge = true;
            obj2.IdDadosBancarios = Guid.NewGuid();
            obj2.NomeBanco = "Banco da Amazonia";
            obj2.Agencia = "7541";
            obj2.Conta = "171717";
            obj2.TipoConta = "1";
            obj2.TipoUsuario = 2;
            obj2.NomeTitular = "Saci";
            obj2.IdUsuarioRelacional = Guid.NewGuid().ToString();
            obj2.IdConjugeRelacional = Guid.NewGuid().ToString();
            obj2.EnderecoDiferente = false;
            obj2.IndicadorComprovaRenda = true;
            obj2.IsAtivo = true;
            obj1.Conjuge = obj2;

            //## Lista de Proponentes ###############################################################
            var listaUsuarioProponente = new List<VM_DadosPessoais>();

            //## Proponente 1 #######################################################################
            var usuarioProponente1 = new VM_DadosPessoais();
            usuarioProponente1.IdUsuario = 1;
            usuarioProponente1.GuidUsuario = Guid.NewGuid().ToString();
            usuarioProponente1.Nome = "usuarioProponente1";
            usuarioProponente1.CPF_CNPJ = "68412251717";
            usuarioProponente1.Rg = "622547812";
            usuarioProponente1.OrgaoExpedidor = "SSP";
            usuarioProponente1.EstadoRG = 0;
            usuarioProponente1.DataExpedicaoRG = Convert.ToDateTime("28/03/2015");
            usuarioProponente1.CEP = "81550000";
            usuarioProponente1.Endereco = "Rua do Saci";
            usuarioProponente1.Numero = "17 B";
            usuarioProponente1.Complemento = "Beco 77";
            usuarioProponente1.Bairro = "Selva";
            usuarioProponente1.Cidade = "Sacilândia";
            usuarioProponente1.Estado = 1;
            usuarioProponente1.Fone = "41998989898";
            usuarioProponente1.TelefonePreferencial = "4135251585";
            usuarioProponente1.Email = "saciperere@gmail.com";
            usuarioProponente1.DataNascimento = Convert.ToDateTime("17/12/1977");
            usuarioProponente1.IndicadorConjuge = true;
            usuarioProponente1.IdDadosBancarios = Guid.NewGuid();
            usuarioProponente1.NomeBanco = "Banco da Amazonia";
            usuarioProponente1.Agencia = "7541";
            usuarioProponente1.Conta = "171717";
            usuarioProponente1.TipoConta = "1";
            usuarioProponente1.TipoUsuario = 1;
            usuarioProponente1.NomeTitular = "Saci";
            usuarioProponente1.IdUsuarioRelacional = Guid.NewGuid().ToString();
            usuarioProponente1.IdConjugeRelacional = Guid.NewGuid().ToString();
            usuarioProponente1.EnderecoDiferente = false;
            usuarioProponente1.IndicadorComprovaRenda = true;
            usuarioProponente1.IsAtivo = true;

            //-- Proponente 1 Conjuge --------------------------------
            var conjuge1 = new VM_DadosPessoais();
            conjuge1.IdUsuario = 1;
            conjuge1.GuidUsuario = Guid.NewGuid().ToString();
            conjuge1.Nome = "conjuge1";
            conjuge1.CPF_CNPJ = "68412251717";
            conjuge1.Rg = "622547812";
            conjuge1.OrgaoExpedidor = "SSP";
            conjuge1.EstadoRG = 0;
            conjuge1.DataExpedicaoRG = Convert.ToDateTime("28/03/2015");
            conjuge1.CEP = "81550000";
            conjuge1.Endereco = "Rua do Saci";
            conjuge1.Numero = "17 B";
            conjuge1.Complemento = "Beco 77";
            conjuge1.Bairro = "Selva";
            conjuge1.Cidade = "Sacilândia";
            conjuge1.Estado = 1;
            conjuge1.Fone = "41998989898";
            conjuge1.TelefonePreferencial = "4135251585";
            conjuge1.Email = "saciperere@gmail.com";
            conjuge1.DataNascimento = Convert.ToDateTime("17/12/1977");
            conjuge1.IndicadorConjuge = true;
            conjuge1.IdDadosBancarios = Guid.NewGuid();
            conjuge1.NomeBanco = "Banco da Amazonia";
            conjuge1.Agencia = "7541";
            conjuge1.Conta = "171717";
            conjuge1.TipoConta = "1";
            conjuge1.TipoUsuario = 2;
            conjuge1.NomeTitular = "Saci";
            conjuge1.IdUsuarioRelacional = Guid.NewGuid().ToString();
            conjuge1.IdConjugeRelacional = Guid.NewGuid().ToString();
            conjuge1.EnderecoDiferente = false;
            conjuge1.IndicadorComprovaRenda = true;
            conjuge1.IsAtivo = true;
            usuarioProponente1.Conjuge = conjuge1;

            //## Proponente 2 ###############################################################
            var usuarioProponente2 = new VM_DadosPessoais();
            var conjuge2 = new VM_DadosPessoais();
            usuarioProponente2.Conjuge = conjuge2;
            //## Proponente 3 ###############################################################
            var usuarioProponente3 = new VM_DadosPessoais();
            var conjuge3 = new VM_DadosPessoais();
            usuarioProponente3.Conjuge = conjuge3;

            listaUsuarioProponente.Add(usuarioProponente1);
            listaUsuarioProponente.Add(usuarioProponente2);
            listaUsuarioProponente.Add(usuarioProponente3);

            obj1.UsuariosProponente = listaUsuarioProponente;
            return obj1;
        }


        public List<VM_DadosPessoais> getListaNovosProponentes()
        {
            //## Lista de Proponentes ###############################################################
            var listaUsuarioProponente = new List<VM_DadosPessoais>();

            //## Proponente 1 #######################################################################
            var usuarioProponente1 = new VM_DadosPessoais();
            usuarioProponente1.IdUsuario = 1;
            usuarioProponente1.GuidUsuario = GUID_PADRAO;
            usuarioProponente1.Nome = "usuarioProponente1";
            usuarioProponente1.CPF_CNPJ = "68412251717";
            usuarioProponente1.Rg = "622547812";
            usuarioProponente1.OrgaoExpedidor = "SSP";
            usuarioProponente1.EstadoRG = 0;
            usuarioProponente1.DataExpedicaoRG = Convert.ToDateTime("28/03/2015");
            usuarioProponente1.CEP = "81550000";
            usuarioProponente1.Endereco = "Rua do Saci";
            usuarioProponente1.Numero = "17 B";
            usuarioProponente1.Complemento = "Beco 77";
            usuarioProponente1.Bairro = "Selva";
            usuarioProponente1.Cidade = "Sacilândia";
            usuarioProponente1.Estado = 1;
            usuarioProponente1.Fone = "41998989898";
            usuarioProponente1.TelefonePreferencial = "4135251585";
            usuarioProponente1.Email = "saciperere2@gmail.com";
            usuarioProponente1.DataNascimento = Convert.ToDateTime("17/12/1977");
            usuarioProponente1.IndicadorConjuge = true;
            usuarioProponente1.IdDadosBancarios = Guid.NewGuid();
            usuarioProponente1.NomeBanco = "Banco da Amazonia";
            usuarioProponente1.Agencia = "7541";
            usuarioProponente1.Conta = "171717";
            usuarioProponente1.TipoConta = "1";
            usuarioProponente1.TipoUsuario = 1;
            usuarioProponente1.NomeTitular = "Saci";
            usuarioProponente1.Login = "saciperere3@gmail.com";
            usuarioProponente1.IdUsuarioRelacional = Guid.NewGuid().ToString();
            usuarioProponente1.IdConjugeRelacional = Guid.NewGuid().ToString();
            usuarioProponente1.EnderecoDiferente = false;
            usuarioProponente1.IndicadorComprovaRenda = true;
            usuarioProponente1.IsAtivo = true;

            //-- Proponente 1 Conjuge --------------------------------
            var conjuge1 = new VM_DadosPessoais();
            conjuge1.IdUsuario = 1;
            conjuge1.GuidUsuario = GUID_PADRAO;
            conjuge1.Nome = "conjuge1";
            conjuge1.CPF_CNPJ = "68412251717";
            conjuge1.Rg = "622547812";
            conjuge1.OrgaoExpedidor = "SSP";
            conjuge1.EstadoRG = 0;
            conjuge1.DataExpedicaoRG = Convert.ToDateTime("28/03/2015");
            conjuge1.CEP = "81550000";
            conjuge1.Endereco = "Rua do Saci";
            conjuge1.Numero = "17 B";
            conjuge1.Complemento = "Beco 77";
            conjuge1.Bairro = "Selva";
            conjuge1.Cidade = "Sacilândia";
            conjuge1.Estado = 1;
            conjuge1.Fone = "41998989898";
            conjuge1.TelefonePreferencial = "4135251585";
            conjuge1.Email = "saciperere3@gmail.com";
            conjuge1.DataNascimento = Convert.ToDateTime("17/12/1977");
            conjuge1.IndicadorConjuge = true;
            conjuge1.IdDadosBancarios = Guid.NewGuid();
            conjuge1.NomeBanco = "Banco da Amazonia";
            conjuge1.Agencia = "7541";
            conjuge1.Conta = "171717";
            conjuge1.TipoConta = "1";
            conjuge1.TipoUsuario = 2;
            conjuge1.NomeTitular = "Saci";
            conjuge1.Login = "saciperere4@gmail.com";
            conjuge1.IdUsuarioRelacional = Guid.NewGuid().ToString();
            conjuge1.IdConjugeRelacional = Guid.NewGuid().ToString();
            conjuge1.EnderecoDiferente = false;
            conjuge1.IndicadorComprovaRenda = true;
            conjuge1.IsAtivo = true;
            usuarioProponente1.Conjuge = conjuge1;

            //## Proponente 2 ###############################################################
            var usuarioProponente2 = new VM_DadosPessoais();
            var conjuge2 = new VM_DadosPessoais();
            usuarioProponente2.Conjuge = conjuge2;
            //## Proponente 3 ###############################################################
            var usuarioProponente3 = new VM_DadosPessoais();
            var conjuge3 = new VM_DadosPessoais();
            usuarioProponente3.Conjuge = conjuge3;

            listaUsuarioProponente.Add(usuarioProponente1);
            listaUsuarioProponente.Add(usuarioProponente2);
            listaUsuarioProponente.Add(usuarioProponente3);

            return listaUsuarioProponente;
        }
        [TestMethod]

        public void testInserirAtualizarUsuario()
        {
            //string jsonDadosUsuario = "{\"Agencia\":null,\"Bairro\":\"456464\",\"CEP\":\"88888888\",\"CPF_CNPJ\":\"02511365478\",\"Cidade\":\"56464\",\"Complemento\":\"546\",\"ComprovaRenda\":false,\"Conjuge\":null,\"Conta\":null,\"DataExpedicaoRG\":\"/Date(-62135589600000-0200)/\",\"DataNascimento\":\"\/Date(535082400000-0200)\/\",\"Email\":\"mario.souza@bcredi.com.br\",\"Endereco\":\"54654654\",\"EnderecoDiferente\":false,\"Estado\":1,\"EstadoRG\":0,\"Fone\":\"41 456465456\",\"Foto\":null,\"GuidUsuario\":\"AVB5ECC8-0854-44C9-B2AF-AC71192E4F0B\",\"IdConjugeRelacional\":null,\"IdDadosBancarios\":\"00000000-0000-0000-0000-000000000000\",\"IdUsuario\":58,\"IdUsuarioRelacional\":\"\",\"IndicadorComprovaRenda\":false,\"IndicadorConjuge\":false,\"IsAtivo\":true,\"NOME\":\"Novo"+Guid.NewGuid+"\",\"NomeBanco\":null,\"NomeTitular\":null,\"Numero\":\"54654\",\"OrgaoExpedidor\":null,\"Rg\":null,\"TelefonePreferencial\":null,\"TipoConta\":null,\"UsuariosProponente\":null}";
            string jsonDadosUsuario = "{\"Agencia\":null,\"Bairro\":\"456464\",\"CEP\":\"88888888\",\"CPF_CNPJ\":\"02511365478\",\"Cidade\":\"56464\",\"Complemento\":\"546\",\"ComprovaRenda\":false,\"Conjuge\":null,\"Conta\":null,\"DataExpedicaoRG\":\"\\/Date(-621355896000-0200)\\/\",\"DataNascimento\":\"\\/Date(535082400000-0200)\\/\",00\"Email\":\"mario.souza@bcredi.com.br\",\"Endereco\":\"54654654\",\"EnderecoDiferente\":false,\"Estado\":1,\"EstadoRG\":0,\"Fone\":\"41456465456\",\"Foto\":null,\"GuidUsuario\":\"AVB5ECC8-0854-44C9-B2AF-AC71192E4F0B\",\"IdConjugeRelacional\":null,\"IdDadosBancarios\":\"00000000-0000-0000-0000-000000000000\",\"IdUsuario\":58,\"IdUsuarioRelacional\":\"\",\"IndicadorComprovaRenda\":false,\"IndicadorConjuge\":false,\"IsAtivo\":true,\"NOME\":\"MarioFabre\",\"NomeBanco\":null,\"NomeTitular\":null,\"Numero\":\"54654\",\"OrgaoExpedidor\":null,\"Rg\":null,\"TelefonePreferencial\":null,\"TipoConta\":null,\"UsuariosProponente\":null}";


        }
        
        #endregion

        #region [Teste para SESSAO FAKE]

        public void configurar_sessao_fake()
        {
            Usuario usuarioTestes = new Usuario();
            usuarioTestes.Id = 1;

            System.Web.HttpContext.Current = HttpContextManager.FakeHttpContext();
            Core.UsuarioAtual = usuarioTestes;
        }

        #endregion

    }
}

