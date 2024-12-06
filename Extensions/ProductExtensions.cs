using TrackerEF.Models;

namespace TrackerEF.Extensions;

public static class ProductExtensions
{
    public static float ConvertToUSD(this Product product, float price)
    {
        var convertedValue = product.Currency switch
        {
            Currency.USD => price,
            Currency.SEK => Convert.ToSingle(price / 10.39),
            Currency.EUR => Convert.ToSingle(price * 1.0957),
            _ => throw new ArgumentException($"This currency, {product.Currency} is not supported."),
        };

        return convertedValue;
    }
}
