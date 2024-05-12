using Assets.Scripts.Flowers;
using Assets.Scripts.WorldDevice;

using UnityEngine;

namespace Assets.Scripts.Equipment
{
    public class Pruner : BaseEquipment
    {
        public override string Message => "Секатор";

        public override void Use(GameObject gameObject)
        {
            Flowerbed flowerbed = gameObject.GetComponent<Flowerbed>();
            FlowerParameters flower = flowerbed.Collect();
            MoneyUI.Instance.Money += 100;
            base.Use(gameObject);
        }
    }
}