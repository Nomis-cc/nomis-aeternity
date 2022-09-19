namespace Nomis.Utils.Extensions
{
    /// <summary>
    /// Extension methods for converting DateTime.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert Unix TimeStamp to DateTime.
        /// </summary>
        /// <param name="unixTimeStamp">Unix TimeStamp in string.</param>
        /// <returns><see cref="DateTime"/>.</returns>
        public static DateTime ToDateTime(this string unixTimeStamp)
        {
            long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
            DateTime dt = new DateTime(beginTicks + long.Parse(unixTimeStamp) * 10000, DateTimeKind.Utc).ToLocalTime();
            return dt;
        }
    }
}