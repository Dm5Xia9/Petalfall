using Assets.Scripts;

public class Pit : Entity<Pit, PitScript>
{
    public Pit(PitScript gameObject) : base(gameObject)
    { }

    public override string Title => "Яма";

    public override bool IsItem => false;

    public override bool CanCleaned => false;

    public override bool CanUse(IEntity target)
    {
        return Unity.InTimeline() &&
            Player.Instance.HandIsEmpty();
    }

    public override void Use(IEntity target)
    {
        Player.Instance.Controller.Teleport(SpawnPoint2.SpawnPoints.GetRandom().transform.position);
        DayAndNightControl.Instance.SecondsInAFullDay = 360;

        base.Use(target);
    }
}
