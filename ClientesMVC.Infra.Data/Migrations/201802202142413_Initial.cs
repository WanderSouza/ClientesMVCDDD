namespace ClientesMVC.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 8000, unicode: false),
                        UFID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UF", t => t.UFID)
                .Index(t => t.UFID);
            
            CreateTable(
                "dbo.UF",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CEP = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Logradouro = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(maxLength: 8000, unicode: false),
                        Bairro = c.String(nullable: false, maxLength: 8000, unicode: false),
                        CidadeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidade", t => t.CidadeID)
                .Index(t => t.CidadeID);
            
            CreateTable(
                "dbo.PessoaFisica",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CPF = c.String(maxLength: 8000, unicode: false),
                        DataNasc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Nome = c.String(maxLength: 8000, unicode: false),
                        Sobrenome = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cliente", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.PessoaJuridica",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CNPJ = c.String(maxLength: 8000, unicode: false),
                        RazaoSocial = c.String(maxLength: 8000, unicode: false),
                        NomeFantasia = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cliente", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PessoaJuridica", "ID", "dbo.Cliente");
            DropForeignKey("dbo.PessoaFisica", "ID", "dbo.Cliente");
            DropForeignKey("dbo.Cliente", "CidadeID", "dbo.Cidade");
            DropForeignKey("dbo.Cidade", "UFID", "dbo.UF");
            DropIndex("dbo.PessoaJuridica", new[] { "ID" });
            DropIndex("dbo.PessoaFisica", new[] { "ID" });
            DropIndex("dbo.Cliente", new[] { "CidadeID" });
            DropIndex("dbo.Cidade", new[] { "UFID" });
            DropTable("dbo.PessoaJuridica");
            DropTable("dbo.PessoaFisica");
            DropTable("dbo.Cliente");
            DropTable("dbo.UF");
            DropTable("dbo.Cidade");
        }
    }
}
