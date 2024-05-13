using System;
using UnityEngine;

public class BenchScript : ObjectMonoBehaviour<Bench, BenchScript>
{
    [SerializeField] private int _skippedMinutes;
    [SerializeField] private float _speed;
    public int SkippedMinutes => _skippedMinutes;

    protected override Bench CreateEntity()
    {
        return new Bench(this);
    }

    public void SkipTime(DateTime resultDt)
    {
        StartCoroutine(DateTimeCoroutines.Skip(resultDt, _speed));
    }
}