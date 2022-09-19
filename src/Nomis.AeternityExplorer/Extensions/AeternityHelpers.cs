using System.Numerics;

using Nomis.AeternityExplorer.Interfaces.Models;

namespace Nomis.AeternityExplorer.Extensions
{
    /// <summary>
    /// Extension methods for aeternity.
    /// </summary>
    public static class AeternityHelpers
    {
        /// <summary>
        /// Convert Wei value to Aeternity.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Aeternity.</returns>
        public static decimal ToAeternity(this string valueInWei)
        {
            if (!decimal.TryParse(valueInWei, out decimal wei))
            {
                return 0;
            }

            return wei.ToAeternity();
        }

        /// <summary>
        /// Convert Wei value to Aeternity.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Aeternity.</returns>
        public static decimal ToAeternity(this long valueInWei)
        {
            return valueInWei * (decimal)0.000_000_000_000_000_001;
        }

        /// <summary>
        /// Convert Wei value to Aeternity.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Aeternity.</returns>
        public static decimal ToAeternity(this BigInteger valueInWei)
        {
            return (decimal)valueInWei * (decimal)0.000_000_000_000_000_001;
        }

        /// <summary>
        /// Convert Wei value to Aeternity.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Aeternity.</returns>
        public static decimal ToAeternity(this decimal valueInWei)
        {
            return new BigInteger(valueInWei).ToAeternity();
        }

        /// <summary>
        /// Get token UID based on it ContractAddress and Id.
        /// </summary>
        /// <param name="token">Token info.</param>
        /// <returns>Returns token UID.</returns>
        public static string GetTokenUid(this AeternityExplorerAccountAEX141TokenEvent token)
        {
            return token.ContractId + "_" + token.TokenId;
        }
    }
}