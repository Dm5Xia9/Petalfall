using System;
using System.Collections;

using StarterAssets;

using UnityEngine;

namespace Assets.Scripts.WorldDevice
{
    public class Bench : BaseWorldDevice
    {
        [SerializeField] private int _skippedMinutes;
        [SerializeField] private float _speed;
        //public override string Message => "Отдохнуть";

        //protected override void OnActive()
        //{
        //    StartCoroutine(Timer());
        //}

        //private IEnumerator Timer()
        //{
        //    ThirdPersonController.Instance.playerInput.enabled = false;
        //    DateTime resultTime = DayAndNightControl.Now.AddMinutes(_skippedMinutes);
        //    while (Check(resultTime))
        //    {
        //        DayAndNightControl.Instance.SkipTicks(Time.deltaTime * _speed);
        //        yield return null;
        //    }
        //    ThirdPersonController.Instance.playerInput.enabled = true;
        //}

        //private bool Check(DateTime maxDt)
        //{
        //    try
        //    {
        //        return DayAndNightControl.Now <= maxDt;
        //    }
        //    catch
        //    {
        //        return true;
        //    }
        //}
    }
}