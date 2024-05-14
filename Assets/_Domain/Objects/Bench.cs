using System;

public class Bench : Entity<Bench, BenchScript>
{
    public Bench(BenchScript gameObject) : base(gameObject)
    { }

    public override string Title => "Лавка";

    public override bool IsItem => false;

    public override bool CanCleaned => false;

    public override bool CanUse(IEntity target)
    {
        return Unity.InTimeline();
    }

    public override void Use(IEntity target)
    {
        DateTime endTime = DayAndNightControl.Now.AddMinutes(Unity.SkippedMinutes);
        Unity.SkipTime(endTime);

        base.Use(target);
    }
}
