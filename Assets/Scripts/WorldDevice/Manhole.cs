using Assets.Scripts.Equipment;

namespace Assets.Scripts.WorldDevice
{
    public class Manhole : BaseWorldDevice
    {
        public override bool TriggerEnable => Person.InHandObject is WateringCan can && can.IsWaterlogged() == false;
        public override string Message => "Наполнить";

        protected override void OnActive()
        {
            (Person.InHandObject as WateringCan).Fill();
            base.OnActive();
        }
    }
}