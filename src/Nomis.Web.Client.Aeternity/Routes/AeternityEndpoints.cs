using Nomis.Web.Client.Common.Routes;

namespace Nomis.Web.Client.Aeternity.Routes
{
    /// <summary>
    /// Aeternity endpoints.
    /// </summary>
    public class AeternityEndpoints :
        BaseEndpoints
    {
        /// <summary>
        /// Initialize <see cref="AeternityEndpoints"/>.
        /// </summary>
        /// <param name="baseUrl">Aeternity API base URL.</param>
        public AeternityEndpoints(string baseUrl)
            : base(baseUrl)
        {
        }

        /// <inheritdoc/>
        public override string Blockchain => "aeternity";
    }
}