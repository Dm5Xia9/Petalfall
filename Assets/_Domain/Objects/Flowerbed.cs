using System;
using System.Collections.Generic;

using Assets.Scripts;

using UnityEngine;

public class Flowerbed : Entity<Flowerbed, FlowerbedScript>
{
    private FlowerbedStage _flowerbedStage;
    private int _fertilizerCount;
    private DateTime _lastWatering;
    private DateTime _lastStageUpdate;
    private FlowerParameters _flowerParameters;
    private int _seed;
    private Dictionary<FlowerbedStage, float> _stagePeriods => new()
        {
            { FlowerbedStage.Maturation, 24.0f },
            { FlowerbedStage.Flowering, 48.0f },
            { FlowerbedStage.Harvest, 48.0f },
        };


    public Flowerbed(FlowerbedScript gameObject) : base(gameObject)
    {

    }

    public DateTime LastWatering => _lastWatering;
    public FlowerbedStage FlowerbedStage => _flowerbedStage;
    public DateTime LastStageUpdate => _lastStageUpdate;

    public override bool CanCleaned => false;

    public override string Title => "Колодец";

    public override bool IsItem => false;

    public int Seed => _seed;

    public float WaterCapacity => 1.0f - (float)(DayAndNightControl.Now - _lastWatering).TotalHours / Unity.WateringPeriod;

    public override bool CanUse(IEntity target)
    {
        if (!Unity.InTimeline())
            return false;

        if (target == null)
            return _flowerbedStage == FlowerbedStage.Harvest;

        switch (_flowerbedStage)
        {
            case FlowerbedStage.Empty:
                return target is FlowerSeeds;

            case FlowerbedStage.Maturation:
                if (target is WateringCan can1 && can1.IsWaterlogged())
                    return true;
                if (target is Fertilizer)
                    return true;
                return false;

            case FlowerbedStage.Flowering:
                if (target is WateringCan can2 && can2.IsWaterlogged())
                    return true;
                if (target is Pruner)
                    return true;
                return false;

            case FlowerbedStage.Compost:
                return target is Shovel;
        }

        return false;
    }

    public override void Use(IEntity target)
    {
        if (Player.Instance.HandIsEmpty() == false)
        {
            if (target is FlowerSeeds seeds)
            {
                this.Plant(seeds.Unity.Flower);
                seeds.Count--;
            }
            else if (target is WateringCan watering && watering.IsWaterlogged())
            {
                this.Watering();
                watering.Count--;
            }
            else if (target is Fertilizer fertilizer)
            {
                this.Fertilize();
                fertilizer.Count--;
            }
            else if (target is Pruner pruner)
            {
                this.Collect();
                Player.Instance.Balance += 100;
            }
            else if (target is Shovel shovel)
            {
                this.Collect();
            }
        }
        else
        {
            int seedsCount = 1 + _fertilizerCount;
            Collect();
            Unity.CreateSeedsAndPickup(_flowerParameters, seedsCount);
        }
        base.Use(target);
    }

    public void Tick()
    {
        if (_flowerbedStage == FlowerbedStage.Empty || _flowerbedStage == FlowerbedStage.Compost)
            return;

        if ((DayAndNightControl.Now - _lastStageUpdate).TotalHours >= _stagePeriods[_flowerbedStage])
        {
            _lastStageUpdate = DayAndNightControl.Now;
            _flowerbedStage++;
            Unity.UpdateFlowerModel(_flowerbedStage, _flowerParameters);

            if (_flowerbedStage == FlowerbedStage.Harvest)
                Pollination();
        }

        if (WaterCapacity <= 0.0f)
        {
            _lastStageUpdate = DayAndNightControl.Now;
            _flowerbedStage = FlowerbedStage.Compost;
            Unity.UpdateFlowerModel(_flowerbedStage, _flowerParameters);
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
        if (_flowerbedStage == FlowerbedStage.Compost)
            _flowerbedStage = FlowerbedStage.Empty;
        else
            _flowerbedStage = FlowerbedStage.Compost;

        _lastStageUpdate = DayAndNightControl.Now;
        _fertilizerCount = 0;

        Unity.UpdateFlowerModel(_flowerbedStage, _flowerParameters);

        return _flowerParameters;
    }

    private void Pollination()
    {
        Flowerbed[] flowerbeds = Unity.GetAllFlowerbeds();
        List<Flowerbed> nearFlowerbeds = new(), farFlowerbeds = new();
        foreach (Flowerbed flowerbed in flowerbeds)
        {
            if (flowerbed == this)
                continue;
            if (flowerbed.FlowerbedStage != FlowerbedStage.Flowering && flowerbed.FlowerbedStage != FlowerbedStage.Harvest)
                continue;
            if (flowerbed.FlowerbedStage == FlowerbedStage.Flowering &&
                (DayAndNightControl.Now - flowerbed.LastStageUpdate).TotalMinutes < _stagePeriods[FlowerbedStage.Flowering] / 2.0f)
                continue;

            if (Vector3.Distance(Unity.transform.position, flowerbed.Unity.transform.position) <= Unity.NearRadius)
                nearFlowerbeds.Add(flowerbed);
            else
                farFlowerbeds.Add(flowerbed);
        }

        Dictionary<FlowerParameters, float> parent = new();
        parent.AddWeight(_flowerParameters, Unity.SelfGeneWeight);
        foreach (Flowerbed near in nearFlowerbeds)
            parent.AddWeight(near._flowerParameters, Unity.NearGeneWeight / nearFlowerbeds.Count);
        foreach (Flowerbed far in farFlowerbeds)
            parent.AddWeight(far._flowerParameters, Unity.FarGeneWeight / nearFlowerbeds.Count);

        _flowerParameters = FlowerParametersUtils.Combine(parent);
    }


    public void Plant(FlowerParameters flower)
    {
        _seed = UnityEngine.Random.Range(0, 1_000_000);

        _flowerParameters = flower;
        _flowerbedStage = FlowerbedStage.Maturation;
        _fertilizerCount = 0;

        _lastStageUpdate = DayAndNightControl.Now;
        _lastWatering = DayAndNightControl.Now;

        Unity.UpdateFlowerModel(_flowerbedStage, _flowerParameters);
    }
}
