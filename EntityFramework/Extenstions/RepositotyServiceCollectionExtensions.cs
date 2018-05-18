using Core.Repository;
using Web.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositotyServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositotyServiceCollectionExtensions(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRepository,EfRepository>();

            return serviceCollection;
        }
    }
}
