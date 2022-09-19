using Nomis.AeternityExplorer.Interfaces.Models;
using Nomis.Utils.Wrapper;
using Nomis.Web.Client.Common.Managers;

namespace Nomis.Web.Client.Aeternity.Managers
{
    /// <summary>
    /// Aeternity manager.
    /// </summary>
    public interface IAeternityManager :
        IManager
    {
        /// <summary>
        /// Get aeternity wallet score.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        /// <returns>Returns result of <see cref="AeternityWalletScore"/>.</returns>
        Task<IResult<AeternityWalletScore>> GetWalletScoreAsync(string address);
    }
}