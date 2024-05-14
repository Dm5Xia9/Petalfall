using UnityEngine;

public class StoneBlockScript : ObjectMonoBehaviour<StoneBlock, StoneBlockScript>
{
    [SerializeField] private int _minPickaxeLevel;
    [SerializeField] private int _resources;
    [SerializeField] private int _oneClickResources;

    public int MinPickaxeLevel => _minPickaxeLevel;
    public int Resources
    {
        get { return _resources; }
        set { _resources = value; }
    }
    public int OneClickResources => _oneClickResources;

    protected override StoneBlock CreateEntity()
    {
        return new StoneBlock(this);
    }


}