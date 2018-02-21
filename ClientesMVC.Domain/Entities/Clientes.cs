
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesMVC.Domain.Entities
{
    [Table("Cliente")]
    public class Clientes
    {
        public int ID { get; set; }
                
        public string CEP { get; set; }
        
        public string Logradouro { get; set; }
                
        public int Numero { get; set; }

        public string Complemento { get; set; }
                
        public string Bairro { get; set; }
                
        public int CidadeID { get; set; }

        public virtual Cidade Cidade { get; set; }
    }
}
