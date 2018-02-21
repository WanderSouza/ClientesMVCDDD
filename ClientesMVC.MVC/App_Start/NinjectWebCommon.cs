[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ClientesMVC.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ClientesMVC.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace ClientesMVC.MVC.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using ClientesMVC.Application;
    using ClientesMVC.Application.Interface;
    using ClientesMVC.Domain.Interfaces.Repositories;
    using ClientesMVC.Domain.Interfaces.Services;
    using ClientesMVC.Domain.Services;
    using ClientesMVC.Infra.Data.Repositories;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind<IPessoaFisicaAppService>().To<PessoaFisicaAppService>();
            kernel.Bind<IPessoaJuridicaAppService>().To<PessoaJuridicaAppService>();
            kernel.Bind<ICidadeAppService>().To<CidadeAppService>();
            kernel.Bind<IUFAppService>().To<UFAppService>();

            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind<IPessoaFisicaService>().To<PessoaFisicaService>();
            kernel.Bind<IPessoaJuridicaService>().To<PessoaJuridicaService>();
            kernel.Bind<ICidadeService>().To<CidadeService>();
            kernel.Bind<IUFService>().To<UFService>();

            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<IPessoaFisicaRepository>().To<PessoaFisicaRepository>();
            kernel.Bind<IPessoaJuridicaRepository>().To<PessoaJuridicaRepository>();
            kernel.Bind<ICidadeRepository>().To<CidadeRepository>();
            kernel.Bind<IUFRepository>().To<UFRepository>();
        }        
    }
}
