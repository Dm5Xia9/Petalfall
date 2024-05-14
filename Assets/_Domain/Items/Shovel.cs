public class Shovel : Entity<Shovel, ShovelScript>
{
    public Shovel(ShovelScript gameObject) : base(gameObject)
    { }

    public override string Title => "Лопата";

    public override bool CanCleaned => false;

    public override bool IsItem => true;

    public override bool CanUse(IEntity target)
    {
        return target is Shovel;
    }

    public override void Use(IEntity target)
    {
        base.Use(target);
    }
}

