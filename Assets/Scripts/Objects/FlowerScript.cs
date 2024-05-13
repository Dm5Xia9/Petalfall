using System.Collections;
using UnityEngine;

public class FlowerScript : ObjectMonoBehaviour<Flower, FlowerScript>
{
    [SerializeField] private FlowerModelGenerator _flowerGenerator;
    [SerializeField] private FlowerSeedsScript _baseSeeds;

    [SerializeField] private FlowerParameters _flowerParameters;

    [SerializeField] private int _maxSeeds;

    private int _seed;

    protected override Flower CreateEntity()
    {
        return new Flower(this);
    }

    protected override void ProtectedStart()
    {
        _seed = Random.Range(0, 1_000_000);
        _flowerParameters = FlowerParameters.GetRandom();

        FlowerModelGenerator flowerModel = Instantiate(_flowerGenerator, transform.position, transform.rotation, transform);
        flowerModel.GenerateFlower(_flowerParameters, _seed);

        base.ProtectedStart();
    }

    public void Active()
    {
        StartCoroutine(CreateSeed());
        StartCoroutine(DestroyFlower());
    }

    private IEnumerator CreateSeed()
    {
        var seeds = Instantiate(_baseSeeds, transform.parent);
        seeds.Init(_flowerParameters, Random.Range(1, _maxSeeds + 1));
        yield return null;

        Player.Instance.PickupHandEntity(seeds.Entity);
        yield return null;
    }

    private IEnumerator DestroyFlower()
    {
        yield return null;

        Destroy(gameObject);
        yield return null;
    }

}