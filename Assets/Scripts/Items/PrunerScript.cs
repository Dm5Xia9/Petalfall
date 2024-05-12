public class PrunerScript : EntityMonoBehaviour<Pruner, PrunerScript>
{
    protected override Pruner CreateEntity()
    {
        return new Pruner(this);
    }
}

