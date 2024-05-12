
using UnityEngine;

namespace Assets.Scripts.WorldDevice
{
    public class Pit : BaseWorldDevice
    {
        public override bool TriggerEnable => base.TriggerEnable & Person.InHandObject == null;
        public override string Message => "Прыгнуть";

        public AudioClip Clip;

        protected override void OnActive()
        {
            Person.Teleport(SpawnPoint2.SpawnPoints.GetRandom().transform.position);
            DayAndNightControl.Instance.SecondsInAFullDay = 360;
            DateTimeUI.Instance.source.Stop();
            DateTimeUI.Instance.source.PlayOneShot(Clip);
            base.OnActive();
        }
    }
}