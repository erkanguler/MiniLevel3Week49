using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrackerEF.Models;
using TrackerEF.Data;

namespace TrackerEF;

class App
{
    public const int THREE_YEAR = 365 * 3;
    public const string MAIN_MENU =
    """
    
    ----- Main Menu -----
    Enter 1 to add a new asset    
    Enter 2 to edit an asset (only model of an asset can be edited)
    Enter 3 to delete an asset
    """;
    public const string SUBMENU_EDITING =
    """

    Enter 'q' to quit
    Press 'Enter' key to continue editing.
    Enter 3 return main menu
    """;
    public const string SUBMENU_DELETING =
    """

    Enter 'q' to quit
    Press 'Enter' key to continue deleting.
    Enter 3 return main menu"
    """;

    public static void Main()
    {
        // Manage configuration for DbContext (TrackerContext)
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<TrackerContext>()
            .UseSqlServer(config.GetConnectionString("AssetsDB"))
            .Options;

        var context = new TrackerContext(options);
        var tracker = new AssetTracker();

        // Seed database if it is not seeded
        DatabaseInitializer.Init(context, tracker);

        context.Add(tracker);
        context.SaveChanges();

        PrintAssets([.. context.Assets]);
        PrintTextWithColor($"\nNumber of assets in the database: {context.Assets.Count()}", ConsoleColor.Green);

        while (true)
        {
        GoToMainMenu:
            PrintTextWithColor(MAIN_MENU, ConsoleColor.Blue);
            string? input = GetInput();

            if (input is null)
                continue;

            exitIfUserWants(input);

            if (IsTokenEqual(input, "1")) // Add a new asset
            {
                while (true)
                {
                    PrintTextWithColor("\nEnter 'q' to quit.\n", ConsoleColor.Blue);

                    // Enable selection of an asset type for user.
                    int? productClass;
                    while (true)
                    {
                        string m = "Enter '1' or '2' to choose a Computer or Smartphone as a asset: \n1 = Computer\n2 = Smartphone\n";
                        PrintTextWithColor(m, ConsoleColor.Blue);
                        string? _productClass = GetInput();

                        if (_productClass is null)
                            continue;

                        exitIfUserWants(_productClass);

                        productClass = GetInputAsInteger(_productClass, 1, 2);

                        if (productClass is null)
                            continue;

                        break;
                    }

                    // Gets local price from user
                    float price;
                    while (true)
                    {
                        PrintTextWithColor("Enter a price for your asset: ", ConsoleColor.Blue);
                        string? _price = GetInput();

                        if (_price is null)
                            continue;

                        exitIfUserWants(_price);

                        float? price2 = GetPriceAsFloat(_price);

                        if (price2 is null)
                            continue;

                        price = (float)price2;

                        break;
                    }

                    // Gets local currency from user
                    int? currency;
                    while (true)
                    {
                        string m = "Enter '1', '2' or '3' to choose a currency: \n1 = EUR for Germany\n2 = SEK for Sweden\n3 = USD for United States\n";
                        PrintTextWithColor(m, ConsoleColor.Blue);
                        string? _currency = GetInput();

                        if (_currency is null)
                            continue;

                        exitIfUserWants(_currency);

                        currency = GetInputAsInteger(_currency, 1, 3);

                        if (currency is null)
                            continue;

                        break;
                    }

                    // Gets purchase date from user
                    DateTime purchaseDate;
                    while (true)
                    {
                        PrintTextWithColor("Enter a date (yyyy-MM-dd): ", ConsoleColor.Blue);
                        string? _purchaseDate = GetInput();

                        if (_purchaseDate is null)
                            continue;

                        exitIfUserWants(_purchaseDate);

                        if (!DateTime.TryParse(_purchaseDate, out DateTime purchaceDate2))
                            continue;

                        purchaseDate = Convert.ToDateTime(purchaceDate2);

                        break;
                    }

                    // Gets brand name from user
                    string? brand;
                    while (true)
                    {
                        PrintTextWithColor("Enter a brand name: ", ConsoleColor.Blue);
                        brand = GetInput();

                        if (brand is null)
                            continue;

                        exitIfUserWants(brand);

                        break;
                    }

                    // Gets model name from user
                    string? model;
                    while (true)
                    {
                        PrintTextWithColor("Enter a model name: ", ConsoleColor.Blue);
                        model = GetInput();

                        if (model is null)
                            continue;

                        exitIfUserWants(model);

                        break;
                    }

                    // Adds an asset
                    if (productClass == 1)
                        tracker.AddAsset(new Computer(price, (Currency)(currency - 1), purchaseDate, brand, model, (Country)(currency - 1)));
                    else
                        tracker.AddAsset(new Smartphone(price, (Currency)(currency - 1), purchaseDate, brand, model, (Country)(currency - 1)));

                    context.SaveChanges();

                    PrintAssets([.. context.Assets]);
                    PrintTextWithColor($"\nNumber of assets in the database: {context.Assets.Count()}", ConsoleColor.Green);
                    PrintTextWithColor("\nSelect an option:\nEnter 'q' to quit\nPress 'Enter' key to add more assets.\nEnter 3 to return main menu", ConsoleColor.Blue);
                    string? quitOrAdd = GetInput();

                    if (quitOrAdd is null)
                        continue;

                    exitIfUserWants(quitOrAdd);
                    goto GoToMainMenu;
                }
            }
            if (IsTokenEqual(input, "2")) // Edit an asset (only model name)
            {
                while (true)
                {
                    Console.Write("Enter an asset ID: ");
                    string? assetId = GetInput();

                    if (assetId is null)
                        continue;

                    exitIfUserWants(assetId);

                    int? aId = GetInputAsInteger(assetId!, 1, 200);

                    if (aId is null)
                        continue;

                    Product? foundAsset = (Product?)context.Assets.SingleOrDefault(a => a.Id == aId);
                    if (foundAsset is null)
                        PrintTextWithColor($"\nThere is no asset with this id, {aId}", ConsoleColor.Red);
                    else
                    {
                        string? newModel;
                        while (true)
                        {
                            Console.Write("New model name: ");
                            newModel = GetInput();

                            if (newModel is null)
                                continue;

                            exitIfUserWants(assetId);
                            break;
                        }

                        foundAsset.Model = newModel;
                        context.SaveChanges();
                        PrintAssets([.. context.Assets]);
                        PrintTextWithColor(SUBMENU_EDITING, ConsoleColor.Blue);
                        string? quitOrContinue = GetInput();

                        if (quitOrContinue is null)
                            continue;

                        exitIfUserWants(quitOrContinue);
                        goto GoToMainMenu;
                    }
                }
            }
            if (IsTokenEqual(input, "3")) // Delete an asset
            {
                while (true)
                {
                    Console.Write("Enter an asset ID: ");
                    string? assetId = GetInput();

                    if (assetId is null)
                        continue;

                    exitIfUserWants(assetId);

                    int? aId = GetInputAsInteger(assetId!, 1, 200);

                    if (aId is null)
                        continue;

                    Product? foundAsset = (Product?)context.Assets.SingleOrDefault(a => a.Id == aId);
                    if (foundAsset is null)
                        PrintTextWithColor($"\nThere is no asset with this id, {aId}", ConsoleColor.Red);
                    else
                    {
                        context.Assets.Remove(foundAsset);
                        context.SaveChanges();
                        PrintAssets([.. context.Assets]);
                        PrintTextWithColor($"\nAsset with id, {assetId} is deleted.", ConsoleColor.Green);
                        PrintTextWithColor($"\nNumber of assets in the database: {context.Assets.Count()}", ConsoleColor.Green);
                        PrintTextWithColor(SUBMENU_DELETING, ConsoleColor.Blue);
                        string? quitOrContinue = GetInput();

                        if (quitOrContinue is null)
                            continue;

                        exitIfUserWants(quitOrContinue);
                        goto GoToMainMenu;
                    }
                }
            }
        }
    }

