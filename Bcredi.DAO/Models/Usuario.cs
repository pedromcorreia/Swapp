using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Models
{
    [DataContract]
    public class Usuario : BaseVO
    {
        private string nome;

        public void setNome(string nome)
        {
            Nome = nome;
            this.nome = nome;
        }
        
        [DataMember]
        public int IdPerfil { get; set; }

        public string DataUltimoAcesso { get; set; }

        public string IpUltimoAcesso { get; set; }


        /// <summary>
        /// Configura o atributo Email
        /// </summary>
        /// <param name="email"></param>
        public void setEmail(string email)
        {
            Email = email;
            this.email = email;
        }
        /// <summary>
        /// Email do usuário
        /// </summary>

        private string email;
        /// <summary>
        /// Login do usuário
        /// </summary>
        private string login;
        /// <summary>
        /// Configura o atributo login
        /// </summary>
        /// <param name="login"></param>
        public void setLogin(string login)
        {
            Login = login;
            this.email = login;
        }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        private string password;

        /// <summary>
        /// Propriedade nome
        /// </summary>
        [DisplayName("Nome")]
        [DataMember]
        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        /// <summary>
        /// Propriedade login
        /// </summary>
        [DisplayName("Login")]
        [DataMember]
        public string Login
        {
            get
            {
                return login;
            }

            set
            {
                login = value;
            }
        }

        /// <summary>
        /// Propriedade email
        /// </summary>
        [DataMember]
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
        private string criptograf;
        public string Criptograf
        {
            get
            {
                return criptograf;
            }

            set
            {
                criptograf = value;
            }
        }

        /// <summary>
        /// Propriedade password(nao serializar este objeto)
        /// </summary>        
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        /// <summary>
        /// Configurar um novo usuário
        /// </summary>
        /// <param name="idUsuario">id do usuário</param>
        /// <param name="nome">nome do usuário</param>
        /// <returns>Usuario</returns>
        public Usuario buildUsuario(int idUsuario, string nome)
        {
            Usuario usuario = new Usuario();
            usuario.Id = idUsuario;
            usuario.Nome = nome;
            return usuario;
        }

        /// <summary>
        /// Configurar um novo usuário
        /// </summary>
        /// <param name="nome">nome do usuário</param>
        /// <param name="login">login do usuário</param>
        /// <param name="email">email do usuário</param>
        /// <returns>Usuario</returns>
        /// 
        public Usuario buildUsuario(string nome, string login, string email)
        {
            Usuario usuario = new Usuario();
            usuario.Nome = nome;
            usuario.Login = login;
            usuario.Email = email;
            return usuario;
        }


        public Usuario getFake()
        {

            Random random = new Random();
            int randomNumber = random.Next(0, 10000);

            Usuario fake = new Usuario();
            fake.Nome = "Alessandro";
            fake.setId(6);
            fake.setDescricao("fake " + randomNumber);

            return fake;
        }

        //Model Javier 22/09/2016

        private string endereco;
        public string Endereco
        {
            get
            {
                return endereco;
            }

            set
            {
                endereco = value;
            }
        }

        private string fone;
        public string Fone
        {
            get
            {
                return fone;
            }

            set
            {
                fone = value;
            }
        }
        private string cpf_cnpj;
        [DisplayName("Cpf_cnpj")]
        [DataMember]
        public string Cpf_cnpj
        {
            get
            {
                return cpf_cnpj;
            }

            set
            {
                cpf_cnpj = value;
            }
        }

        private DateTime dataExpiracaoSenha;
        public DateTime DataExpiracaoSenha
        {
            get
            {
                return dataExpiracaoSenha;
            }

            set
            {
                dataExpiracaoSenha = value;
            }
        }
        private int isSenhaTemporaria;
        public int IsSenhaTemporaria
        {
            get
            {
                return isSenhaTemporaria;
            }

            set
            {
                isSenhaTemporaria = value;
            }
        }
        private int idSetor;
        public int IdSetor
        {
            get
            {
                return idSetor;
            }

            set
            {
                idSetor = value;
            }
        }
        private int idResponsavelDireto;
        public int IdResponsavelDireto
        {
            get
            {
                return idResponsavelDireto;
            }

            set
            {
                idResponsavelDireto = value;
            }
        }

        private string txtUsuario;
        public string TxtUsuario
        {
            get
            {
                return txtUsuario;
            }

            set
            {
                txtUsuario = value;
            }
        }

        private string txtEmail;
        public string TxtEmail
        {
            get
            {
                return txtEmail;
            }

            set
            {
                txtEmail = value;
            }
        }

        private int txtSetor;
        public int TxtSetor
        {
            get
            {
                return txtSetor;
            }

            set
            {
                txtSetor = value;
            }
        }
        private int txtIdPerfil;
        public int TxtIdPerfil
        {
            get
            {
                return txtIdPerfil;
            }

            set
            {
                txtIdPerfil = value;
            }
        }
        private string txtEndereco;
        public string TxtEndereco
        {
            get
            {
                return txtEndereco;
            }

            set
            {
                txtEndereco = value;
            }
        }


        public string Telefone { get; set; }

        private string txtCpfCnpj;
        [DisplayName("Cpf_cnpj")]
        [DataMember]
        public string TxtCpfCnpj
        {
            get
            {
                return txtCpfCnpj;
            }

            set
            {
                txtCpfCnpj = value;
            }
        }
        [DataMember]
        public string TokenResetSenha { get; set; }

        private string idUsuarioCriador;
        public string IdUsuarioCriador
        {
            get
            {
                return idUsuarioCriador;
            }

            set
            {
                idUsuarioCriador = value;
            }
        }

        public string Cep { get; set; }

        public DateTime DataAniversario { get; set; }

        public string GuidUsuario { get; set; }

        //public IEnumerable<Usuario> ListaMensagens { get; set; }

        public bool IndicadorAssinatura { get; set; }

        public bool UpdateDadosPessoais { get; set; }

        public string ChaveDocumentoAssinatura { get; set; }


    }
}
