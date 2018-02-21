using ClientesMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesMVC.Domain.Interfaces.Services
{
    public interface IPessoaJuridicaService : IServiceBase<PessoaJuridica>
    {
        IEnumerable<PessoaJuridica> GetAllEagerLoading();

        PessoaJuridica GetCityStateEagerLoadingById(int id);
    }
}
