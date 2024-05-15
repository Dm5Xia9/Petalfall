public class Manhole : Entity<Manhole, ManholeScript>
{
    public Manhole(ManholeScript gameObject) : base(gameObject)
    { }

    public override bool CanCleaned => false;

    public override string Title => "Колодец";

    public override bool IsItem => false;

    public override bool CanUse(IEntity target)
    {
        return base.CanUse(target) || (Unity.InTimeline() &&
            Player.Instance.HandIsEmpty() == false &&
            Player.Instance.HandEntity is WateringCan can &&
            can.IsWaterlogged() == false);
    }

    public override void Use(IEntity target)
    {
        base.Use(target);
        ((WateringCan)Player.Instance.HandEntity).Fill();
    }
}
