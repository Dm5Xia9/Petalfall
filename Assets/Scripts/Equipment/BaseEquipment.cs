using System.Collections;

using UnityEngine;

namespace Assets.Scripts.Equipment
{
    public class BaseEquipment : ActivationEvent
    {
        [SerializeField] private Vector3 _offsetInHand;
        [SerializeField] private Quaternion _rotateInHand;

        private Rigidbody _rigidBody;

        private bool _isPickup;
        private Transform _initParent;

        public override bool TriggerEnable => _isPickup == false && Person.playerInput.enabled == true;
        public virtual int? Counter => null;

        protected override void ProtectedStart()
        {
            base.ProtectedStart();
            _rigidBody = GetComponent<Rigidbody>();
            _initParent = transform.parent;
        }

        protected override void OnActive()
        {
            //StartCoroutine(PickupCoroutine());
            if (Person.InHandObject != null)
                Person.InHandObject.Drop();
            Pickup();
            base.OnActive();
        }

        public virtual void Use(GameObject gameObject) { }

        public void Pickup()
        {
            Person.InHandObject = this;
            transform.SetParent(Person.Hand.transform, false);
            transform.SetLocalPositionAndRotation(_offsetInHand, _rotateInHand);

            _rigidBody.isKinematic = true;
            _isPickup = true;
            NotActive();
        }

        public void Drop()
        {
            Person.InHandObject = null;
            transform.SetParent(_initParent, true);

            _rigidBody.isKinematic = false;
            _isPickup = false;
            Active();
        }

        private IEnumerator PickupCoroutine()
        {
            if (Person.InHandObject != null)
                Person.InHandObject.Drop();
            yield return null;

            Pickup();
            yield return null;
        }
    }
}