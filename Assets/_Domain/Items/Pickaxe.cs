public class Pickaxe : Entity<Pickaxe, PickaxeScript>
{
    public Pickaxe(PickaxeScript gameObject) : base(gameObject)
    { }

    public override string Title => "Кирка";

    public override bool IsItem => true;

    public override bool CanCleaned => Unity.Strength <= 0;

    public override bool CanUse(IEntity target)
    {
        return Unity.Strength >= 0;
    }

    public override void Use(IEntity target)
    {
        Unity.Strength--;

        base.Use(target);
    }
}
