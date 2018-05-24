using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Web.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositotyServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>),typeof(EfRepository<>));
            AddContextDependency(serviceCollection);
            return serviceCollection;
        }

        private static IServiceCollection AddContextDependency(IServiceCollection serviceCollection)
        {
            var Repositories = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
                        t.IsClass && t.IsPublic &&  
                        t.Namespace == "Web.EntityFramework.Repository");

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
