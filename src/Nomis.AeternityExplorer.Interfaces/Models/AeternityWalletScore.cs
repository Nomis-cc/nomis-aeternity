namespace Nomis.AeternityExplorer.Interfaces.Models
{
    /// <summary>
    /// Aeternity wallet score.
    /// </summary>
    public class AeternityWalletScore
    {
        /// <summary>
        /// Nomis Score in range of [0; 1].
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Additional stat data used in score calculations.
        /// </summary>
        public AeternityWalletStats? Stats { get; set; }
    }
}