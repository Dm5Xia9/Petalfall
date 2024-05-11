
using UnityEngine;

namespace Assets.Scripts.WorldDevice
{
    public class Pit : BaseWorldDevice
    {
        public override bool TriggerEnable => base.TriggerEnable & _person.InHandObject == null;
        public override string Message => "Телепортироваться";

        public AudioClip Clip;

        protected override void OnActive()
        {
            _person.Teleport(SpawnPoint2.SpawnPoints.GetRandom().transform.position);
            DayAndNightControl.Instance.SecondsInAFullDay = 360;
            DateTimeUI.Instance.source.Stop();
            DateTimeUI.Instance.source.PlayOneShot(Clip);
        }
    }
}