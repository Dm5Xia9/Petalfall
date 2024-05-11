using System;
using System.Collections;

using StarterAssets;

using UnityEngine;

namespace Assets.Scripts.WorldDevice
{
    public class Bed : BaseWorldDevice
    {
        [SerializeField] private WorldGenerator _worldGenerator;

        [SerializeField] private int _wakeUpTime;
        [SerializeField] private float _speed;
        public override string Message => "Лечь спать";

        protected override void OnActive()
        {
            DayAndNightControl time = DayAndNightControl.Instance;
            DateTime awakeTime = new(DayAndNightControl.Now.Year, DayAndNightControl.Now.Month, DayAndNightControl.Now.Day + (time.currentHour >= 6 ? 1 : 0), _wakeUpTime, 0, 0);
            StartCoroutine(Timer(awakeTime));
            _worldGenerator.Generate();
        }

        private IEnumerator Timer(DateTime resultTime)
        {
            ThirdPersonController.Instance.playerInput.enabled = false;
            while (Check(resultTime))
            {
                DayAndNightControl.Instance.SkipTicks(Time.deltaTime * _speed);
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
}