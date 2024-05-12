using UnityEngine;

namespace Assets.Scripts.WorldDevice
{
    public class Computer : BaseWorldDevice
    {
        [SerializeField] private FertilizerSource _fertilizerSource;

        public override bool TriggerEnable => base.TriggerEnable && MoneyUI.Instance.Money > 50;
        public override string Message => "Заказать удобрения";

        protected override void OnActive()
        {
            _fertilizerSource.AddFertilizer();
            MoneyUI.Instance.Money -= 50;
            base.OnActive();
        }
    }
}