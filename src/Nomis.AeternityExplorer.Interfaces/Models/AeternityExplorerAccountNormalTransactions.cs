using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nomis.AeternityExplorer.Interfaces.Models
{
    /// <summary>
    /// AeternityExplorer account normal transactions.
    /// </summary>
    public class AeternityExplorerAccountNormalTransactions :
        IAeternityExplorerTransferList<AeternityExplorerAccountNormalTransaction>
    {
        /// <summary>
        /// Account normal transaction list.
        /// </summary>
        [JsonPropertyName("data")]
        [DataMember(EmitDefaultValue = true)]
        public List<AeternityExplorerAccountNormalTransaction>? Data { get; set; } = new();

        /// <summary>
        /// Next list of transfers.
        /// </summary>
        [JsonPropertyName("next")]
        public string? Next { get; set; }

        /// <summary>
        /// Previous list of transfers.
        /// </summary>
        [JsonPropertyName("prev")]
        public string? Prev { get; set; }
    }
}