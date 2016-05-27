[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PenDesign.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PenDesign.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace PenDesign.WebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using NinjectConfig;
    using Ninject;
    using Ninject.Web.Common;
    using PenDesign.Core.Model;
    using PenDesign.Core.Interface.Service.BasicServiceInterface;
    using PenDesign.Service.Base;
    using PenDesign.Data;
    using PenDesign.Core.Interface.Data;
    using PenDesign.Data.Interface;

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
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
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
            //dbcontext
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InRequestScope();

            //Repository
            kernel.Bind<IRepository<AdminMenu>>().To<Repository<AdminMenu>>().InRequestScope();
            kernel.Bind<IRepository<Banner>>().To<Repository<Banner>>().InRequestScope();
            kernel.Bind<IRepository<BannerMapping>>().To<Repository<BannerMapping>>().InRequestScope();
            kernel.Bind<IRepository<Contact>>().To<Repository<Contact>>().InRequestScope();
            kernel.Bind<IRepository<Control>>().To<Repository<Control>>().InRequestScope();
            kernel.Bind<IRepository<ControlMapping>>().To<Repository<ControlMapping>>().InRequestScope();
            kernel.Bind<IRepository<GroupControl>>().To<Repository<GroupControl>>().InRequestScope();
            kernel.Bind<IRepository<Language>>().To<Repository<Language>>().InRequestScope();
            kernel.Bind<IRepository<NewsCategory>>().To<Repository<NewsCategory>>().InRequestScope();
            kernel.Bind<IRepository<NewsCategoryMapping>>().To<Repository<NewsCategoryMapping>>().InRequestScope();
            kernel.Bind<IRepository<News>>().To<Repository<News>>().InRequestScope();
            kernel.Bind<IRepository<NewsMapping>>().To<Repository<NewsMapping>>().InRequestScope();
            kernel.Bind<IRepository<NewsDraft>>().To<Repository<NewsDraft>>().InRequestScope();
            kernel.Bind<IRepository<Project>>().To<Repository<Project>>().InRequestScope();
            kernel.Bind<IRepository<ProjectMapping>>().To<Repository<ProjectMapping>>().InRequestScope();
            kernel.Bind<IRepository<ProjectImage>>().To<Repository<ProjectImage>>().InRequestScope();
            kernel.Bind<IRepository<ProjectImageMapping>>().To<Repository<ProjectImageMapping>>().InRequestScope();
            kernel.Bind<IRepository<Config>>().To<Repository<Config>>().InRequestScope();
            kernel.Bind<IRepository<OtherPageSEO>>().To<Repository<OtherPageSEO>>().InRequestScope();
            kernel.Bind<IRepository<UserInfo>>().To<Repository<UserInfo>>().InRequestScope();


            //Service
            kernel.Bind<IAdminMenuService>().To<AdminMenuService>().InRequestScope();
            kernel.Bind<IBannerService>().To<BannerService>().InRequestScope();
            kernel.Bind<IBannerMappingService>().To<BannerMappingService>().InRequestScope();
            kernel.Bind<IContactService>().To<ContactService>().InRequestScope();
            kernel.Bind<IControlService>().To<ControlService>().InRequestScope();
            kernel.Bind<IControlMappingService>().To<ControlMappingService>().InRequestScope();
            kernel.Bind<IGroupControlService>().To<GroupControlService>().InRequestScope();
            kernel.Bind<ILanguageService>().To<LanguageService>().InRequestScope();
            kernel.Bind<INewsCategoryService>().To<NewsCategoryService>().InRequestScope();
            kernel.Bind<INewsCategoryMappingService>().To<NewsCategoryMappingService>().InRequestScope();
            kernel.Bind<INewsService>().To<NewsService>().InRequestScope();
            kernel.Bind<INewsMappingService>().To<NewsMappingService>().InRequestScope();
            kernel.Bind<INewsDraftService>().To<NewsDraftService>().InRequestScope();
            kernel.Bind<IProjectService>().To<ProjectService>().InRequestScope();
            kernel.Bind<IProjectMappingService>().To<ProjectMappingService>().InRequestScope();
            kernel.Bind<IProjectImageService>().To<ProjectImageService>().InRequestScope();
            kernel.Bind<IProjectImageMappingService>().To<ProjectImageMappingService>().InRequestScope();
            kernel.Bind<IConfigService>().To<ConfigService>().InRequestScope();
            kernel.Bind<IOtherPageSEOService>().To<OtherPageSEOService>().InRequestScope();
            kernel.Bind<IUserInfoService>().To<UserInfoService>().InRequestScope();
        }        
    }
}
