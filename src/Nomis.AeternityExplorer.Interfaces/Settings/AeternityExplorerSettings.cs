using Nomis.Utils.Contracts.Common;

namespace Nomis.AeternityExplorer.Interfaces.Settings
{
    /// <summary>
    /// AeternityExplorer settings.
    /// </summary>
    public class AeternityExplorerSettings :
        ISettings
    {
        /// <summary>
        /// API base URL.
        /// </summary>
        /// <remarks>
        /// <see href="https://github.com/aeternity/ae_mdw#hosted-infrastructure"/>
        /// </remarks>
        public string? ApiBaseUrl { get; set; }
    }
}