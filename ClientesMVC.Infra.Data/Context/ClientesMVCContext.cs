using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ClientesMVC.Domain.Entities;
using ClientesMVC.Infra.Data.EntityConfig;

namespace ClientesMVC.Infra.Data.Context
{
    public class ClientesMVCContext : DbContext
    {
        public ClientesMVCContext() : base("ClientesMVC")
        {

        }
            
        public DbSet<Clientes> Clientes { get; set; }

        public DbSet<PessoaFisica> PessoaFisicas { get; set; }

        public DbSet<PessoaJuridica> PessoaJuridicas { get; set; }

        public DbSet<UF> UFs { get; set; }

        public DbSet<Cidade> Cidades { get; set; }

        //Muda algumas configurações do modelo
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Evita que sejam deletados dados em cascata nas relações 1 pra muitos ou muitos pra muitos
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //Atribui o tipo varchar para as colunas strings, já que o padrão é nvarchar o que ocuparia muito mais espaço no banco
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            //Atribui as configurações do arquivo de configuração da entidade Clientes
            modelBuilder.Configurations.Add(new ClientesMVCConfiguration());
        }
    }
}
