using ClientesMVC.Application.Interface;
using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

//Essa camada tem como função coordenar e expor as operações realizadas pelo domínio
namespace ClientesMVC.Application
{
    public class PessoaFisicaAppService : AppServiceBase<PessoaFisica>, IPessoaFisicaAppService
    {
        private readonly IPessoaFisicaService _pessoaFisicaService;

        public PessoaFisicaAppService(IPessoaFisicaService pessoaFisicaService) : base(pessoaFisicaService)
        {
            _pessoaFisicaService = pessoaFisicaService;
        }

        public IEnumerable<PessoaFisica> GetAllEagerLoading()
        {
            return _pessoaFisicaService.GetAllEagerLoading();
        }

        public PessoaFisica GetCityStateEagerLoadingById(int id)
        {
            return _pessoaFisicaService.GetCityStateEagerLoadingById(id);
        }
    }
}
