using ClientesMVC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ClientesMVC.Domain.Interfaces.Repositories
{
    //Herda os métodos CRUD da interface base
    public interface IPessoaJuridicaRepository : IRepositoryBase<PessoaJuridica>
    {
        IEnumerable<PessoaJuridica> GetAllEagerLoading();

        PessoaJuridica GetCityStateEagerLoadingById(int id);
    }
}
