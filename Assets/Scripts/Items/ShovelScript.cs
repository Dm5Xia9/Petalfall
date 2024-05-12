public class ShovelScript : EntityMonoBehaviour<Shovel, ShovelScript>
{
    protected override Shovel CreateEntity()
    {
        return new Shovel(this);
    }
}

