using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using BusinessDataModels.UnitOfWork;
using BusinessServices;
using System.Web.Http.Cors;
using System.Web.Http;
using BusinessDataModels;
using BusinessDataModels.Repository;
using AutoMapper;
using GoodExample.Mappings;

namespace GoodExample.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            AutoMapperConfiguration.InitializeAutoMapper();
        }

        private static void SetAutofacContainer()
        {
            // Autofac for the unti container

            var builder = new ContainerBuilder();

            var cors = new EnableCorsAttribute("http://localhost:65162/", "*", "GET, POST, PUT, DELETE, OPTIONS"); // Origins, headers, methods

            var config = System.Web.Http.GlobalConfiguration.Configuration;

            config.EnableCors(cors);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();

            builder.RegisterType<UnitofWork>().As<IUnitofWork>().InstancePerRequest();
            builder.RegisterType<UnitofWork>().As<IUnityofWorkAsync>().InstancePerRequest();
            builder.RegisterType<DBFactory>().As<IDBFactory>().InstancePerRequest();
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(PhoneBookRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(PhoneBookService).Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();            

            //builder.RegisterType<PromotionRepository>().As<IPromotionRepository>().SingleInstance();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            
        }
    }
}