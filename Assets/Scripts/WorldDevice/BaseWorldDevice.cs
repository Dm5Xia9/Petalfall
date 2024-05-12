using UnityEngine;

namespace Assets.Scripts.WorldDevice
{
    public class BaseWorldDevice : ActivationEvent
    {
        [SerializeField] private int _startUsingTime;
        [SerializeField] private int _endUsingTime;

        //public override bool TriggerEnable => DayAndNightControl.Now.Hour >= _startUsingTime && DayAndNightControl.Now.Hour < _endUsingTime;
    }
}