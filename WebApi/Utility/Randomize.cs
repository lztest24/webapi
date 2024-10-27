
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace WebApi.Utility
{

    public static class Randomize
    {
        public static string GetRandomProductName()
        {
            return RandomizerFactory.GetRandomizer(new FieldOptionsTextWords{Min = 2, Max = 5}).Generate() ?? string.Empty;
        }
        public static string GetRandomProductDescription()
        {
            return RandomizerFactory.GetRandomizer(new FieldOptionsTextWords{Min = 10, Max = 30}).Generate() ?? string.Empty;
        }
        
        public static decimal GetRandomProductPrice()
        {
            return (decimal)(Math.Round(new Random().NextDouble(),2) + new Random().Next(10,10000));
        }
    }
}