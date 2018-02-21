using AutoMapper;
using ClientesMVC.Application.Interface;
using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Entities.Enums;
using ClientesMVC.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClientesMVC.MVC.Controllers
{
    //Controller para tratar as requisições da api
    public class ClienteApiViewModelsController : ApiController
    {
        private readonly IPessoaFisicaAppService _pessoaFisicaApp;
        private readonly IPessoaJuridicaAppService _pessoaJuridicaApp;

        public ClienteApiViewModelsController(IPessoaFisicaAppService pessoaFisicaApp, IPessoaJuridicaAppService pessoaJuridicaApp)
        {
            _pessoaFisicaApp = pessoaFisicaApp;
            _pessoaJuridicaApp = pessoaJuridicaApp;
        }

        //Tratamento para a requisição get "api/ClienteApiViewModels"
        public IHttpActionResult GetClienteViewModels()
        {
            List<ClienteViewModel> clienteList = new List<ClienteViewModel>();

            try
            {
                //Recupera todos os clientes do tipo pessoa física
                var pessoasFisicas = _pessoaFisicaApp.GetAllEagerLoading();
                //Adiciona cada um deles ao viewmodel
                foreach (PessoaFisica pessoa in pessoasFisicas)
                {
                    //Utilizando AutoMapper para atribuição dos dados do model para a viewmodel
                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<PessoaFisica, ClienteViewModel>().ForMember(p => p.TipoPessoa, map => map.UseValue(TipoPessoa.Física)); });
                    var cliente = config.CreateMapper().Map<PessoaFisica, ClienteViewModel>(pessoa);

                    clienteList.Add(cliente);
                }

                //Recupera todos os clientes do tipo pessoa jurídica
                var pessoasJuridicas = _pessoaJuridicaApp.GetAllEagerLoading();
                //Adiciona cada um deles ao viewmodel
                foreach (PessoaJuridica pessoa in pessoasJuridicas)
                {
                    //Utilizando AutoMapper para atribuição dos dados do model para a viewmodel
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<PessoaJuridica, ClienteViewModel>().ForMember(p => p.TipoPessoa, map => map.UseValue(TipoPessoa.Jurídica)); });
                    var cliente = config.CreateMapper().Map<PessoaJuridica, ClienteViewModel>(pessoa);

                    clienteList.Add(cliente);
                }

                //Retorna o Json além do status 200 - Ok
                return Ok(new { clientes = clienteList });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}