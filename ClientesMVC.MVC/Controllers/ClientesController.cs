using AutoMapper;
using ClientesMVC.Application.Interface;
using ClientesMVC.Domain.Entities;
using ClientesMVC.Domain.Entities.Enums;
using ClientesMVC.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

//Camada de apresentação, interface do usuário
namespace ClientesMVC.MVC.Controllers
{
    public class ClientesController : Controller
    {
        //Instancia os objetos da camada de aplicação
        private readonly IPessoaFisicaAppService _pessoaFisicaApp;
        private readonly IPessoaJuridicaAppService _pessoaJuridicaApp;
        private readonly ICidadeAppService _cidadeApp;
        private readonly IUFAppService _UFApp;

        //Construtor que após a injeção das dependências via Ninject recupera os objetos da camada de aplicação
        public ClientesController(IPessoaFisicaAppService pessoaFisicaApp, IPessoaJuridicaAppService pessoaJuridicaApp, ICidadeAppService cidadeApp, IUFAppService UFApp)
        {
            _pessoaFisicaApp = pessoaFisicaApp;
            _pessoaJuridicaApp = pessoaJuridicaApp;
            _cidadeApp = cidadeApp;
            _UFApp = UFApp;
        }        

        // GET: Clientes
        public ActionResult Index()
        {
            List<ClienteViewModel> clienteList = new List<ClienteViewModel>();

            try
            {
                //Recupera todos os clientes do tipo pessoa física, usando eager loading para recuperar dados da entidade cidade
                var pessoasFisicas = _pessoaFisicaApp.GetAllEagerLoading();
                //Adiciona cada um deles ao viewmodel
                foreach (PessoaFisica pessoa in pessoasFisicas)
                {
                    //Utilizando AutoMapper para atribuição dos dados do model para a viewmodel
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<PessoaFisica, ClienteViewModel>()
                        .ForMember(c => c.TipoPessoa, map => map.UseValue(TipoPessoa.Física));
                    });
                    var clienteF = config.CreateMapper().Map<PessoaFisica, ClienteViewModel>(pessoa);

                    clienteList.Add(clienteF);
                }

