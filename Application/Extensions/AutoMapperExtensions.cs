using AutoMapper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperInterface(this IServiceCollection serviceCollection)
        {
            var config = new MapperConfiguration(m =>
            {
                //m.CreateMap<Common.DTO.UserDTO, Common.Domain.Entity.User>();
            });
            var mapper=config.CreateMapper();
            serviceCollection.AddSingleton<IMapper>(mapper);
            return serviceCollection;
        }
    }
}
