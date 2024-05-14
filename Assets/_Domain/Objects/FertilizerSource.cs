public class FertilizerSource : CountableEntity<FertilizerSource, FertilizerSourceScript>
{
    public FertilizerSource(FertilizerSourceScript unity) : base(unity)
    { }

    public override string Title => "AMID Car";

    public override bool IsItem => false;

    public override bool CanCleaned => false;

    public override bool CanUse(IEntity target)
    {
        return base.CanUse(target) &&
            Unity.InTimeline() &&
            Player.Instance.HandIsEmpty();
    }

    public int GetPrice()
    {
        return Unity.BaseFertilizer.OnePrice;
    }

    public void AddFertilizer()
    {
        Unity.Count++;
    }

    public override void Use(IEntity target)
    {
        Unity.GiveFertilizer(Unity.Count);
        Unity.Count = 0;

        base.Use(target);
    }

}