    public static void exitIfUserWants(string input)
    {
        if (IsTokenEqual(input, "q"))
            Environment.Exit(0);
    }
    public static List<Asset> SortAssets(List<Asset> assets)
    {
        return assets.OrderBy(asset => (asset as Product)!.Country).ThenBy(asset => (asset as Product)!.PurchaceDate).ToList();
    }

    public static string FormatAssetData(string[] properties)
    {
        string s = "";
        foreach (var name in properties)
            s += name.PadRight(20);

        return s;
    }

    public static bool IsAssetGettingOlder(Asset asset)
    {
        // More than 3 years and less then 3 months
        var today = DateTime.Now;
        TimeSpan assetAge = today - (asset as Product)!.PurchaceDate;
        TimeSpan threeMonths = today.AddMonths(3) - today;

        if ((assetAge.Days > THREE_YEAR) & (assetAge.Days < THREE_YEAR + threeMonths.Days))
            return true;

        return false;
    }

    public static bool IsAssetTooOld(Asset asset)
    {
        // More than 3 years and more than 3 months
        var today = DateTime.Now;
        TimeSpan assetAge = today - (asset as Product)!.PurchaceDate;
        TimeSpan threeMonths = today.AddMonths(3) - today;

        if ((assetAge.Days > THREE_YEAR) & (assetAge.Days > THREE_YEAR + threeMonths.Days))
            return true;

        return false;
    }

    public static string? GetInput()
    {
        string? input = Console.ReadLine();

        if (String.IsNullOrWhiteSpace(input))
            return null;

        input = input!.Trim();

        return input;
    }

    public static float? GetPriceAsFloat(string? input)
    {
        float price;
        var style = NumberStyles.AllowDecimalPoint;
        var culture = CultureInfo.CreateSpecificCulture("en-US");

        if (!float.TryParse(input, style, culture, out price) | price < 0)
        {
            PrintTextWithColor("\nProvide a price like 100 or 11.99.\n", ConsoleColor.Red);
            return null;
        }

        return price;
    }

    public static int? GetInputAsInteger(string input, int min, int max)
    {
        int number;
        if (!int.TryParse(input, out number) | number < min | number > max)
        {
            PrintTextWithColor($"\nProvide a number between {min} and {max}.\n", ConsoleColor.Red);
            return null;
        }

        return number;
    }

    public static bool IsTokenEqual(string str1, string str2)
    {
        return str1.Trim().Equals(str2.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    public static void PrintAssets(List<Asset> assets)
    {
        printHeader();

        foreach (Product asset in SortAssets(assets))
        {
            if (IsAssetTooOld(asset))
            {
                PrintTextWithColor(FormatAssetData(asset.GetPropertyValues()), ConsoleColor.Red);
            }
            else if (IsAssetGettingOlder(asset))
            {
                PrintTextWithColor(FormatAssetData(asset.GetPropertyValues()), ConsoleColor.Yellow);
            }
            else
                Console.WriteLine(FormatAssetData(asset.GetPropertyValues()));
        }
    }
    public static void printHeader()
    {
        string header = "";
        var columns = new string[] {
                "Asset Id",
                "Office",
                "Asset",
                "Brand",
                "Model",
                "Price (USD)",
                "Price (Local)",
                "Purchase Date"
            };

        foreach (var name in columns)
            header += name.PadRight(20);

        PrintTextWithColor($"{header}", ConsoleColor.Cyan);
    }

    public static void PrintTextWithColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}
