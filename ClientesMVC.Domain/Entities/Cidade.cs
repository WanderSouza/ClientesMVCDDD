using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesMVC.Domain.Entities
{
    [Table("Cidade")]
    public class Cidade
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public int UFID { get; set; }

        public virtual UF UF { get; set; }
    }
}