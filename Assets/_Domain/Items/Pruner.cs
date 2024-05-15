public class Pruner : Entity<Pruner, PrunerScript>
{
    public Pruner(PrunerScript gameObject) : base(gameObject)
    { }

    public override string Title => "Секатор";

    public override bool IsItem => true;

    public override bool CanCleaned => false;

    public override bool CanUse(IEntity target)
    {
        return base.CanUse(target);
    }

    public override void Use(IEntity target)
    {
        base.Use(target);
    }
}

