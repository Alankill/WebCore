using Application.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class  ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationInterface(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITaskAppService,TaskAppService>();

            return serviceCollection;
        }

    }
}
