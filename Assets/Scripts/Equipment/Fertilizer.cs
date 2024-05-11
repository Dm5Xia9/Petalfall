using Assets.Scripts.WorldDevice;

using UnityEngine;

namespace Assets.Scripts.Equipment
{
    public class Fertilizer : SpendableEquipment
    {
        [SerializeField] private int _count;

        public override int? Counter => _count;
        protected override bool IsOver => _count <= 0;

        public void Init(int count = 1)
        {
            _count = count;
        }

        public override string Message => "Удобрение";

        public override void Use(GameObject gameObject)
        {
            Flowerbed flowerbed = gameObject.GetComponent<Flowerbed>();
            flowerbed.Fertilize();
            _count--;
            base.Use(gameObject);
        }
    }
}