## Cadastro de Clientes MVC - DDD

- O objetivo dessa aplicação, é cadastrar clientes do tipo pessoa física ou jurídica, além de expor estes dados via Web Api.
- O usuário pode cadastrar, visualizar, editar e excluir os dados de um cliente, além de obter os dados dos clientes cadastrados via Web Api no formato Json.
- No momento do cadastro, foi utilizada uma api gratuita para busca de dados de endereço a partir do número do CEP.
- Este projeto foi feito utilizando as seguintes tecnologias: ASP .NET MVC 5, Entity Framework 6, Bootstrap 3, JQuery e banco de dados SQL Server, utilizando a arquitetura no padrão DDD (projeto orientado a domínio).

## Requisitos

- IIS 7.5
- .NET Framework 4.5 ou superior
- SQL Server Express
- Visual Studio 2013 ou superior

## Etapas para executar a aplicação

1. Primeiramente, efetue um git clone deste repositório.
2. Faça um build da solução.
3. Abra o NuGet Package Manager Console, selecione o projeto "ClientesMVC.Infra.Data" e execute 'Update-Database'.
4. F5 para que a aplicação se inicie.
