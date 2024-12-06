using TrackerEF.Extensions;

namespace TrackerEF.Models;

public class Smartphone : Product
{
    public Smartphone() { }

    public Smartphone(
        float price,
        Currency currency,
        DateTime purchaceDate,
        string brand,
        string model,
        Country country
        ) : base(price, currency, purchaceDate, brand, model, country)
    {
        PriceUSD = this.ConvertToUSD(price);
    }
}
