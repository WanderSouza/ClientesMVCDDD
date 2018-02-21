using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Interfaces.Repositories;
using ClientesMVC.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesMVC.Domain.Services
{
    public class ClientesService : ServiceBase<Clientes>, IClientesService
    {
        private IClientesRepository _clientesRepository;

        public ClientesService(IClientesRepository clientesRepository) : base(clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }
    }
}
