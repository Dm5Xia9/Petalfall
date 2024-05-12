using System.Collections;

using UnityEngine;

namespace Assets.Scripts.Equipment
{
    public class SpendableEquipment : BaseEquipment
    {
        protected virtual bool IsOver => false;

        public override void Use(GameObject gameObject)
        {
            base.Use(gameObject);

            if (IsOver == true)
                StartCoroutine(DestroyEquipment());
        }

        private IEnumerator DestroyEquipment()
        {
            Drop();
            yield return null;

            Person.InHandObject = null;
            Destroy(gameObject);
            yield return null;
        }
    }
}