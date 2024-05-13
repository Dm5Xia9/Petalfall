using System;
using UnityEngine;

public class BedScript : ObjectMonoBehaviour<Bed, BedScript>
{
    [SerializeField] private TimePoint _wakeUpTime;
    [SerializeField] private float _speed;
    public TimePoint WakeUpTime => _wakeUpTime;

    protected override Bed CreateEntity()
    {
        return new Bed(this);
    }

    public void SkipTime(DateTime resultDt)
    {
        StartCoroutine(DateTimeCoroutines.Skip(resultDt, _speed));
    }
}

