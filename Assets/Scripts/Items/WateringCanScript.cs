public class WateringCanScript : CountableMonoBehaviour<WateringCan, WateringCanScript>
{
    protected override WateringCan CreateEntity()
    {
        return new WateringCan(this);
    }
}

