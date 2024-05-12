using System.Collections;

using UnityEngine;

namespace Assets.Scripts.Equipment
{
    public class BaseEquipment : ActivationEvent
    {
        [SerializeField] private Vector3 _offsetInHand;
        [SerializeField] private Quaternion _rotateInHand;

        //private Rigidbody _rigidBody;

        //private bool _isPickup;
        //private Transform _initParent;

        //public override bool TriggerEnable => _isPickup == false;
        //public virtual int? Counter => null;

        //protected override void ProtectedStart()
        //{
        //    base.ProtectedStart();
        //    _rigidBody = GetComponent<Rigidbody>();
        //    _initParent = transform.parent;
        //}

        //protected override void OnActive()
        //{
        //    //StartCoroutine(PickupCoroutine());
        //    if (_person.InHandObject != null)
        //        _person.InHandObject.Drop();
        //    Pickup();
        //}

        //public virtual void Use(GameObject gameObject) { }

        //public void Pickup()
        //{
        //    _person.InHandObject = this;
        //    transform.SetParent(_person.Hand.transform, false);
        //    transform.SetLocalPositionAndRotation(_offsetInHand, _rotateInHand);

        //    _rigidBody.isKinematic = true;
        //    _isPickup = true;
        //    NotActive();
        //}

        //public void Drop()
        //{
        //    _person.InHandObject = null;
        //    transform.SetParent(_initParent, true);

        //    _rigidBody.isKinematic = false;
        //    _isPickup = false;
        //    Active();
        //}

        //private IEnumerator PickupCoroutine()
        //{
        //    if (_person.InHandObject != null)
        //        _person.InHandObject.Drop();
        //    yield return null;

        //    Pickup();
        //    yield return null;
        //}
    }
}