using ClientesMVC.Application.Interface;
using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Interfaces.Repositories;
using ClientesMVC.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace ClientesMVC.Application
{
    public class PessoaJuridicaAppService : AppServiceBase<PessoaJuridica>, IPessoaJuridicaAppService
    {
        private readonly IPessoaJuridicaService _pessoaJuridicaService;

        public PessoaJuridicaAppService(IPessoaJuridicaService pessoaJuridicaService) : base(pessoaJuridicaService)
        {
            _pessoaJuridicaService = pessoaJuridicaService;
        }

        public IEnumerable<PessoaJuridica> GetAllEagerLoading()
        {
            return _pessoaJuridicaService.GetAllEagerLoading();
        }

        public PessoaJuridica GetCityStateEagerLoadingById(int id)
        {
            return _pessoaJuridicaService.GetCityStateEagerLoadingById(id);
        }
    }
}
