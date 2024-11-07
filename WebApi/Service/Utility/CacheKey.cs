
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace WebApi.Utility
{

    public static class CacheKey
    {
        public enum Prefix
        {
            PRODUCT,
            PRODUCTPAGE
        }
        public static string Get(Prefix pref, params object[] param)
        {
            return $"{pref}_{string.Join('_', param)}";
        }
    }
}