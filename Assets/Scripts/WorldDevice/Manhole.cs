using Assets.Scripts.Equipment;

namespace Assets.Scripts.WorldDevice
{
    public class Manhole : BaseWorldDevice
    {
        public override bool TriggerEnable => _person.InHandObject is WateringCan can && can.IsWaterlogged() == false;
        public override string Message => "Наполнить";

        protected override void OnActive()
        {
            (_person.InHandObject as WateringCan).Fill();
        }
    }
}