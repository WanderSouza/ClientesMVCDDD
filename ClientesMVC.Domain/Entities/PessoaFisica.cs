using ClientesMVC.Domain.Entities.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Essa camada é a base do projeto, aqui é definido o modelo de negócios através de classes relativas as entidades 
namespace ClientesMVC.Domain.Entities
{
    [Table("PessoaFisica")]//Atribui o nome à tabela
    public class PessoaFisica : Clientes //Herda campos da classe cliente
    {        
        public string CPF { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DataNasc { get; set; }
                
        public string Nome { get; set; }
        
        public string Sobrenome { get; set; }
    }
}