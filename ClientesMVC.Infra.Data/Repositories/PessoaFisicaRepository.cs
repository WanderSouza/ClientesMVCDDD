using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

//Esse camada faz a ponte entre nosso modelo de domínio e o banco de dados, cada classe de domínio (entidade) possui um repositório de dados
namespace ClientesMVC.Infra.Data.Repositories
{
    //Herda os métodos do CRUD do repositório base e implementa a interface IPessoaFisicaRepository
    public class PessoaFisicaRepository: RepositoryBase<PessoaFisica>, IPessoaFisicaRepository
    {
        public IEnumerable<PessoaFisica> GetAllEagerLoading()
        {
            return Db.PessoaFisicas.Include(c => c.Cidade).ToList();
        }

        public PessoaFisica GetCityStateEagerLoadingById(int id)
        {
            return Db.PessoaFisicas.Include(c => c.Cidade.UF).SingleOrDefault(p => p.ID == id);
        }
    }
}
