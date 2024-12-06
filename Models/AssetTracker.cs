namespace TrackerEF.Models;

public partial class AssetTracker
{
    public uint Id { get; set; }
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public AssetTracker() { }

    public void AddAsset(Asset asset)
    {
        Assets.Add(asset);
    }
}
