using ClientesMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesMVC.Domain.Interfaces.Services
{
    public interface IPessoaFisicaService : IServiceBase<PessoaFisica>
    {
        IEnumerable<PessoaFisica> GetAllEagerLoading();

        PessoaFisica GetCityStateEagerLoadingById(int id);
    }
}
