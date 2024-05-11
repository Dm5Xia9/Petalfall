using System;
using System.Collections;

using StarterAssets;

using UnityEngine;

public class SkipTime : ActivationEvent
{
    public float Speed;
    public int CountMinutes;

    public override string Message => "Отдохнуть";

    protected override void OnActive()
    {
        StartCoroutine(Timer());
    }


    private IEnumerator Timer()
    {
        ThirdPersonController.Instance.playerInput.enabled = false;
        DateTime resultTime = DayAndNightControl.Now.AddMinutes(CountMinutes);
        while (Check(resultTime))
        {
            DayAndNightControl.Instance.SkipTicks(Time.deltaTime * Speed);
            yield return null;
        }
        ThirdPersonController.Instance.playerInput.enabled = true;
    }

    private bool Check(DateTime maxDt)
    {
        try
        {
            return DayAndNightControl.Now <= maxDt;
        }
        catch
        {
            return true;
        }
    }
}
