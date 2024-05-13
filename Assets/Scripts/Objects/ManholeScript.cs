public class ManholeScript : ObjectMonoBehaviour<Manhole, ManholeScript>
{
    protected override Manhole CreateEntity()
    {
        return new Manhole(this);
    }
}
