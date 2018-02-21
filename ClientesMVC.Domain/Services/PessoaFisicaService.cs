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
    public class PessoaFisicaService : ServiceBase<PessoaFisica>, IPessoaFisicaService
    {
        private IPessoaFisicaRepository _pessoaFisicaRepository;

        public PessoaFisicaService(IPessoaFisicaRepository pessoaFisicaRepository) : base(pessoaFisicaRepository)
        {
            _pessoaFisicaRepository = pessoaFisicaRepository;
        }

        public IEnumerable<PessoaFisica> GetAllEagerLoading()
        {
            return _pessoaFisicaRepository.GetAllEagerLoading();
        }

        public PessoaFisica GetCityStateEagerLoadingById(int id)
        {
            return _pessoaFisicaRepository.GetCityStateEagerLoadingById(id);
        }
    }
}
