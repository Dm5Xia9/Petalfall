using Assets.Scripts.WorldDevice;

using UnityEngine;

namespace Assets.Scripts.Equipment
{
    public class Shovel : BaseEquipment
    {
        public override string Message => "Лопата";

        public override void Use(GameObject gameObject)
        {
            Flowerbed flowerbed = gameObject.GetComponent<Flowerbed>();
            flowerbed.Collect();
            base.Use(gameObject);
        }
    }
}