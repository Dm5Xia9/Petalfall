using Assets.Scripts.Flowers;
using Assets.Scripts.WorldDevice;

using UnityEngine;

namespace Assets.Scripts.Equipment
{
    public class FlowerSeeds : SpendableEquipment
    {
        [SerializeField] private FlowerParameters _flower;
        [SerializeField] private int _count;

        public override int? Counter => _count;
        protected override bool IsOver => _count <= 0;

        public override string Message => "Семена";

        public void Init(FlowerParameters flower, int count = 1)
        {
            _flower = flower;
            _count = count;
        }

        public override void Use(GameObject gameObject)
        {
            Flowerbed flowerbed = gameObject.GetComponent<Flowerbed>();
            flowerbed.Plant(_flower);
            _count--;
            base.Use(gameObject);
        }

    }
}