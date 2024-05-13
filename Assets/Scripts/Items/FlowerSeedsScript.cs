using UnityEngine;

public class FlowerSeedsScript : CountableMonoBehaviour<FlowerSeeds, FlowerSeedsScript>
{
    [SerializeField] private FlowerParameters _flower;

    public FlowerParameters Flower => _flower;
    protected override FlowerSeeds CreateEntity()
    {
        return new FlowerSeeds(this);
    }

    public void Init(FlowerParameters flower, int count = 1)
    {
        _flower = flower;
        Count = count;
    }
}

