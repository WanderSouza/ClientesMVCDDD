using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ClientesMVC.Infra.Data.Repositories
{
    public class PessoaJuridicaRepository : RepositoryBase<PessoaJuridica>, IPessoaJuridicaRepository
    {
        public IEnumerable<PessoaJuridica> GetAllEagerLoading()
        {
            return Db.PessoaJuridicas.Include(c => c.Cidade).ToList();
        }

        public PessoaJuridica GetCityStateEagerLoadingById(int id)
        {
            return Db.PessoaJuridicas.Include(c => c.Cidade.UF).SingleOrDefault(p => p.ID == id);
        }
    }
}
