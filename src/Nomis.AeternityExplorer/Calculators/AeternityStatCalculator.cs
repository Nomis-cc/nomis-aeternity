using System.Numerics;

using Nomis.AeternityExplorer.Extensions;
using Nomis.AeternityExplorer.Interfaces.Models;
using Nomis.Utils.Extensions;

namespace Nomis.AeternityExplorer.Calculators
{
    /// <summary>
    /// Aeternity wallet stats calculator.
    /// </summary>
    internal sealed class AeternityStatCalculator
    {
        private readonly string _address;
        private readonly decimal _balance;
        private readonly IEnumerable<AeternityExplorerAccountNormalTransaction> _transactions;
        private readonly IEnumerable<AeternityExplorerAccountInternalTransaction> _internalTransactions;
        private readonly IEnumerable<AeternityExplorerAccountAEX141TokenEvent> _aex141TokenTransfers;
        private readonly IEnumerable<AeternityExplorerAccountAEX9TokenEvent> _aex9TokenTransfers;

        public AeternityStatCalculator(
            string address,
            decimal balance,
            IEnumerable<AeternityExplorerAccountNormalTransaction> transactions,
            IEnumerable<AeternityExplorerAccountInternalTransaction> internalTransactions,
            IEnumerable<AeternityExplorerAccountAEX141TokenEvent> aex141TokenTransfers,
            IEnumerable<AeternityExplorerAccountAEX9TokenEvent> aex9TokenTransfers)
        {
            _address = address;
            _balance = balance;
            _transactions = transactions;
            _internalTransactions = internalTransactions;
            _aex141TokenTransfers = aex141TokenTransfers;
            _aex9TokenTransfers = aex9TokenTransfers;
        }

        private int GetWalletAge()
        {
            var firstTransaction = _transactions.First();
            return (int)((DateTime.UtcNow - firstTransaction.MicroTime.ToString().ToDateTime()).TotalDays / 30);
        }

        private IEnumerable<double> GetTransactionsIntervals()
        {
            var result = new List<double>();
            DateTime? lastDateTime = null;
            foreach (var transaction in _transactions.OrderByDescending(x => x.MicroTime))
            {
                var transactionDate = transaction.MicroTime.ToString().ToDateTime();
                if (!lastDateTime.HasValue)
                {
                    lastDateTime = transactionDate;
                    continue;
                }

                var interval = Math.Abs((transactionDate - lastDateTime.Value).TotalHours);
                lastDateTime = transactionDate;
                result.Add(interval);
            }

            return result;
        }

        // TODO - check this!
        private BigInteger GetTokensSum(IEnumerable<AeternityExplorerAccountAEX141TokenEvent> tokenList)
        {
            var transactions = tokenList.Select(x => x.BlockHeight).ToHashSet();
            var result = new BigInteger();
            foreach (var st in _internalTransactions.Where(x => transactions.Contains(x.Height)))
            {
                result += st.Amount;
            }

            return result;
        }

        public AeternityWalletStats GetStats()
        {
            if (!_transactions.Any())
            {
                return new()
                {
                    NoData = true
                };
            }

            var intervals = GetTransactionsIntervals().ToList();
            if (!intervals.Any())
            {
                return new()
                {
                    NoData = true
                };
            }

            var monthAgo = DateTime.Now.AddMonths(-1);

            var soldTokens = _aex141TokenTransfers.Where(x => x.Sender?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true).ToList();
            var soldSum = GetTokensSum(soldTokens);

            var soldTokensIds = soldTokens.Select(x => x.GetTokenUid());
            var buyTokens = _aex141TokenTransfers.Where(x => x.Recipient?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true && soldTokensIds.Contains(x.GetTokenUid()));
            var buySum = GetTokensSum(buyTokens);

            var buyNotSoldTokens = _aex141TokenTransfers.Where(x => x.Recipient?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true && !soldTokensIds.Contains(x.GetTokenUid()));
            var buyNotSoldSum = GetTokensSum(buyNotSoldTokens);

            var holdingTokens = _aex141TokenTransfers.Count() - soldTokens.Count;
            var nftWorth = buySum == 0 ? 0 : (decimal)soldSum / (decimal)buySum * (decimal)buyNotSoldSum;
            var contractsCreated = _transactions.Count(x => x.Transaction?.Type?.Equals("contract_create", StringComparison.CurrentCultureIgnoreCase) == true);
            var totalTokens = _aex9TokenTransfers.Select(x => x.ContractId).Distinct();

            return new()
            {
                Balance = _balance.ToAeternity(),
                WalletAge = GetWalletAge(),
                TotalTransactions = _transactions.Count(),
                MinTransactionTime = intervals.Min(),
                MaxTransactionTime = intervals.Max(),
                AverageTransactionTime = intervals.Average(),
                WalletTurnover = _transactions.Sum(x => (decimal?)x.Transaction?.Amount ?? 0).ToAeternity(),
                LastMonthTransactions = _transactions.Count(x => x.MicroTime.ToString().ToDateTime() > monthAgo),
                TimeFromLastTransaction = (int)((DateTime.UtcNow - _transactions.Last().MicroTime.ToString().ToDateTime()).TotalDays / 30),
                NftHolding = holdingTokens,
                NftTrading = (soldSum - buySum).ToAeternity(),
                NftWorth = nftWorth.ToAeternity(),
                DeployedContracts = contractsCreated,
                TokensHolding = totalTokens.Count()
            };
        }
    }
}