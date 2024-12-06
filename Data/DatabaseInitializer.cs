using TrackerEF.Models;

namespace TrackerEF.Data;

public static class DatabaseInitializer
{
    public static void Init(TrackerContext context, AssetTracker tracker)
    {
        if (context.Assets.Any())
            return; // Database has been seeded

        tracker.AddAsset(new Smartphone(200, Currency.USD, DateTime.Now.AddMonths(-36 + 4), "Motorola", "X3", Country.USA));
        tracker.AddAsset(new Smartphone(400, Currency.USD, DateTime.Now.AddMonths(-36 + 5), "Motorola", "X3", Country.USA));
        tracker.AddAsset(new Smartphone(400, Currency.USD, DateTime.Now.AddMonths(-36 + 10), "Motorola", "X2", Country.USA));
        tracker.AddAsset(new Smartphone(4500, Currency.SEK, DateTime.Now.AddMonths(-36 + 6), "Samsung", "Galaxy 10", Country.SWEDEN));
        tracker.AddAsset(new Smartphone(4500, Currency.SEK, DateTime.Now.AddMonths(-36 + 7), "Samsung", "Galaxy 10", Country.SWEDEN));
        tracker.AddAsset(new Smartphone(3000, Currency.SEK, DateTime.Now.AddMonths(-36 + 4), "Sony", "XPeria 7", Country.SWEDEN));
        tracker.AddAsset(new Smartphone(3000, Currency.SEK, DateTime.Now.AddMonths(-36 + 5), "Sony", "XPeria 7", Country.SWEDEN));
        tracker.AddAsset(new Smartphone(220, Currency.EUR, DateTime.Now.AddMonths(-36 + 12), "Siemens", "Brick", Country.GERMANY));
        tracker.AddAsset(new Computer(100, Currency.USD, DateTime.Now.AddMonths(-38), "Dell", "Desktop 900", Country.USA));
        tracker.AddAsset(new Computer(100, Currency.USD, DateTime.Now.AddMonths(-37), "Dell", "Desktop 900", Country.USA));
        tracker.AddAsset(new Computer(300, Currency.USD, DateTime.Now.AddMonths(-36 + 1), "Lenovo", "X100", Country.USA));
        tracker.AddAsset(new Computer(300, Currency.USD, DateTime.Now.AddMonths(-36 + 4), "Lenovo", "X200", Country.USA));
        tracker.AddAsset(new Computer(500, Currency.USD, DateTime.Now.AddMonths(-36 + 9), "Lenovo", "X300", Country.USA));
        tracker.AddAsset(new Computer(1500, Currency.SEK, DateTime.Now.AddMonths(-36 + 7), "Dell", "Optiplex 100", Country.SWEDEN));
        tracker.AddAsset(new Computer(1400, Currency.SEK, DateTime.Now.AddMonths(-36 + 8), "Dell", "Optiplex 200", Country.SWEDEN));
        tracker.AddAsset(new Computer(1300, Currency.SEK, DateTime.Now.AddMonths(-36 + 9), "Dell", "Optiplex 300", Country.SWEDEN));
        tracker.AddAsset(new Computer(1600, Currency.EUR, DateTime.Now.AddMonths(-36 + 14), "Asus", "ROG 600", Country.GERMANY));
        tracker.AddAsset(new Computer(1200, Currency.EUR, DateTime.Now.AddMonths(-36 + 4), "Asus", "ROG 500", Country.GERMANY));
        tracker.AddAsset(new Computer(1200, Currency.EUR, DateTime.Now.AddMonths(-36 + 3), "Asus", "ROG 500", Country.GERMANY));
        tracker.AddAsset(new Computer(1300, Currency.EUR, DateTime.Now.AddMonths(-36 + 2), "Asus", "ROG 500", Country.GERMANY));
    }
}
