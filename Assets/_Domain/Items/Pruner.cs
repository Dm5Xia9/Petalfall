using Assets.Scripts.Flowers;
using System;
using UnityEngine;

public class Pruner : Entity<Pruner, PrunerScript>
{
    public Pruner(PrunerScript gameObject) : base(gameObject)
    {
    }

    public override string Title => "Секатор";

    public override bool CanCleaned => false;

    public override bool IsItem => true;

    public override bool CanUse(IEntity target)
    {
        return target is Flowerbed;
    }

    public override void Use(IEntity target)
    {
        Flowerbed flowerbed = target as Flowerbed;
        FlowerParameters flower = flowerbed.Collect();
        Player.Instance.Balance += 100;
        base.Use(target);
    }
}

