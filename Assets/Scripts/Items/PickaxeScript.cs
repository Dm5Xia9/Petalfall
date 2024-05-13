using UnityEngine;

public class PickaxeScript : EntityMonoBehaviour<Pickaxe, PickaxeScript>
{
    [SerializeField] private int _level;
    [SerializeField] private int _strength;
    [SerializeField] private int _maxStrength;
    public int Level => _level;
    public int Strength
    {
        get { return _strength; }
        set { _strength = value; }
    }
    public int MaxStrength => _maxStrength;

    protected override Pickaxe CreateEntity()
    {
        return new Pickaxe(this);
    }



}