using System.Net;

using Nomis.AeternityExplorer.Calculators;
using Nomis.AeternityExplorer.Interfaces;
using Nomis.AeternityExplorer.Interfaces.Models;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.AeternityExplorer
{
    /// <inheritdoc cref="IAeternityExplorerService"/>
    internal sealed class AeternityExplorerService :
        IAeternityExplorerService,
        ITransientService
    {
        /// <summary>
        /// Initialize <see cref="AeternityExplorerService"/>.
        /// </summary>
        /// <param name="client"><see cref="IAeternityExplorerClient"/>.</param>
        public AeternityExplorerService(
            IAeternityExplorerClient client)
        {
            Client = client;
        }

        /// <inheritdoc/>
        public IAeternityExplorerClient Client { get; }

        /// <inheritdoc/>
        public async Task<Result<AeternityWalletScore>> GetWalletStatsAsync(string address)
        {
            var balanceWei = (await Client.GetBalanceAsync(address)).Balance;
            var transactions = (await Client.GetTransactionsAsync<AeternityExplorerAccountNormalTransactions, AeternityExplorerAccountNormalTransaction>(address)).ToList();
            var internalTransactions = (await Client.GetTransactionsAsync<AeternityExplorerAccountInternalTransactions, AeternityExplorerAccountInternalTransaction>(address)).ToList();
            var aex9FromTokens = (await Client.GetTransactionsAsync<AeternityExplorerAccountAEX9TokenEvents, AeternityExplorerAccountAEX9TokenEvent>(address, true)).ToList();
            var aex9ToTokens = (await Client.GetTransactionsAsync<AeternityExplorerAccountAEX9TokenEvents, AeternityExplorerAccountAEX9TokenEvent>(address, false)).ToList();
            var aex9Tokens = aex9FromTokens.Union(aex9ToTokens).ToList();
            var aex141FromTokens = (await Client.GetTransactionsAsync<AeternityExplorerAccountAEX141TokenEvents, AeternityExplorerAccountAEX141TokenEvent>(address, true)).ToList();
            var aex141ToTokens = (await Client.GetTransactionsAsync<AeternityExplorerAccountAEX141TokenEvents, AeternityExplorerAccountAEX141TokenEvent>(address, false)).ToList();
            var aex141Tokens = aex141FromTokens.Union(aex141ToTokens).ToList();

            var walletStats = new AeternityStatCalculator(
                    address,
                    (decimal)balanceWei,
                    transactions,
                    internalTransactions,
                    aex141Tokens,
                    aex9Tokens)
                .GetStats();

            return await Result<AeternityWalletScore>.SuccessAsync(new()
            {
                Stats = walletStats,
                Score = walletStats.GetScore()
            }, "Got aeternity wallet score.");
        }
    }
}