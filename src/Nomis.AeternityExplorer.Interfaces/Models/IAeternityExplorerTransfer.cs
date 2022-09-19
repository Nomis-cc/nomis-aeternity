using System.Text.Json.Serialization;

namespace Nomis.AeternityExplorer.Interfaces.Models
{
    /// <summary>
    /// AeternityExplorer transfer.
    /// </summary>
    public interface IAeternityExplorerTransfer
    {
        /// <summary>
        /// List of transfers.
        /// </summary>
        [JsonPropertyName("tx")]
        public AeternityExplorerTransferTx? Transaction { get; set; }
    }
}