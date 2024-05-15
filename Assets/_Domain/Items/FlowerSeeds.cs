public class FlowerSeeds : CountableEntity<FlowerSeeds, FlowerSeedsScript>
{
    public FlowerSeeds(FlowerSeedsScript gameObject) : base(gameObject)
    { }

    public override string Title => "Семена";

    public override bool IsItem => true;

    public override bool CanUse(IEntity target)
    {
        return base.CanUse(target) || target is Flowerbed;
    }

    public override void Use(IEntity target)
    {
        base.Use(target);
    }
}

