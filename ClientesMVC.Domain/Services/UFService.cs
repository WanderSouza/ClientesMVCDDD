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
    public class UFService : ServiceBase<UF>, IUFService
    {
        private IUFRepository _UFRepository;

        public UFService(IUFRepository UFRepository) : base(UFRepository)
        {
            _UFRepository = UFRepository;
        }        
    }
}
