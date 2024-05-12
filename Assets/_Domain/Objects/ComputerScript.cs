using UnityEngine;

public class ComputerScript : ObjectMonoBehaviour<Computer, ComputerScript>
{
    [SerializeField] private FertilizerSourceScript _fertilizerSourceObject;
    public FertilizerSource FertilizerSource => _fertilizerSourceObject.Entity;

    protected override Computer CreateEntity()
    {
        return new Computer(this);
    }
}
