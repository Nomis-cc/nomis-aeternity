using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.AeternityExplorer.Interfaces;
using Nomis.AeternityExplorer.Interfaces.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.AeternityExplorer.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add AeternityExplorer service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddAeternityExplorerService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<AeternityExplorerSettings>(configuration);

            return services
                .AddTransient<IAeternityExplorerClient, AeternityExplorerClient>()
                .AddTransientInfrastructureService<IAeternityExplorerService, AeternityExplorerService>();
        }
    }
}