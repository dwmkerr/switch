namespace SwitchCore.Extensions
{
    /// <summary>
    /// Extensions for the string class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determine whether a string ends with another string.
        /// </summary>
        /// <param name="me">The source string.</param>
        /// <param name="value">The value to check.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore case.</param>
        /// <returns>True if the string 'me' ends with 'value'.</returns>
        public static bool EndsWith(string me, string value, bool ignoreCase)
        {
            //  Trivial case - the string is too small.
            if (me.Length < value.Length)
                return false;

            //  Get the end of the string.
            string end = me.Substring(me.Length - value.Length);

            //  Compare, case insensitive.
            return string.Compare(end, value, ignoreCase) == 0;
        }
    }
}
