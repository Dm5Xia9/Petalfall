public class Pickaxe : Entity<Pickaxe, PickaxeScript>
{
    public Pickaxe(PickaxeScript gameObject) : base(gameObject)
    {
    }

    public override string Title => "Кирка";

    public override bool IsItem => true;

    public override bool CanCleaned => false;

    public override bool CanUse(IEntity target)
    {
        return true;
    }

    public override void Use(IEntity target)
    {
        base.Use(target);
    }
}
