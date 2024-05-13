using System.Collections;
using System.Linq;
using UnityEngine;

public class FlowerbedScript : ObjectMonoBehaviour<Flowerbed, FlowerbedScript>
{
    [SerializeField] private FlowerSeedsScript _baseSeeds;
    [SerializeField] private FlowerModelGenerator _flowerGenerator;
    [SerializeField] private FlowerModelGenerator _flowerModel;
    [SerializeField] private Vector3 _flowerOffset;

    [SerializeField] private float _wateringPeriod;
    [SerializeField] private float _nearRadius;
    [SerializeField] private float _selfGeneWeight;
    [SerializeField] private float _nearGeneWeight;
    [SerializeField] private float _farGeneWeight;


    public float WateringPeriod => _wateringPeriod;
    public float NearRadius => _nearRadius;
    public float SelfGeneWeight => _selfGeneWeight;
    public float NearGeneWeight => _nearGeneWeight;
    public float FarGeneWeight => _farGeneWeight;
    protected override Flowerbed CreateEntity()
    {
        return new Flowerbed(this);
    }


    protected override void ProtectedUpdate()
    {
        base.ProtectedUpdate();
        Entity.Tick();
    }

    public void UpdateFlowerModel(FlowerbedStage stage, FlowerParameters flowerParameters)
    {
        StartCoroutine(UpdateFlowerModelEnumerator(stage, flowerParameters));
    }

    private IEnumerator UpdateFlowerModelEnumerator(FlowerbedStage stage, FlowerParameters flowerParameters)
    {
        if (_flowerModel != null)
        {
            Destroy(_flowerModel.gameObject);
            _flowerModel = null;
        }
        yield return null;

        if (stage != FlowerbedStage.Empty)
        {
            _flowerModel = Instantiate(_flowerGenerator, transform.position + _flowerOffset, transform.rotation, transform);
            _flowerModel.Generate(stage, flowerParameters, Entity.Seed);
        }
        yield return null;
    }


    public void CreateSeedsAndPickup(FlowerParameters flowerParameters, int count = 1)
    {
        StartCoroutine(CreateSeedsAndPickupEnumerator(flowerParameters, count));
    }

    private IEnumerator CreateSeedsAndPickupEnumerator(FlowerParameters flowerParameters, int count = 1)
    {
        var seeds = Instantiate(_baseSeeds);
        seeds.Init(flowerParameters, count);
        yield return null;

        Player.Instance.PickupHandEntity(seeds.Entity);
        yield return null;
    }

    public Flowerbed[] GetAllFlowerbeds()
    {
        return transform.parent.GetComponentsInChildren<FlowerbedScript>()
            .Select(p => p.Entity).ToArray();
    }

}