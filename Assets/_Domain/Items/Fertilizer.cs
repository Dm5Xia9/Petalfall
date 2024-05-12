public class Fertilizer : CountableEntity<Fertilizer, FertilizerScript>
{
    public Fertilizer(FertilizerScript gameObject) : base(gameObject)
    {
    }

    public override string Title => "Удобрение";

    public override bool IsItem => true;

    public override bool CanCleaned => false;
    public int OnePrice => Unity.OnePrice;

    public override bool CanUse(IEntity target)
    {
        return base.CanUse(target) && target is Flowerbed;
    }

    public override void Use(IEntity target)
    {
        var flowerbed = target as Flowerbed;
        flowerbed.Fertilize();
        Count--;
        base.Use(target);
    }
}
