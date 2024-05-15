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
        return base.CanUse(target) || Unity.InTimeline();
    }

    public override void Use(IEntity target)
    {
        base.Use(target);

        DateTime endTime = DayAndNightControl.Now.AddMinutes(Unity.SkippedMinutes);
        Unity.SkipTime(endTime);


    }
}
