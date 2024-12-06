using TrackerEF.Extensions;

namespace TrackerEF.Models;

public class Computer : Product
{
    public Computer() { }

    public Computer(
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