                //Recupera todos os clientes do tipo pessoa jurídica, usando eager loading para recuperar dados da entidade cidade
                var pessoasJuridicas = _pessoaJuridicaApp.GetAllEagerLoading();
                //Adiciona cada um deles ao viewmodel
                foreach (PessoaJuridica pessoa in pessoasJuridicas)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<PessoaJuridica, ClienteViewModel>()
                        .ForMember(c => c.Nome, map => map.UseValue(pessoa.RazaoSocial))
                        .ForMember(c => c.CPF, map => map.UseValue(pessoa.CNPJ))
                        .ForMember(c => c.TipoPessoa, map => map.UseValue(TipoPessoa.Jurídica));
                    });
                    var clienteJ = config.CreateMapper().Map<PessoaJuridica, ClienteViewModel>(pessoa);

                    clienteList.Add(clienteJ);
                }

                //Retorna uma lista de clientes com campos específicos para a view
                return View(clienteList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id, string tipoPessoa)
        {
            ClienteViewModel viewModel;

            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //Preenche o modelo de acordo com o tipo de pessoa
                if (tipoPessoa == "Física")
                {
                    //Utilização de eager loading para recuperas as informações das entidades Cidade e UF relacionadas a pessoa
                    PessoaFisica pessoaFisica = _pessoaFisicaApp.GetCityStateEagerLoadingById(Convert.ToInt32(id));

                    //Utilizando AutoMapper para atribuição dos dados do model para a viewmodel
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<PessoaFisica, ClienteViewModel>()
                        .ForMember(p => p.Cidade, map => map.UseValue(pessoaFisica.Cidade))
                        .ForMember(p => p.TipoPessoa, map => map.UseValue(TipoPessoa.Física))
                        .ForMember(p => p.UF, map => map.UseValue(pessoaFisica.Cidade.UF));
                    });
                    viewModel = config.CreateMapper().Map<PessoaFisica, ClienteViewModel>(pessoaFisica);
                }
                else
                {
                    PessoaJuridica pessoaJuridica = _pessoaJuridicaApp.GetCityStateEagerLoadingById(Convert.ToInt32(id));

                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<PessoaJuridica, ClienteViewModel>()
                        .ForMember(p => p.Cidade, map => map.UseValue(pessoaJuridica.Cidade))
                        .ForMember(p => p.TipoPessoa, map => map.UseValue(TipoPessoa.Jurídica))
                        .ForMember(p => p.UF, map => map.UseValue(pessoaJuridica.Cidade.UF));
                    });
                    viewModel = config.CreateMapper().Map<PessoaJuridica, ClienteViewModel>(pessoaJuridica);
                }

                if (viewModel == null)
                {
                    return HttpNotFound();
                }

                //Retorna o viewModel preenchido para a view
                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Cliente/Create
        //Action para o carregamento da tela de cadastro de clientes
        public ActionResult Create()
        {
            ClienteViewModel clienteVm = new ClienteViewModel();

            try
            {
                //Adiciona DataNasc inicial ao viewmodel
                clienteVm.DataNasc = DateTime.Today;

                //Preenche as viewbags dos dropdownlists
                LoadViewBags(null);

                //Retorna o objeto viewmodel preenchido para a view
                return View(clienteVm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Action para a cadastro de um novo cliente
        public ActionResult Create(ClienteViewModel clienteViewModel)
        {
            try
            {
                //Se é cadastro de pessoa física
                if (clienteViewModel.TipoPessoa == TipoPessoa.Física)
                {
                    //Remove os campos de pessoa jurídica da validação do model
                    ModelState.Remove("CNPJ");
                    ModelState.Remove("NomeFantasia");
                    ModelState.Remove("RazaoSocial");
                }
                //Se é cadastro de pessoa jurídica
                else
                {
                    //Remove os campos de pessoa física da validação do model
                    ModelState.Remove("CPF");
                    ModelState.Remove("DataNasc");
                    ModelState.Remove("Nome");
                    ModelState.Remove("Sobrenome");
                }

                //Se o model é válido
                if (ModelState.IsValid)
                {
                    //Preenche o modelo de acordo com o tipo de pessoa
                    if (clienteViewModel.TipoPessoa == TipoPessoa.Física)
                    {
                        //Utilizando AutoMapper para atribuição dos dados da viewmodel para o model
                        var config = new MapperConfiguration(cfg => {
                            cfg.CreateMap<ClienteViewModel, PessoaFisica>()
                            .ForMember(p => p.CPF, map => map.UseValue(Regex.Replace(clienteViewModel.CPF, "[^0-9]", "")));
                        }); //Regex para persistir somente números
                        var pessoaFisica = config.CreateMapper().Map<ClienteViewModel, PessoaFisica>(clienteViewModel);

                        //Preenche as viewbags dos dropdownlists                        
                        LoadViewBags(pessoaFisica.Cidade);

                        //Adiciona ao contexto de banco de dados os dados de pessoa física
                        _pessoaFisicaApp.Add(pessoaFisica);
                    }
                    else
                    {
                        var config = new MapperConfiguration(cfg => {
                            cfg.CreateMap<ClienteViewModel, PessoaJuridica>()
                            .ForMember(p => p.CNPJ, map => map.UseValue(Regex.Replace(clienteViewModel.CNPJ, "[^0-9]", "")));
                        }); //Regex para persistir somente números
                        var pessoaJuridica = config.CreateMapper().Map<ClienteViewModel, PessoaJuridica>(clienteViewModel);

                        LoadViewBags(pessoaJuridica.Cidade);

                        _pessoaJuridicaApp.Add(pessoaJuridica);
                    }                    

                    //Retorna para a view index
                    return RedirectToAction("Index");
                }
                else
                {
                    //Preenche as viewbags dos dropdownlists                        
                    LoadViewBags(_cidadeApp.GetCityById(clienteViewModel.CidadeID));
                }

                return View(clienteViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int? id, string tipoPessoa)
        {
            ClienteViewModel viewModel;

            try
            {
                if (id == null || tipoPessoa == null)
                {
                    //Se os parâmetros estiverem nulos, retorna um erro com status 400
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //Preenche o modelo de acordo com o tipo de pessoa
                if (tipoPessoa == "Física")
                {
                    //Utilização de eager loading para recuperas as informações das entidades Cidade e UF relacionadas a pessoa
                    PessoaFisica pessoaFisica = _pessoaFisicaApp.GetCityStateEagerLoadingById(Convert.ToInt32(id));

                    //Utilizando AutoMapper para atribuição dos dados do model para a viewmodel
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<PessoaFisica, ClienteViewModel>().ForMember(p => p.CidadeID, map => map.UseValue(pessoaFisica.CidadeID)).ForMember(p => p.UFID, map => map.UseValue(pessoaFisica.Cidade.UF.ID)); });
                    viewModel = config.CreateMapper().Map<PessoaFisica, ClienteViewModel>(pessoaFisica);

                    //Preenche as viewbags dos dropdownlists, com os itens pré-selecionados
                    LoadViewBags(pessoaFisica.Cidade);
                }
                else
                {
                    PessoaJuridica pessoaJuridica = _pessoaJuridicaApp.GetCityStateEagerLoadingById(Convert.ToInt32(id));

                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<PessoaJuridica, ClienteViewModel>().ForMember(p => p.CidadeID, map => map.UseValue(pessoaJuridica.CidadeID)).ForMember(p => p.UFID, map => map.UseValue(pessoaJuridica.Cidade.UF.ID)); });
                    viewModel = config.CreateMapper().Map<PessoaJuridica, ClienteViewModel>(pessoaJuridica);

                    LoadViewBags(pessoaJuridica.Cidade);
                }

                if (viewModel == null)
                {
                    return HttpNotFound();
                }

                //Retorna o objeto viewmodel preenchido para a view
                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: Cliente/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteViewModel clienteViewModel)
        {
            try
            {
                //Se é cadastro de pessoa física
                if (clienteViewModel.TipoPessoa == TipoPessoa.Física)
                {
                    //Remove os campos de pessoa jurídica da validação do model
                    ModelState.Remove("CNPJ");
                    ModelState.Remove("NomeFantasia");
                    ModelState.Remove("RazaoSocial");
                }
                //Se é cadastro de pessoa jurídica
                else
                {
                    //Remove os campos de pessoa física da validação do model
                    ModelState.Remove("CPF");
                    ModelState.Remove("DataNasc");
                    ModelState.Remove("Nome");
                    ModelState.Remove("Sobrenome");
                }

                //Se o model é válido
                if (ModelState.IsValid)
                {
                    //Preenche o modelo de acordo com o tipo de pessoa
                    if (clienteViewModel.TipoPessoa == TipoPessoa.Física)
                    {
                        //Utilizando AutoMapper para atribuição dos dados da viewmodel para o model
                        var config = new MapperConfiguration(cfg => {
                            cfg.CreateMap<ClienteViewModel, PessoaFisica>()
                            .ForMember(p => p.CPF, map => map.UseValue(Regex.Replace(clienteViewModel.CPF, "[^0-9]", "")));
                        }); //Regex para persistir somente números
                        var pessoaFisica = config.CreateMapper().Map<ClienteViewModel, PessoaFisica>(clienteViewModel);

                        //Preenche as viewbags dos dropdownlists                
                        LoadViewBags(pessoaFisica.Cidade);

                        //Efetua o update
                        _pessoaFisicaApp.Update(pessoaFisica);
                    }
                    else
                    {
                        var config = new MapperConfiguration(cfg => {
                            cfg.CreateMap<ClienteViewModel, PessoaJuridica>()
                            .ForMember(p => p.CNPJ, map => map.UseValue(Regex.Replace(clienteViewModel.CNPJ, "[^0-9]", "")));
                        }); //Regex para persistir somente números
                        var pessoaJuridica = config.CreateMapper().Map<ClienteViewModel, PessoaJuridica>(clienteViewModel);

                        LoadViewBags(pessoaJuridica.Cidade);

                        //Efetua o update
                        _pessoaJuridicaApp.Update(pessoaJuridica);
                    }                    

                    //Retorna para a view index
                    return RedirectToAction("Index");
                }

                return View(clienteViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int? id, string tipoPessoa)
        {
            ClienteViewModel viewModel;

            try
            {
                if (id == null || tipoPessoa == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //Preenche o modelo de acordo com o tipo de pessoa
                if (tipoPessoa == "Física")
                {
                    //Utilização de eager loading para recuperas as informações das entidades Cidade e UF relacionadas a pessoa
                    PessoaFisica pessoaFisica = _pessoaFisicaApp.GetCityStateEagerLoadingById(Convert.ToInt32(id));

                    //Utilizando AutoMapper para atribuição dos dados do model para a viewmodel
                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<PessoaFisica, ClienteViewModel>().ForMember(p => p.Cidade, map => map.UseValue(pessoaFisica.Cidade)).ForMember(p => p.UF, map => map.UseValue(pessoaFisica.Cidade.UF)); });
                    viewModel = config.CreateMapper().Map<PessoaFisica, ClienteViewModel>(pessoaFisica);
                }
                else
                {
                    PessoaJuridica pessoaJuridica = _pessoaJuridicaApp.GetCityStateEagerLoadingById(Convert.ToInt32(id));

                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<PessoaJuridica, ClienteViewModel>().ForMember(p => p.Cidade, map => map.UseValue(pessoaJuridica.Cidade)).ForMember(p => p.UF, map => map.UseValue(pessoaJuridica.Cidade.UF)); });
                    viewModel = config.CreateMapper().Map<PessoaJuridica, ClienteViewModel>(pessoaJuridica);
                }

                if (viewModel == null)
                {
                    return HttpNotFound();
                }

                //Retorna o objeto viewmodel preenchido para a view
                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string tipoPessoa)
        {
            try
            {
                //Preenche o modelo de acordo com o tipo de pessoa e remove do banco de dados
                if (tipoPessoa == "Física")
                {
                    PessoaFisica pessoaFisica = _pessoaFisicaApp.GetCityStateEagerLoadingById(id);
                    _pessoaFisicaApp.Remove(pessoaFisica);
                }
                else
                {
                    PessoaJuridica pessoaJuridica = _pessoaJuridicaApp.GetCityStateEagerLoadingById(id);
                    _pessoaJuridicaApp.Remove(pessoaJuridica);
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pessoaFisicaApp.Dispose();
                _pessoaJuridicaApp.Dispose();
                _cidadeApp.Dispose();
            }
            base.Dispose(disposing);
        }

        //Método que recupera os valores das entidades UF e Cidade e atribui as respectivas ViewBags
        private void LoadViewBags(Cidade cidadeItem)
        {
            try
            {
                //Cria o item "- Selecionar Cidade -" para o dropdownlist de cidades
                Cidade cidade = new Cidade() { ID = 0, Nome = "- Selecionar Cidade -" };
                List<Cidade> cidades = new List<Cidade>();

                //Se já tem o item selecionado para o dropdownlist de Cidades
                if (cidadeItem != null)
                {
                    //Recupera a lista de cidades do respectivo UF
                    cidades = _cidadeApp.GetAll().Where(u => u.UFID == cidadeItem.UFID).ToList();
                    //Preenche a ViewBag de Cidades com a lista de cidades e a cidade selecionada
                    ViewBag.CidadeList = new SelectList(cidades, "ID", "Nome", cidadeItem);
                }
                else
                {
                    //Adiciona o item "- Selecionar Cidade -" na ViewBag de Cidades
                    cidades.Add(cidade);
                    ViewBag.CidadeList = new SelectList(cidades, "ID", "Nome");
                }

                //Preenche as ViewBag dos UFs
                var ufs = _UFApp.GetAll();
                ViewBag.UFList = new SelectList(ufs, "ID", "Nome");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Retorna dados no formato Json, quando houver alguma requisição para a url "ClieteCadastro/GetCities" (Jquery ajax call)
        public JsonResult GetCities(string id)
        {
            List<Cidade> cidades = new List<Cidade>();

            try
            {
                Cidade cidade = new Cidade() { ID = 0, Nome = "- Selecionar cidade -" };

                //Se o parâmetro UFID foi informado
                if (!string.IsNullOrEmpty(id))
                {
                    //Recupera todas as cidades do UF em questão
                    cidades = _cidadeApp.GetAll().ToList().Where(c => c.UFID == int.Parse(id)).ToList();
                }
                else
                {
                    //Se não foi informado o UF, retorna apenas o item padrão
                    cidades.Add(cidade);
                }

                //Retorna uma lista de cidades no formato Json
                return Json(new SelectList(cidades, "ID", "Nome"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
