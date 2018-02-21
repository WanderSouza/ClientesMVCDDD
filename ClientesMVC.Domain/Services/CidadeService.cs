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
    public class CidadeService : ServiceBase<Cidade>, ICidadeService
    {
        private ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository) : base(cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public Cidade GetCityById(int CidadeID)
        {
            return _cidadeRepository.GetCityById(CidadeID);
        }
    }
}
