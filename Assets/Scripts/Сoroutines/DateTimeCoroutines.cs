using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class DateTimeCoroutines
{
    public static IEnumerator Skip(DateTime resultTime, float speed)
    {
        Player.Instance.UserInputDisable();
        while (Check(resultTime))
        {
            DayAndNightControl.Instance.SkipTicks(Time.deltaTime * speed);
            yield return null;
        }
        Player.Instance.UserInputEnable();
    }

    private static bool Check(DateTime maxDt)
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