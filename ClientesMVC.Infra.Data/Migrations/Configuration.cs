namespace ClientesMVC.Infra.Data.Migrations
{
    using ClientesMVC.Domain.Entities;
    using ClientesMVC.Infra.Data.Context;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClientesMVCContext>
    {
        public Configuration()
        {
            //Quando true, replica as modifica��es feitas no model no banco de dados
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ClientesMVCContext context)
        {
            //Se encontrou alguma cidade retorna, sen�o, executa o c�digo abaixo
            if (context.Cidades.Any())
            {
                return;
            }

            //Cria um array com 4 UFs
            var ufs = new UF[]
            {
                new UF() { ID = 1, Nome = "SP" },
                new UF() { ID = 2, Nome = "MG" },
                new UF() { ID = 3, Nome = "RJ" },
                new UF() { ID = 4, Nome = "RS" }
            };
            //Percorre o array, adicionando cada um dos item ao contexto
            foreach (UF u in ufs)
            {
                context.UFs.Add(u);
            }
            //Persiste as modifica��es no banco
            context.SaveChanges();

            //Cria um array com 10 cidades para cada UF
            var cidades = new Cidade[]
            {
                //S�o Paulo
                new Cidade() { ID = 1, Nome = "Mirassol", UFID = 1 },
                new Cidade() { ID = 2, Nome = "S�o Jos� do Rio Preto", UFID = 1 },
                new Cidade() { ID = 3, Nome = "S�o Paulo", UFID = 1 },
                new Cidade() { ID = 4, Nome = "Campinas", UFID = 1 },
                new Cidade() { ID = 5, Nome = "Santos", UFID = 1 },
                new Cidade() { ID = 6, Nome = "Neves Paulista", UFID = 1 },
                new Cidade() { ID = 7, Nome = "Jaci", UFID = 1 },
                new Cidade() { ID = 8, Nome = "Ribeir�o Preto", UFID = 1 },
                new Cidade() { ID = 9, Nome = "S�o Carlos", UFID = 1 },
                new Cidade() { ID = 10, Nome = "Vinhedo", UFID = 1 },  
                //Minas Gerais
                new Cidade() { ID = 1, Nome = "Contagem", UFID = 2 },
                new Cidade() { ID = 2, Nome = "Juiz de Fora", UFID = 2 },
                new Cidade() { ID = 3, Nome = "Belo Horizonte", UFID = 2 },
                new Cidade() { ID = 4, Nome = "Uberl�ndia", UFID = 2 },
                new Cidade() { ID = 5, Nome = "Betim", UFID = 2 },
                new Cidade() { ID = 6, Nome = "Montes Claros", UFID = 2 },
                new Cidade() { ID = 7, Nome = "Ribeir�o das Neves", UFID = 2 },
                new Cidade() { ID = 8, Nome = "Uberaba", UFID = 2 },
                new Cidade() { ID = 9, Nome = "Governador Valadares", UFID = 2 },
                new Cidade() { ID = 10, Nome = "Ipatinga", UFID = 2 },
                //Rio de Janeiro
                new Cidade() { ID = 1, Nome = "S�o Gon�alo", UFID = 3 },
                new Cidade() { ID = 2, Nome = "Duque de Caxias", UFID = 3 },
                new Cidade() { ID = 3, Nome = "Nova Igua�u", UFID = 3 },
                new Cidade() { ID = 4, Nome = "Niter�i", UFID = 3 },
                new Cidade() { ID = 5, Nome = "Rio de Janeiro", UFID = 3 },
                new Cidade() { ID = 6, Nome = "Cabo Frio", UFID = 3 },
                new Cidade() { ID = 7, Nome = "S�o Jo�o de Meriti", UFID = 3 },
                new Cidade() { ID = 8, Nome = "Campos dos Goytacazes", UFID = 3 },
                new Cidade() { ID = 9, Nome = "Volta Redonda", UFID = 3 },
                new Cidade() { ID = 10, Nome = "Petr�polis", UFID = 3 },
                //Rio Grande do Sul
                new Cidade() { ID = 1, Nome = "Pelotas", UFID = 4 },
                new Cidade() { ID = 2, Nome = "Canoas", UFID = 4 },
                new Cidade() { ID = 3, Nome = "Santa Maria", UFID = 4 },
                new Cidade() { ID = 4, Nome = "Gravata�", UFID = 4 },
                new Cidade() { ID = 5, Nome = "Viam�o", UFID = 4 },
                new Cidade() { ID = 6, Nome = "Novo Hamburgo", UFID = 4 },
                new Cidade() { ID = 7, Nome = "Porto Alegre", UFID = 4 },
                new Cidade() { ID = 8, Nome = "Caxias do Sul", UFID = 4 },
                new Cidade() { ID = 9, Nome = "S�o Leopoldo", UFID = 4 },
                new Cidade() { ID = 10, Nome = "Rio Grande", UFID = 4 }
            };
            //Percorre o array, adicionando cada um dos item ao contexto
            foreach (Cidade c in cidades)
            {
                context.Cidades.Add(c);
            }
            //Persiste as modifica��es no banco
            context.SaveChanges();
        }
    }
}
