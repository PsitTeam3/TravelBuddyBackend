using TravelBuddy5.DAL.Repositories;
using TravelBuddy5.Interfaces;
using TravelBuddy5.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TravelBuddy5.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TravelBuddy5.App_Start.NinjectWebCommon), "Stop")]

namespace TravelBuddy5.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using IoC;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using DAL.Interfaces;
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

                RegisterServices(kernel);

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Loads all modules or register needed to be injected
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ITourRepo>().To<TourRepo>();
            kernel.Bind<ICityRepo>().To<CityRepo>();
            kernel.Bind<ICountryRepo>().To<CountryRepo>();
            kernel.Bind<IPOIRepo>().To<POIRepo>();
            kernel.Bind<IUserTourRepo>().To<UserTourRepo>();
            kernel.Bind<IUserPOIRepo>().To<UserPOIRepo>();
            kernel.Bind<IGeoLocationService>().To<GeoLocationService>();
        }        
    }
}
