namespace TrackerEF.Models;

public abstract class Asset
{
    public uint Id { get; set; }
    public string GetAssetName() => GetType().Name;
}
