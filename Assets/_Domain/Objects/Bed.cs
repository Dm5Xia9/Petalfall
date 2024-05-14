using System;

public class Bed : Entity<Bed, BedScript>
{
    public Bed(BedScript gameObject) : base(gameObject)
    { }

    public override string Title => "Кровать";

    public override bool IsItem => false;

    public override bool CanCleaned => false;

    public override bool CanUse(IEntity target)
    {
        return Unity.InTimeline();
    }

    public override void Use(IEntity target)
    {
        DayAndNightControl time = DayAndNightControl.Instance;
        DateTime awakeTime = new(
            DayAndNightControl.Now.Year,
            DayAndNightControl.Now.Month,
            DayAndNightControl.Now.Day + (time.currentHour >= 6 ? 1 : 0),
            Unity.WakeUpTime.Hour,
            Unity.WakeUpTime.Minutes,
            0);

        Unity.SkipTime(awakeTime);
        PlayerEvents.WentToBed.Trigger();

        base.Use(target);
    }
}
