using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesMVC.Domain.Entities
{
    [Table("UF")]
    public class UF
    {
        public int ID { get; set; }

        public string Nome { get; set; }
    }
}