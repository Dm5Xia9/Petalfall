using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Equipment;
using Assets.Scripts.Flowers;

using UnityEngine;

namespace Assets.Scripts.WorldDevice
{
    public enum FlowerbedStage
    {
        Empty,
        Maturation,
        Flowering,
        Harvest,
        Compost
    }

    public class Flowerbed : BaseWorldDevice
    {
        [SerializeField] private FlowerSeeds _baseSeeds;
        [SerializeField] private FlowerModelGenerator _flowerGenerator;

        [SerializeField] private FlowerParameters _flowerParameters;
        [SerializeField] private FlowerModelGenerator _flowerModel;
        [SerializeField] private Vector3 _flowerOffset;

        [SerializeField] private FlowerbedStage _flowerStage;
        [SerializeField] private DateTime _lastStageUpdate;
        private Dictionary<FlowerbedStage, float> StagePeriod => new()
        {
            { FlowerbedStage.Maturation, 24.0f },
            { FlowerbedStage.Flowering, 48.0f },
            { FlowerbedStage.Harvest, 48.0f },
        };

        [SerializeField] private DateTime _lastWatering;
        [SerializeField] private float _wateringPeriod;

        [SerializeField] private int _fertilizerCount;

        [SerializeField] private float _nearRadius;
        [SerializeField] private float _selfGeneWeight;
        [SerializeField] private float _nearGeneWeight;
        [SerializeField] private float _farGeneWeight;

        private int _seed;

        private delegate bool TriggerHandler(BaseEquipment equipment);
        private Dictionary<FlowerbedStage, TriggerHandler> AvailableTools => new()
        {
            { FlowerbedStage.Empty, equipment => equipment != null && equipment is FlowerSeeds },
            { FlowerbedStage.Maturation, equipment =>
                {
                    if(equipment == null)
                        return false;
                    if(equipment is WateringCan can && can.IsWaterlogged())
                        return true;
                    if(equipment is Fertilizer)
                        return true;
                    return false;
                } },
            { FlowerbedStage.Flowering, equipment =>
                {
                    if(equipment == null)
                        return false;
                    if(equipment is WateringCan can && can.IsWaterlogged())
                        return true;
                    if(equipment is Pruner)
                        return true;
                    return false;
                } },
            { FlowerbedStage.Harvest, equipment =>
                {
                    if(equipment == null)
                        return true;
                    return false;
                } },
            { FlowerbedStage.Compost, equipment => equipment is Shovel }
        };

        public override bool TriggerEnable => base.TriggerEnable & AvailableTools[_flowerStage](_person.InHandObject);
        public float WaterCapacity => 1.0f - (float)(DayAndNightControl.Now - _lastWatering).TotalHours / _wateringPeriod;
        public override string Message => "Использовать";

        protected override void ProtectedUpdate()
        {
            base.ProtectedUpdate();

            if (_flowerStage == FlowerbedStage.Empty || _flowerStage == FlowerbedStage.Compost)
                return;

            if ((DayAndNightControl.Now - _lastStageUpdate).TotalHours >= StagePeriod[_flowerStage])
            {
                _lastStageUpdate = DayAndNightControl.Now;
                _flowerStage++;
                StartCoroutine(UpdateFlowerModel());

                if (_flowerStage == FlowerbedStage.Harvest)
                    Pollination();
            }

            if (WaterCapacity <= 0.0f)
            {
                _lastStageUpdate = DayAndNightControl.Now;
                _flowerStage = FlowerbedStage.Compost;
                StartCoroutine(UpdateFlowerModel());
            }
        }

        protected override void OnActive()
        {
            if (_person.InHandObject != null)
            {
                _person.InHandObject.Use(gameObject);
            }
            else
            {
                int seedsCount = 1 + _fertilizerCount;
                Collect();
                StartCoroutine(CreateSeeds(seedsCount));
            }
        }

        public void Watering()
        {
            _lastWatering = DayAndNightControl.Now;
        }

        public void Fertilize()
        {
            _fertilizerCount++;
        }

        public FlowerParameters Collect()
        {
            if (_flowerStage == FlowerbedStage.Compost)
                _flowerStage = FlowerbedStage.Empty;
            else
                _flowerStage = FlowerbedStage.Compost;

            _lastStageUpdate = DayAndNightControl.Now;
            _fertilizerCount = 0;

            StartCoroutine(UpdateFlowerModel());

            return _flowerParameters;
        }

        public void Plant(FlowerParameters flower)
        {
            _seed = UnityEngine.Random.Range(0, 1_000_000);

            _flowerParameters = flower;
            _flowerStage = FlowerbedStage.Maturation;
            _fertilizerCount = 0;

            _lastStageUpdate = DayAndNightControl.Now;
            _lastWatering = DayAndNightControl.Now;

            StartCoroutine(UpdateFlowerModel());
        }

        private void Pollination()
        {
            Flowerbed[] flowerbeds = transform.parent.GetComponentsInChildren<Flowerbed>();
            List<Flowerbed> nearFlowerbeds = new(), farFlowerbeds = new();
            foreach (Flowerbed flowerbed in flowerbeds)
            {
                if (flowerbed == this)
                    continue;
                if (flowerbed._flowerStage != FlowerbedStage.Flowering && flowerbed._flowerStage != FlowerbedStage.Harvest)
                    continue;
                if (flowerbed._flowerStage == FlowerbedStage.Flowering &&
                    (DayAndNightControl.Now - flowerbed._lastStageUpdate).TotalMinutes < StagePeriod[FlowerbedStage.Flowering] / 2.0f)
                    continue;

                if (Vector3.Distance(transform.position, flowerbed.transform.position) <= _nearRadius)
                    nearFlowerbeds.Add(flowerbed);
                else
                    farFlowerbeds.Add(flowerbed);
            }

            Dictionary<FlowerParameters, float> parent = new();
            parent.AddWeight(_flowerParameters, _selfGeneWeight);
            foreach (Flowerbed near in nearFlowerbeds)
                parent.AddWeight(near._flowerParameters, _nearGeneWeight / nearFlowerbeds.Count);
            foreach (Flowerbed far in farFlowerbeds)
                parent.AddWeight(far._flowerParameters, _farGeneWeight / nearFlowerbeds.Count);

            _flowerParameters = FlowerParameters.Combine(parent);
        }

        private IEnumerator UpdateFlowerModel()
        {
            if (_flowerModel != null)
            {
                Destroy(_flowerModel.gameObject);
                _flowerModel = null;
            }
            yield return null;

            if (_flowerStage != FlowerbedStage.Empty)
            {
                _flowerModel = Instantiate(_flowerGenerator, transform.position + _flowerOffset, transform.rotation, transform);
                _flowerModel.Generate(_flowerStage, _flowerParameters, _seed);
            }
            yield return null;
        }

        private IEnumerator CreateSeeds(int count = 1)
        {
            FlowerSeeds seeds = Instantiate(_baseSeeds);
            seeds.Init(_flowerParameters, count);
            yield return null;

            seeds.Pickup();
            yield return null;
        }
    }
}