public class Flower : Entity<Flower, FlowerScript>
{
    public Flower(FlowerScript gameObject) : base(gameObject)
    { }

    public override string Title => "Цветок";

    public override bool IsItem => false;

    public override bool CanCleaned => false;

    public override bool CanUse(IEntity target)
    {
        return target == null;
    }

    public override void Use(IEntity target)
    {
        Unity.Active();

        base.Use(target);
    }
}
