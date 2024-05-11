using UnityEngine;

namespace Assets.Scripts.WorldDevice
{
    public class Computer : BaseWorldDevice
    {
        [SerializeField] private FertilizerSource _fertilizerSource;
        public override string Message => "Заказать удобрения";
        public override bool TriggerEnable => base.TriggerEnable && MoneyUI.Instance.Money > 50;

        protected override void OnActive()
        {
            _fertilizerSource.AddFertilizer();
            MoneyUI.Instance.Money -= 50;
        }
    }
}