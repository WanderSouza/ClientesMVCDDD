using ClientesMVC.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ClientesMVC.Infra.Data.EntityConfig
{
    //Muda as configurações da entidade para que na criação da tabela sejam consideradas essas mudanças
    public class ClientesMVCConfiguration : EntityTypeConfiguration<Clientes>
    {
        public ClientesMVCConfiguration()
        {
            //Informa qual campo é a chave primária
            HasKey(c => c.ID);
            //Informa a obrigatoriedade dos campos
            Property(c => c.Bairro).IsRequired();
            Property(c => c.CEP).IsRequired();
            Property(c => c.CidadeID).IsRequired();
            Property(c => c.Logradouro).IsRequired();
            Property(c => c.Numero).IsRequired();
        }
    }
}
