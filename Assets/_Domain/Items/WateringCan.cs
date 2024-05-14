public class WateringCan : CountableEntity<WateringCan, WateringCanScript>
{
    public WateringCan(WateringCanScript gameObject) : base(gameObject)
    { }

    public override string Title => "Лейка";

    public override bool CanCleaned => false;

    public override bool IsItem => true;

    public override bool CanUse(IEntity target)
    {
        return true;
    }

    public bool IsWaterlogged()
    {
        return Count > 0;
    }

    public void Fill()
    {
        Count = Capacity;
    }
}


