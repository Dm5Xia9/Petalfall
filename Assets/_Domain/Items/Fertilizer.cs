using static Cinemachine.DocumentationSortingAttribute;
using static UnityEngine.Rendering.ReloadAttribute;

public class Fertilizer : CountableEntity<Fertilizer, FertilizerScript>
{
    public Fertilizer(FertilizerScript gameObject) : base(gameObject)
    { }

    public override string Title => "Удобрение";

    public override bool IsItem => true;

    public int OnePrice => Unity.OnePrice;

    public override bool CanCleaned => false;

    public override bool CanUse(IEntity target)
    {
        return base.CanUse(target);
    }

    public override void Use(IEntity target)
    {
        base.Use(target);
    }
}
