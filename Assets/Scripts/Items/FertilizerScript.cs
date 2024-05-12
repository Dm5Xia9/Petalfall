using UnityEngine;

public class FertilizerScript : CountableMonoBehaviour<Fertilizer, FertilizerScript>
{
    [SerializeField] private int _onePrice;
    public int OnePrice => _onePrice;
    protected override Fertilizer CreateEntity()
    {
        return new Fertilizer(this);
    }
}