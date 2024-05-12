using Assets.Scripts.WorldDevice;

using UnityEngine;

namespace Assets.Scripts.Equipment
{
    public class WateringCan : BaseEquipment
    {
        [SerializeField] private int _waterCount;
        [SerializeField] private int _waterCapacity;

        public override int? Counter => _waterCount;

        public override string Message => "Лейка";

        public override void Use(GameObject gameObject)
        {
            Flowerbed flowerbed = gameObject.GetComponent<Flowerbed>();
            flowerbed.Watering();
            _waterCount--;

            base.Use(gameObject);
        }

        public bool IsWaterlogged()
        {
            return _waterCount > 0;
        }

        public void Fill()
        {
            _waterCount = _waterCapacity;
        }
    }
}