
using System.Globalization;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace WebApi.Utility
{

    public static class SafeDivision
    {
        public static decimal SafeDiv<T>(this T dividend, T divisor)
        {
            try
            {
                if (dividend == null || divisor == null || (dynamic)divisor! == 0) return 0;
                return 1m * (dynamic)dividend! / (dynamic)divisor!;
            }
            catch
            {
                return 0;
            }
        }
    }
}