using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesMVC.Domain.Entities
{
    [Table("PessoaJuridica")]
    public class PessoaJuridica : Clientes
    {        
        public string CNPJ { get; set; }
                
        public string RazaoSocial { get; set; }
                
        public string NomeFantasia { get; set; }
    }
}