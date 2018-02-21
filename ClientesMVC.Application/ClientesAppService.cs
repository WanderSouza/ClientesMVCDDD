using ClientesMVC.Application.Interface;
using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesMVC.Application
{
    public class ClientesAppService : AppServiceBase<Clientes>, IClientesAppService
    {
        private readonly IClientesService _clientesService;

        public ClientesAppService(IClientesService clientesService) : base(clientesService)
        {
            _clientesService = clientesService;
        }
    }
}
