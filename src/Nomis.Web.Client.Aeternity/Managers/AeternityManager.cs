using Microsoft.Extensions.Options;
using Nomis.AeternityExplorer.Interfaces.Models;
using Nomis.Utils.Wrapper;
using Nomis.Web.Client.Common.Extensions;
using Nomis.Web.Client.Common.Settings;
using Nomis.Web.Client.Aeternity.Routes;

namespace Nomis.Web.Client.Aeternity.Managers
{
    /// <inheritdoc cref="IAeternityManager" />
    public class AeternityManager :
        IAeternityManager
    {
        private readonly HttpClient _httpClient;
        private readonly AeternityEndpoints _endpoints;

        /// <summary>
        /// Initialize <see cref="AeternityManager"/>.
        /// </summary>
        /// <param name="webApiSettings"><see cref="WebApiSettings"/>.</param>
        public AeternityManager(
            IOptions<WebApiSettings> webApiSettings)
        {
            _httpClient = new()
            {
                BaseAddress = new(webApiSettings.Value?.ApiBaseUrl ?? throw new ArgumentNullException(nameof(webApiSettings.Value.ApiBaseUrl)))
            };
            _endpoints = new(webApiSettings.Value?.ApiBaseUrl ?? throw new ArgumentNullException(nameof(webApiSettings.Value.ApiBaseUrl)));
        }

        /// <inheritdoc />
        public async Task<IResult<AeternityWalletScore>> GetWalletScoreAsync(string address)
        {
            var response = await _httpClient.GetAsync(_endpoints.GetWalletScore(address));
            return await response.ToResultAsync<AeternityWalletScore>();
        }
    }
}