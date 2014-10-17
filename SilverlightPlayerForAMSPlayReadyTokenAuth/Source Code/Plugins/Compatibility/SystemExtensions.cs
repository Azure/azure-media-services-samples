namespace System
{
    public class SystemExtensions
    {
        public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
        {
            try
            {
                result = (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
                return true;
            }
            catch 
            {
                result = default(TEnum);
                return false;
            }
        }

        public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct
        {
            return TryParse(value, false, out result);
        }

        public static bool IsNullOrWhiteSpace(string value) 
        {
            return value == null || string.IsNullOrEmpty(value.Trim());
        }
    }
}
