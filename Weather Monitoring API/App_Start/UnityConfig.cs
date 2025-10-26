using AutoMapper;
using BLL.Mapping;
using BLL.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Weather_Monitoring_API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            IMapper mapper = AutoMapperConfig.Initialize();

            container.RegisterInstance<IMapper>(mapper);
    
            container.RegisterType<CountryService>();
            container.RegisterType<StateService>();
            container.RegisterType<CityService>();
            container.RegisterType<AreaService>();
            container.RegisterType<LocationHierarchyService>();

       
            container.RegisterType<AlertService>();
            container.RegisterType<LocationService>();
            container.RegisterType<WeatherRecordService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}