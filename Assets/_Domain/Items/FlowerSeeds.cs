using System;
using UnityEngine;

public class FlowerSeeds : CountableEntity<FlowerSeeds, FlowerSeedsScript>
{
    public FlowerSeeds(FlowerSeedsScript gameObject) : base(gameObject)
    {
    }

    public override string Title => "Семена";

    public override bool IsItem => true;
    public override bool CanCleaned => Count <= 0;

    public override bool CanUse(IEntity target)
    {
        return base.CanUse(target) && target is Flowerbed;
    }

    public override void Use(IEntity target)
    {
        var flowerbed = target as Flowerbed;
        flowerbed.Plant(Unity.Flower);
        Count--;
        base.Use(target);
    }
}

