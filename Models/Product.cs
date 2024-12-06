namespace TrackerEF.Models;

public class Product : Asset
{
    public float PriceUSD { get; set; }
    public float Price { get; set; }
    public Currency Currency { get; set; }
    public DateTime PurchaceDate { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public Country Country { get; set; }

    public Product() { }

    public Product(float price, Currency currency, DateTime purchaceDate, string brand, string model, Country country)
    {
        Price = price;
        Currency = currency;
        PurchaceDate = purchaceDate;
        Brand = brand;
        Model = model;
        Country = country;
    }

    public string GetPriceInDollarAsString() => Math.Round((decimal)PriceUSD, 2).ToString();

    public string GetFormattedLocalPrice() => $"{Currency} {Math.Round((decimal)Price, 2)}";

    public string GetDateAsString() => DateOnly.FromDateTime(PurchaceDate).ToString();

    public string[] GetPropertyValues()
    {
        return [
            Id.ToString(),
            Country.ToString(),
            GetAssetName(),
            Brand,
            Model,
            GetPriceInDollarAsString(),
            GetFormattedLocalPrice(),
            GetDateAsString()
        ];
    }

}
