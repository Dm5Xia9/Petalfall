﻿public class Manhole : Entity<Manhole, ManholeScript>
{
    public Manhole(ManholeScript gameObject) : base(gameObject)
    {

    }

    public override bool CanCleaned => false;

    public override string Title => "Колодец";

    public override bool IsItem => false;

    public override bool CanUse(IEntity target)
    {
        return Unity.InTimeline() && 
            !Player.Instance.HandIsEmpty() &&
            Player.Instance.HandEntity is WateringCan can && 
            can.IsWaterlogged() == false;
    }

    public override void Use(IEntity target)
    {
        ((WateringCan)Player.Instance.HandEntity).Fill();
        base.Use(target);
    }
}