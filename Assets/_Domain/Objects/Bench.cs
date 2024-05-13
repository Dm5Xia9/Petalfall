public class Bench : Entity<Bench, BenchScript>
{
    public Bench(BenchScript gameObject) : base(gameObject)
    {

    }

    public override bool CanCleaned => false;

    public override string Title => "Лавка";

    public override bool IsItem => false;

    public override bool CanUse(IEntity target)
    {
        return Unity.InTimeline();
    }

    public override void Use(IEntity entity)
    {
        var dt = DayAndNightControl.Now.AddMinutes(Unity.SkippedMinutes);
        Unity.SkipTime(dt);
        base.Use(entity);
    }
}
