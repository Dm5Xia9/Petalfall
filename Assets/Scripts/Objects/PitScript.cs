public class PitScript : ObjectMonoBehaviour<Pit, PitScript>
{
    protected override Pit CreateEntity()
    {
        return new Pit(this);
    }


}
