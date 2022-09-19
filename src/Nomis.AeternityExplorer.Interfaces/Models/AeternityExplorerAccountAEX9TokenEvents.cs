﻿using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nomis.AeternityExplorer.Interfaces.Models
{
    /// <summary>
    /// AeternityExplorer account AEX-9 token transfer events.
    /// </summary>
    public class AeternityExplorerAccountAEX9TokenEvents :
        IAeternityExplorerTransferList<AeternityExplorerAccountAEX9TokenEvent>
    {
        /// <summary>
        /// Account AEX-9 token event list.
        /// </summary>
        [JsonPropertyName("data")]
        [DataMember(EmitDefaultValue = true)]
        public List<AeternityExplorerAccountAEX9TokenEvent>? Data { get; set; }

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