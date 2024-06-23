

using Microsoft.Extensions.DependencyInjection;

namespace TinyFileExplorer.Configurations
{
    public static class TinyFileExplorerServiceExtensions
    {
        public static IServiceCollection AddTinyFileExplorer(this IServiceCollection services, Action<TinyFileExplorerServiceOptions> configureOptions) 
        {

            var options = new TinyFileExplorerServiceOptions();
            configureOptions(options);
            services.Configure(configureOptions);
            services.AddSingleton(options);
            return services;
        }
    }
}
