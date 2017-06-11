using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bcredi.DAO.Models
{
    public class VM_UsuarioComplementoDadosBancarios
    {
        #region Usuário

        public int IdUsuario { get; set; }

        public int IdProposta { get; set; }

        public string GuidUsuario { get; set; }

        [MaxLength(200, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        [EmailAddress(ErrorMessage = "Digite um email válido!")]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        [MaxLength(40, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string Fone { get; set; }

        [AdditionalMetadata("MaxLength", 9)]
        [MaxLength(9, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string CEP { get; set; }

        [Display(Name = "Telefone Preferencial")]
        [MaxLength(50, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]

        public string TelefonePreferencial { get; set; }

        #endregion

        #region Usuário Complemento

        [Display(Name = "Endereço")]
        [MaxLength(100, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        [MaxLength(20, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string Numero { get; set; }

        [MaxLength(60, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        public string Complemento { get; set; }

        [MaxLength(60, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string Bairro { get; set; }

        [MaxLength(60, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public int Estado { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        public string Foto { get; set; }

        //public HttpPostedFileBase Foto { get; set; }

        public bool IsAtivo { get; set; }

        #endregion

        #region Dados Bancários

        public Guid IdDadosBancarios { get; set; }

        [Display(Name = "Nome do Banco")]
        [MaxLength(100, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string NomeBanco { get; set; }

        [Display(Name = "Agência")]
        [MaxLength(10, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string Agencia { get; set; }

        [Display(Name = "Tipo Conta")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string TipoConta { get; set; }

        [MaxLength(10, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string Conta { get; set; }

        [Display(Name = "Nome Titular")]
        [MaxLength(100, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requerido.")]
        public string NomeTitular { get; set; }

        #endregion

        public string Cnpj { get; set; }

        public string Cpf { get; set; }

        public string RazaoSocial { get; set; }

        public double Valor { get; set; }
    }
}
