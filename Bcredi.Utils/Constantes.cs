using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.Utils
{
    public class Constantes
    {
        public const string ATIVO = "True";

        public const string INATIVO = "False";

        public const int ID_STATUS_PARCELA_EM_ATRASO_PENDENTE = 1; 

        public const int ID_STATUS_PARCELA_EM_ATRASO_PAGA = 2;

        #region [HIERARQUIA DO USUARIO]

        public const int ID_HIERARQUIA_DIRETOR = 1;

        public const int ID_HIERARQUIA_GESTOR = 2;

        public const int ID_HIERARQUIA_USUARIO = 3;
        #endregion [HIERARQUIA DO USUARIO]

        #region [TIPOS DE CLIENTE NO CONTRATO]
        public const int FLAG_NAO_CLIENTE_PRINCIPAL_DO_CONTRATO = 0;

        public const int FLAG_CLIENTE_PRINCIPAL_DO_CONTRATO = 1;

        #endregion [TIPOS DE CLIENTE NO CONTRATO]

        public const string CAMINHO_DOCUMENTO_JURIDICO = "Juridico";
        public const string RATING_PARAMETRO_ENVIAR_JURIDICO = "ENVIAR_JURIDICO";
        public const string EXCLUIR_DOCUMENTO_JURIDICO = "EXCLUIR_DOCUMENTO_JURIDICO";
        public const int ID_ESTADO_PADRAO = 18;

        // TODO: LABATTI REVISTAR (CARTEIRA X TIPO EMPREENDIMENTO)
        public const int ID_CARTEIRA_PADRAO = 2; // REPOSITORIO CARTEIRA

        public const int ID_EMPREENDIMENTO_PADRAO = 1; // REPOSITORIO EMPREENDIMENTO

        //public const int ID_CARTEIRA_PADRAO = 2; // REPOSITORIO TIPO_EMPREENDIMENTO

        public const int ID_CAMPO_TIPO_DATA = 1;

        public const double DIAS_VALOR_CALCULO = 0.00277777777;

        #region [FASES DO CONTRATO]

        public const int CONTRATO_FASE_COBRANCA_EM_ATRASO = 0;
        public const string CONTRATO_FASE_COBRANCA_EM_ATRASO_DESCRICAO = "EM ATRASO";

        public const int CONTRATO_FASE_COBRANCA_ADMINISTRATIVA = 1;
        public const int CONTRATO_FASE_COBRANCA_JURIDICA = 2;
        public const int CONTRATO_FASE_NOTIFICACAO = 3;
        public const int CONTRATO_FASE_CONSOLIDACAO = 4;
        public const int CONTRATO_FASE_LEILAO = 5;
        public const int CONTRATO_FASE_BNDU = 6;
        #endregion

        #region [TIPOS DE TAREFA]
        public const int ID_TAREFA_AUTOMATICA = 1;

        public const int ID_TAREFA_MANUAL = 2;

        #endregion [TIPOS DE TAREFA]

        #region [TIPO DE LANCAMENTO]
        public const string TIPO_LANCAMENTO_DEBITO = "D";

        public const string TIPO_LANCAMENTO_CREDITO = "C";

        #endregion [TIPO DE LANCAMENTO]

        #region TIPOS DE STATUS UNIDADE SERVICING
        public const int CONSTANTE_INICIADO = 1;
        public const int CONSTANTE_PRE_NEGADO = 2;
        public const int CONSTANTE_PRE_APROVADO = 3;
        public const int CONSTANTE_JURIDICO = 4;
        public const int CONSTANTE_COMITE = 6;
        public const int CONSTANTE_GERENCIAL = 7;
        public const int CONSTANTE_APROVADO = 8;
        public const int CONSTANTE_NEGADO_GERENCIA = 9;
        public const int CONSTANTE_PENDENTE = 10;
        public const int CONSTANTE_NEGADO_JURIDICO = 11;
        public const int CONSTANTE_NEGADO_PRE_JURIDICO = 12;

        public const string CONSTANTE_COMERCIAL = "Comercial";
        public const string CONSTANTE_RESIDENCIAL = "Residencial";
        public const string CONSTANTE_MISTO = "Misto";
        public const string CONSTANTE_LOTEAMENTO = "Loteamento";

        public const int STATUS_NEGOCIACAO_EM_ANDAMENTO = 1;
        public const int STATUS_NEGOCIACAO_CONCLUIDA = 2;

        #endregion TIPOS DE STATUS UNIDADE SERVICING

        #region [STATUS DA TAREFA]
        public const int ID_TAREFA_TODAS = 0;

        public const int ID_TAREFA_NAO_INICIADA = 1;

        public const int ID_TAREFA_EM_ANDAMENTO = 2;

        public const int ID_TAREFA_FINALIZADA = 3;

        public const int ID_TAREFA_INSUCESSO = 4;

        public const int ID_TAREFA_EXPIRADA = 5;

        public const int ID_TAREFA_REAGENDADA = 6;

        public const int ID_TAREFA_ATRIBUIDA_NOVO_USUARIO = 7;

        public const int ID_TAREFA_DEVOLVIDA_SOLICITANTE = 8;

        //Quantidade maxima de vezes que uma tarefa pode ser delegada
        public const int QUANTIDADE_MAXIMA_DELEGAR_TAREFA = 3;

        public const string SERVER_PATH_FILE = "\\\\barsf00019\\wwwroot\\FileServer";


        //TODO Labbati: Perguntar para Alessandro qual sera este email
        public const string EMAIL_AUTOMATICO_SISTEMA = "onesystem@bariguifinanceira.com.br";

        #endregion [STATUS DA TAREFA]

        #region [SERVICING NOVA CARTEIRA]

        public const string NOME_PASTA_CARTEIRA = "CARTEIRA";
        public const string NOME_PASTA_PLANILHA = "PLANILHA";
        public const string NOME_PASTA_SPE = "SPE";
        public const string NOME_PASTA_EMPREENDIMENTO = "EMPREENDIMENTO";
        public const string NOME_PASTA_DOCUMENTOSDACARTEIRA = "DOCUMENTOSDACARTEIRA";
        public const string NOME_PASTA_DOCUMENTOSDAUNIDADE = "DOCUMENTOSDAUNIDADE";
        public const string NOME_PASTA_DOCUMENTOSDAPROPRIETARIO = "DOCUMENTOSDOPROPRIETARIO";

        public const string RATING_PARAMETRO_LISTAR_SERVICING = "RATING_PARAMETRO_LISTAR";

        #endregion [SERVICING NOVA CARTEIRA]

        #region [STATUS DA SEMAFORO]
        public const int VERDE = 0;

        public const int AMARELO = 1;

        public const int VERMELHO = 2;

        public const int CINZA = 3;

        #endregion [STATUS DA TAREFA]

        #region [SEMAFORO]
        public const string SEMAFORO_VERDE = "/Imagens/green-face.png";

        public const string SEMAFORO_AMARELO = "/Imagens/yellow-face.png";

        public const string SEMAFORO_VERMELHO = "/Imagens/red-face.png";

        //Faltam apenas 50% do tempo para a atividade
        public const double PERCENTUAL_50 = 0.5;

        //Faltam apenas 20% do tempo para a atividade
        public const double PERCENTUAL_20 = 0.2;

        public static int TAMANHO_LIMITE_MEGABYTES = 15;

        //Tamanho limite 15 MB
        public static int TAMANHO_LIMITE_ARQUIVO_EM_BYTES = TAMANHO_LIMITE_MEGABYTES * 1048576;

        #endregion [SEMAFORO]

        #region [TIPOS DE ARQUIVOS PERMITIDOS] 
        public static string TIPO_ARQUIVO_DOC = "application/msword";
        public static string TIPO_ARQUIVO_DOCX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public static string TIPO_ARQUIVO_XLS = "application/vnd.ms-excel";
        public static string TIPO_ARQUIVO_XLSX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public static string TIPO_ARQUIVO_PDF = "application/pdf";
        public static string TIPO_ARQUIVO_JPEG = "image/jpeg";
        public static string TIPO_ARQUIVO_PNG = "image/png";
        public static string TIPO_ARQUIVO_OCTET_STREAM = "application/octet-stream";
        #endregion [TIPOS DE ARQUIVOS PERMITIDOS] 

        #region [TIPO FERIADO]
        public static int FERIADO_UNIVERSAL = 0;
        public static int FERIADO_NACIONAL = 1;
        public static int FERIADO_ESTADUAL = 2;
        public static int FERIADO_MUNICIPAL = 3;
        public static int FERIADO_FLUTUANTE = 4;
        #endregion [TIPO FERIADO]

        #region [LOG_ACESSO]
        /// <summary>
        /// Somente operacoes de login/acesso ao sistema
        /// </summary>
        public const string LOGIN = "LOGIN";
        public const string LOGOUT = "LOGOUT";
        public const string LOGOUT_TIMEOUT = "LOGOUT_TIMEOUT";
        #endregion [LOG_ACESSO]

        #region [LOG]
        /// <summary>
        /// Somente operacoes de sistema tais como
        /// RATING_PARAMETRO_PESQUISAR, RATING_PARAMETRO_CRIAR, RATING_PARAMETRO_EDITAR, RATING_PARAMETRO_APAGAR, 
        /// CONTRATO_PESQUISAR, CONTRATO_INTEGRAR, ETC.
        /// 
        /// </summary>
        public const string RATING_PARAMETRO_LISTAR = "RATING_PARAMETRO_LISTAR";
        public const string RATING_PARAMETRO_PESQUISAR = "RATING_PARAMETRO_PESQUISAR";
        public const string RATING_PARAMETRO_CRIAR = "RATING_PARAMETRO_CRIAR";
        public const string RATING_PARAMETRO_EDITAR = "RATING_PARAMETRO_EDITAR";
        public const string RATING_PARAMETRO_APAGAR = "RATING_PARAMETRO_APAGAR";

        public const string RATING_CONTRATO_LISTAR = "RATING_CONTRATO_LISTAR";
        public const string RATING_CONTRATO_PESQUISAR = "RATING_CONTRATO_PESQUISAR";
        public const string RATING_CONTRATO_CRIAR = "RATING_CONTRATO_CRIAR";
        public const string RATING_CONTRATO_EDITAR = "RATING_CONTRATO_EDITAR";
        public const string RATING_CONTRATO_APAGAR = "RATING_CONTRATO_APAGAR";
        public const string RATING_CONTRATO_CALCULAR = "RATING_CONTRATO_CALCULAR";

        public const string USUARIO_RESETAR_SENHA = "USUARIO_RESETAR_SENHA";
        public const string USUARIO_REDEFINIR_SENHA = "USUARIO_REDEFINIR_SENHA";
        public const string USUARIO_LISTAR = "USUARIO_LISTAR";
        public const string USUARIO_PESQUISAR = "USUARIO_PESQUISAR";
        public const string USUARIO_CRIAR = "USUARIO_CRIAR";
        public const string USUARIO_EDITAR = "USUARIO_EDITAR";
        public const string USUARIO_APAGAR = "USUARIO_APAGAR";

        public const string DASHBOARD_LISTAR = "DASHBOARD_LISTAR";

        public const string NOTIFICACAO_DOCUMENTO_CONTRATO_CRI_CRIAR = "NOTIFICACAO_DOCUMENTO_CONTRATO_CRI_CRIAR";
        public const string NOTIFICACAO_DOCUMENTO_MATRICULA_CRI_CRIAR = "NOTIFICACAO_DOCUMENTO_MATRICULA_CRI_CRIAR";
        public const string NOTIFICACAO_CONSULTA_RESTRITIVA_CRIAR = "NOTIFICACAO_CONSULTA_RESTRITIVA_CRIAR";
        public const string NOTIFICACAO_CONSULTA_RESTRITIVA_ATUALIZAR = "NOTIFICACAO_CONSULTA_RESTRITIVA_ATUALIZAR";
        public const string NOTIFICACAO_CONSULTA_RESTRITIVA_APAGAR = "NOTIFICACAO_CONSULTA_RESTRITIVA_APAGAR";

        #endregion [LOG]

        #region INPUTINICIAL

        public const int POSICAO_COLUNA_CARTEIRA = 1;
        public const int POSICAO_COLUNA_UNIDADE = 2;
        public const int POSICAO_COLUNA_NOME = 3;
        public const int POSICAO_COLUNA_CPF_CNPJ = 4;
        public const int POSICAO_COLUNA_EMPREENDIMENTO = 5;
        public const int POSICAO_COLUNA_TIPO = 6;
        public const int POSICAO_COLUNA_CIDADE = 7;
        public const int POSICAO_COLUNA_ESTADO = 8;
        public const int POSICAO_COLUNA_COOBRIGACAO = 9;
        public const int POSICAO_COLUNA_VMD = 10;
        public const int POSICAO_COLUNA_DATAVENDA = 11;
        public const int POSICAO_COLUNA_VENCIMENTO = 12;
        public const int POSICAO_COLUNA_VALORVENDA = 13;
        public const int POSICAO_COLUNA_TAXACARTEIRA = 14;
        public const int POSICAO_COLUNA_BEHAVIOR030 = 15;
        public const int POSICAO_COLUNA_BEHAVIOR3160 = 16;
        public const int POSICAO_COLUNA_BEHAVIOR6190 = 17;
        public const int POSICAO_COLUNA_BEHAVIOR91 = 18;
        public const int POSICAO_COLUNA_PRAZODECORRER = 19;
        public const int POSICAO_COLUNA_VALORPARCELA = 20;
        public const int POSICAO_COLUNA_BALAO = 21;
        public const int POSICAO_COLUNA_VALORBALAO = 22;

        #endregion INPUTINICIAL


        #region [TIPO DOCUMENTO JURIDICO]

        public const int TIPO_DOCUMENTO_JURIDICO_CONTRATO = 2;
        public const int TIPO_DOCUMENTO_JURIDICO_MATRICULA = 3;

        #endregion [TIPO DOCUMENTO JURIDICO]

        #region DIAS DA SEMANA
        public const int DIA_DOMINGO = 0;
        public const int DIA_SEGUNDA = 1;
        public const int DIA_TERÇA = 2;
        public const int DIA_QUARTA = 3;
        public const int DIA_QUINTA = 4;
        public const int DIA_SEXTA = 5;
        public const int DIA_SABADO = 6;
        #endregion DIAS DA SEMANA

        #region PERIDO
        public const int CONSTANTE_MANHA = 1;
        public const int CONSTANTE_TARDE = 2;
        public const int CONSTANTE_NOITE = 3;
        #endregion PERIDO

        #region [TIPO DE USUARIOS DA PROPOSTA]
        public const int TIPO_USUARIO = 0;
        public const int TIPO_USUARIO_PROPONENTE = 1;
        public const int TIPO_USUARIO_CONJUGE = 2;
        #endregion [TIPO DE USUARIOS DA PROPOSTA]

        #region TIPO DE DOCUMENTO GUID  

        public const string CONST_GUID_CPF = "9D515B63-652F-4CBD-ABE9-9E0605A4F522";
        public const string CONST_GUID_RG = "93CA3B58-37F0-4FCD-947C-2787AB3A2D81";

        #endregion TIPO DE DOCUMENTO GUID  

        #region STATUS REGUA
        //TODO 18/04/2017 - Jogar no web.config
        public const string STATUS_REGUA_ANALISE_CREDITO = "3a8ea776-e8b5-413d-8a96-173f61e78f87";
        public const string STATUS_REGUA_AVALIACAO_IMOVEL = "C032A44B-B618-47F4-B29C-EF682B004801";
        public const string STATUS_REGUA_ANALISE_JURIDICA = "6D70CDBD-91DE-4DDA-896E-864ABB3ACD6E";
        public const string STATUS_REGUA_EMISSAO_CONTRATO = "4EA2949B-17CA-4516-A33D-BBBC08C4C455";
        public const string STATUS_REGUA_QUITACAO_DIVIDA = "35770C76-3990-46D5-9D57-D163D6A230E6";
        public const string STATUS_REGUA_CARTORIO = "3E0A044C-030E-435A-BCD9-5EA3112B7A6F";
        public const string STATUS_REGUA_PAGAMENTO = "725DA796-B620-41D9-9308-82F4A33778CD";
        #endregion STATUS PROPOSTA

        #region TIPO RENDA
        public const int TIPO_RENDA_ASSALARIADO = 1;
        public const int TIPO_RENDA_EMPRESARIO = 2;
        public const int TIPO_RENDA_AUTONOMO = 3;
        public const int TIPO_RENDA_OUTROS = 4;
        #endregion TIPO RENDA
        
        #region STATUS PROPOSTA
        public const int STATUS_PROPOSTA_ABERTA  = 1;
        public const int STATUS_PROPOSTA_FECHADA = 2;
        #endregion STATUS PROPOSTA

        #region [GUID_PAGINAS_SISTEMA]
        public const string AVALIACAO_IMOVEL = "e39e65d1-cace-49ab-b3bf-e15864c8210d";
        public const string ANALISE_JURIDICA = "8238a442-2cb2-49de-8b88-9601786749ff";
        public const string ANALISE_CREDITO = "ca11a41f-dd5e-4e97-a29b-3800cd3932a8";
        public const string QUITACAO_DIVIDA = "440b5315-9fa1-4924-a30b-19b8ef46d235";
        #endregion [TIPO DE USUARIOS DA PROPOSTA]

        #region [GUID_TIPO_DOCUMENTO]
        public const string DOC_CPF = "9D515B63-652F-4CBD-ABE9-9E0605A4F522";
        public const string DOC_RG = "93CA3B58-37F0-4FCD-947C-2787AB3A2D81";
        public const string DOC_ANALISE_JURIDICA = "468207EF-FF91-4F22-AF0F-5CE33098C87B";
        public const string DOC_COMPROVANTE_PGTO_AVALIACAO = "2AAC6274-EFC1-4343-AE28-D3753EC8B287";
        public const string DOC_BOLETO = "DC16697A-3C3D-484A-A2B5-9A38AAFB0B19";
        public const string DOC_ANALISE_CREDITO = "8A62F85C-8881-4DCA-9E20-8307B193A4EF";
        public const string DOC_FOTO_IMOVEL = "7EA6530B-1425-431E-B7F1-3DFCBB40A94E";
        public const string DOC_IPTU = "D3522E76-1EBC-4B01-969F-B72B8D23219E";
        public const string DOC_CONDOMINIO = "8DBC4A61-1D65-42C4-B2E3-467EBEE8F9BD";
        public const string DOC_PENDECIA_QUITACAO = "98646747-660B-4BE6-A8A1-AF8046A6F645";
        public const string DOC_COMPROVANTE_CARTORIO = "30CF9209-A942-4BEF-AD66-7A6060E6FEC2";
        public const string DOC_PENDECIA_CARTORIO = "5b5db664-8012-47f4-8991-df3783fadb44";
        public const string DOC_COMPROVANTE_PAGAMENTO = "474B8F61-2137-440D-88BF-3A698031E0E1";
        #endregion [TIPO DE DOCTOS GERAL]

        public const string SERVER_WEB_API = "http://localhost:20024/";
        public const string SERVER_WEB_API_TOKEN = "";

        #region [STRING_TIPO_DOCUMENTO]
        public const string DOC_STRING_BOLETO = "boleto";
        public const string DOC_STRING_CPF = "CPF";
        public const string DOC_STRING_RG = "RG";
        public const string DOC_STRING_ANALISE_JURIDICA = "Analise Juridica";
        public const string DOC_STRING_COMPROVANTE_PAGAMENTO_AVALIACAO = "Comprovante Pagamento Avaliacao";
        public const string DOC_STRING_ANALISE_CREDITO = "Analise Credito";
        public const string DOC_STRING_FOTO_IMOVEL = "Foto Imovel";
        #endregion [TIPO STRING_TIPO_DOCUMENTO DOCTOS GERAL]

        #region [QUITACAO_DIVIDA]

        public const string DOC_INFORMACOES_DOCUMENTOS = "InformacoesDocumentos";
        public const string DOC_INFORMACOES_PESSOAIS = "InformacoesPessoais";
        public const string DOC_SOLICITACAO_PORTABILIDADE = "SolicitacaoPortabilidade";
        public const string DOC_QUITACAO_FINALIZADA = "QuitacaoFinalizada";
        public const string DOC_QUITACAO = "Quitacao";
        #endregion [QUITACAO_DIVIDA]

        #region [CONSTANTE_BCREDI_GUID]
        public const string BCREDI_GUID = "bcd577b0-8b58-46c7-a560-9100cb81476f";
        #endregion [CONSTANTE_BCREDI_GUID]
    }
}
