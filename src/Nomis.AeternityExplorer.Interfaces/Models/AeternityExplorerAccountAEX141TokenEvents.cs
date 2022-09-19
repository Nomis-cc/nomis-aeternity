using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nomis.AeternityExplorer.Interfaces.Models
{
    /// <summary>
    /// AeternityExplorer account AEX-141 token transfer events.
    /// </summary>
    public class AeternityExplorerAccountAEX141TokenEvents :
        IAeternityExplorerTransferList<AeternityExplorerAccountAEX141TokenEvent>
    {
        /// <summary>
        /// Account AEX-141 token event list.
        /// </summary>
        [JsonPropertyName("data")]
        [DataMember(EmitDefaultValue = true)]
        public List<AeternityExplorerAccountAEX141TokenEvent>? Data { get; set; }

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