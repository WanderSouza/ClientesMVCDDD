using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace ClientesMVC.Infra.Data.Repositories
{
    public class ClientesRepository : RepositoryBase<Clientes>, IClientesRepository
    {
        public IEnumerable<Clientes> GetAllEagerLoading()
        {
            return Db.Clientes.Include(c => c.Cidade).ToList();
        }
    }
}
