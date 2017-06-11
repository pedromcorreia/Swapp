using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bcredi.DAO.Models
{
    public class VM_DadosPessoais
    {
        #region Usuário

        public int IdUsuario { get; set; }

        public string GuidUsuario { get; set; }

        public int TipoUsuario { get; set; }

        [MaxLength(200, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        [EmailAddress(ErrorMessage = "Digite um email válido!")]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        [MaxLength(40, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string Fone { get; set; }

        [AdditionalMetadata("MaxLength", 9)]
        [MaxLength(9, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string CEP { get; set; }

        [Display(Name = "Telefone Preferencial")]
        [MaxLength(50, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string TelefonePreferencial { get; set; }

        [Display(Name = "CPF")]
        [MaxLength(11, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string CPF_CNPJ { get; set; }

        [Display(Name = "Nome Completo")]
        [MaxLength(50, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string Nome { get; set; }

        public string Login { get; set; }

        #endregion

        #region Usuário Complemento

        [Display(Name = "Endereço residencial")]
        [MaxLength(100, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        [MaxLength(20, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string Numero { get; set; }

        [MaxLength(60, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        public string Complemento { get; set; }

        [MaxLength(60, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string Bairro { get; set; }

        [MaxLength(60, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public int Estado { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        public string Foto { get; set; }

        //public HttpPostedFileBase Foto { get; set; }

        public bool IsAtivo { get; set; }

        [Display(Name = "RG")]
        [MaxLength(20, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        public string Rg { get; set; }

        [Display(Name = "Órgão expedidor")]
        [MaxLength(8, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        public string OrgaoExpedidor { get; set; }

        [Display(Name = "Estado")]
        public int EstadoRG { get; set; }

        [Display(Name = "Data de expedição")]
        [MaxLength(20, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        public DateTime DataExpedicaoRG { get; set; }

        public bool IndicadorConjuge { get; set; }

        [Display(Name = "Data de nascimento")]
        [MaxLength(20, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        public DateTime DataNascimento { get; set; }

        public int idUsuarioInclusao { get; set; }

        public string dataInclusao { get; set; }

        public int idUsuarioAtualizacao { get; set; }

        public string dataAtualizacao { get; set; }

        #endregion

        #region Dados Bancários

        public Guid IdDadosBancarios { get; set; }

        [Display(Name = "Nome do Banco")]
        [MaxLength(100, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string NomeBanco { get; set; }

        [Display(Name = "Agência")]
        [MaxLength(10, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string Agencia { get; set; }

        [Display(Name = "Tipo Conta")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string TipoConta { get; set; }

        [MaxLength(10, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string Conta { get; set; }

        [Display(Name = "Nome Titular")]
        [MaxLength(100, ErrorMessage = "O campo {0} não pode ter mais que {1} campos.")]
        [Required(ErrorMessage = "O campo \"{0}\" é requirido.")]
        public string NomeTitular { get; set; }

        #endregion

        #region Usuario Conjuge/Proponente

        public string IdUsuarioRelacional { get; set; }

        public string IdConjugeRelacional { get; set; }

        public bool EnderecoDiferente { get; set; }

        public bool IndicadorComprovaRenda { get; set; }
        
        public List<VM_DadosPessoais> UsuariosProponente { get; set; }

        public VM_DadosPessoais Conjuge { get; set; }

        public VM_DadosPessoais UsuarioPrincipal { get; set; }

        #endregion

        public bool ComprovaRenda { get; set; }

        public string StringEstado { get; set; }

        public Guid idItemGenerico { get; set; }

    }
}
