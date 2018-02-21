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
    public class CidadeAppService : AppServiceBase<Cidade>, ICidadeAppService
    {
        private readonly ICidadeService _cidadeService;

        public CidadeAppService(ICidadeService cidadeService) : base(cidadeService)
        {
            _cidadeService = cidadeService;
        }

        public Cidade GetCityById(int CidadeID)
        {
            return _cidadeService.GetCityById(CidadeID);
        }
    }
}
