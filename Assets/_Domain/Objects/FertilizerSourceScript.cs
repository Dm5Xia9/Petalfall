using System.Collections;
using UnityEngine;

public class FertilizerSourceScript : CountableObjectMonoBehaviour<FertilizerSource, FertilizerSourceScript>
{
    [SerializeField] private FertilizerScript _baseFertilizerObject;
    [SerializeField] private GameObject _startRoadPos;
    [SerializeField] private GameObject _centerRoadPos;
    [SerializeField] private GameObject _endRoadPos;
    [SerializeField] private float _carSpeed;
    [SerializeField] private bool _isHere = true;

    public Fertilizer BaseFertilizer => _baseFertilizerObject.Entity;

    protected override FertilizerSource CreateEntity()
    {
        return new FertilizerSource(this);
    }

    protected override void ProtectedUpdate()
    {
        base.ProtectedUpdate();

        if (_isHere != CanUseAsObject())
        {
            if (_isHere == true)
                StartCoroutine(CarOutcome());
            else
                StartCoroutine(CarIncome());
            _isHere = !_isHere;
        }
    }

    public void GiveFertilizer(int count)
    {
        StartCoroutine(CreateFertilizerAndPickup(count));
    }

    private IEnumerator CreateFertilizerAndPickup(int count)
    {
        var fertilizer = Instantiate(_baseFertilizerObject);
        fertilizer.Count = count;
        yield return null;

        Player.Instance.PickupHandEntity(fertilizer.Entity);
        yield return null;
    }

    private IEnumerator CarIncome()
    {
        while (transform.position.z >= _centerRoadPos.transform.position.z)
        {
            transform.Translate(new Vector3(0, 0, -Time.deltaTime * _carSpeed));
            yield return null;
        }
    }

    private IEnumerator CarOutcome()
    {
        while (transform.position.z >= _endRoadPos.transform.position.z)
        {
            transform.Translate(new Vector3(0, 0, -Time.deltaTime * _carSpeed));
            yield return null;
        }
        transform.position = _startRoadPos.transform.position;
        yield return null;
    }
}