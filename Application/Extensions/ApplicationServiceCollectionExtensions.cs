using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class  ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection serviceCollection)
        {
            AddAppServiceDependency(serviceCollection);
            return serviceCollection;
        }

        public static IServiceCollection AddAppServiceDependency(IServiceCollection serviceCollection)
        {
            var Repositories = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
                        t.IsClass && !t.IsNested && t.IsPublic &&
                        t.Namespace == "Application.AppService"
                        );

            foreach (var type in Repositories)
            {
                var allInterfaces = type.GetInterfaces();
                var mainInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));
                foreach (var itype in mainInterfaces)
                {
                    serviceCollection.AddScoped(itype, type);
                }
            }
            return serviceCollection;
        }

    }
}
