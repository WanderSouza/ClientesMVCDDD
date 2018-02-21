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
    public class PessoaJuridicaService : ServiceBase<PessoaJuridica>, IPessoaJuridicaService
    {
        private IPessoaJuridicaRepository _pessoaJuridicaRepository;

        public PessoaJuridicaService(IPessoaJuridicaRepository pessoaJuridicaRepository) : base(pessoaJuridicaRepository)
        {
            _pessoaJuridicaRepository = pessoaJuridicaRepository;
        }

        public IEnumerable<PessoaJuridica> GetAllEagerLoading()
        {
            return _pessoaJuridicaRepository.GetAllEagerLoading();
        }

        public PessoaJuridica GetCityStateEagerLoadingById(int id)
        {
            return _pessoaJuridicaRepository.GetCityStateEagerLoadingById(id);
        }
    }
}
