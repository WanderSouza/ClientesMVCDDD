using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Entities.CustomValidation;
using ClientesMVC.Domain.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesMVC.MVC.ViewModels
{
    //ViewModel que contém os campos de pessoa física e jurídica, além dos campos em comum da entidade Cliente
    public class ClienteViewModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "CEP não pode ser maior do que 8 caracteres")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Digite o Logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Digite o Número")]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "Digite o Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Escolha a Cidade")]
        [Display(Name = "Cidade")]
        public int CidadeID { get; set; }

        public Cidade Cidade { get; set; }

        [Display(Name = "UF")]
        public int UFID { get; set; }

        public UF UF { get; set; }

        public TipoPessoa TipoPessoa { get; set; }

        #region Pessoa Física        
        [Required(ErrorMessage = "Digite o CPF")]
        public string CPF { get; set; }

        [MinimumAge(19)] //Data annotation customizado
        [Required(ErrorMessage = "Digite a Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Nascimento")] //Atribuindo o nome do campo para exibição na View
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        public DateTime DataNasc { get; set; }

        [Required(ErrorMessage = "Digite o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Sobrenome")]
        [StringLength(15, ErrorMessage = "Sobrenome não pode ser maior do que 15 caracteres")]
        public string Sobrenome { get; set; }
        #endregion

        #region Pessoa Jurídica        
        [Required(ErrorMessage = "Digite o CNPJ")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Digite a Razão Social")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Digite o Nome Fantasia")]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }
        #endregion        
    }
}