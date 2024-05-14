public class StoneBlock : Entity<StoneBlock, StoneBlockScript>
{
    public StoneBlock(StoneBlockScript gameObject) : base(gameObject)
    { }

    public override string Title => $"Камень {Unity.Resources}";

    public override bool IsItem => false;

    public override bool CanCleaned => Unity.Resources <= 0;

    public override bool CanUse(IEntity target)
    {
        return target is Pickaxe pickaxe &&
            pickaxe.Unity.Level >= Unity.MinPickaxeLevel &&
            pickaxe.CanUse(this);
    }

    public override void Use(IEntity target)
    {
        target.Use(this);
        Unity.Resources -= Unity.OneClickResources;

        base.Use(target);
    }
}
