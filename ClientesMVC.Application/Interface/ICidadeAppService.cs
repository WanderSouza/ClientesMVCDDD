using ClientesMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesMVC.Application.Interface
{
    public interface ICidadeAppService : IAppServiceBase<Cidade>
    {
        Cidade GetCityById(int CidadeID);
    }
}
