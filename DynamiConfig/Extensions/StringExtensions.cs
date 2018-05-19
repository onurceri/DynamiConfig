namespace DynamiConfig.Extensions
{
    public static class StringExtension
    {
        public static bool IsNotNull(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                return true;
            return false;
        }
    }
}
