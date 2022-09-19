using Nomis.AeternityExplorer.Interfaces.Models;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.AeternityExplorer.Interfaces
{
    /// <summary>
    /// AeternityExplorer service.
    /// </summary>
    public interface IAeternityExplorerService :
        IInfrastructureService
    {
        /// <summary>
        /// Client for interacting with AeternityExplorer API.
        /// </summary>
        public IAeternityExplorerClient Client { get; }

        /// <summary>
        /// Get aeternity wallet stats by address.
        /// </summary>
        /// <param name="address">Aeternity wallet address.</param>
        /// <returns>Returns <see cref="AeternityWalletScore"/> result.</returns>
        public Task<Result<AeternityWalletScore>> GetWalletStatsAsync(string address);
    }
}