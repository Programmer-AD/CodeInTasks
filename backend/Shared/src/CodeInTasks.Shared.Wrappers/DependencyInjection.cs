using CodeInTasks.Shared.Wrappers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Shared.Wrappers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWrappers(this IServiceCollection services)
        {
            services.AddSingleton<IJsonSerializer, Serialization.JsonSerializerWrapper>();

            return services;
        }
    }
}
