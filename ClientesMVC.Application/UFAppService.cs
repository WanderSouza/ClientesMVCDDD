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
    public class UFAppService : AppServiceBase<UF>, IUFAppService
    {
        private readonly IUFService _UFService;

        public UFAppService(IUFService UFService) : base(UFService)
        {
            _UFService = UFService;
        }
    }
}
