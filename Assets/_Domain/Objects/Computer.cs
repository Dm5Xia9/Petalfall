public class Computer : Entity<Computer, ComputerScript>
{
    public Computer(ComputerScript unity) : base(unity)
    { }

    public override string Title => "Лавка";

    public override bool IsItem => false;

    public override bool CanCleaned => false;

    public override bool CanUse(IEntity target)
    {
        return Unity.InTimeline() &&
            Player.Instance.EnoughBalance(Unity.FertilizerSource.GetPrice());
    }

    public override void Use(IEntity target)
    {
        Unity.FertilizerSource.AddFertilizer();
        Player.Instance.Balance -= Unity.FertilizerSource.GetPrice();

        base.Use(target);
    }

}
