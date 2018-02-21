using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Interfaces.Repositories;

namespace ClientesMVC.Infra.Data.Repositories
{
    public class CidadeRepository : RepositoryBase<Cidade>, ICidadeRepository
    {
        public Cidade GetCityById(int CidadeID)
        {
            return Db.Cidades.Find(CidadeID);
        }
    }
}
