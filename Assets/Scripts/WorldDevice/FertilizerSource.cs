using System.Collections;

using Assets.Scripts.Equipment;

using UnityEngine;

namespace Assets.Scripts.WorldDevice
{
    public class FertilizerSource : BaseWorldDevice
    {
        [SerializeField] private Fertilizer _baseFertilizer;
        [SerializeField] private int _fertilizerCount;

        [SerializeField] private GameObject _startRoadPos;
        [SerializeField] private GameObject _centerRoadPos;
        [SerializeField] private GameObject _endRoadPos;
        [SerializeField] private float _carSpeed;

        private bool _isHere = true;

        public override bool TriggerEnable => Person.InHandObject == null && _fertilizerCount > 0;
        public override string Message => "Взять удобрения";

        protected override void ProtectedUpdate()
        {
            base.ProtectedUpdate();

            if (_isHere != base.TriggerEnable)
            {
                if (_isHere == true)
                    StartCoroutine(CarOutcome());
                else
                    StartCoroutine(CarIncome());
                _isHere = !_isHere;
            }
        }

        protected override void OnActive()
        {
            _fertilizerCount--;

            StartCoroutine(CreateFertilizer());
            base.OnActive();
        }

        public void AddFertilizer()
        {
            _fertilizerCount++;
        }

        private IEnumerator CreateFertilizer()
        {
            Fertilizer fertilizer = Instantiate(_baseFertilizer);
            fertilizer.Init(_fertilizerCount + 1);
            yield return null;

            fertilizer.GetComponent<Fertilizer>().Pickup();
            yield return null;
        }

        private IEnumerator CarIncome()
        {
            while (transform.position.z >= _centerRoadPos.transform.position.z)
            {
                transform.Translate(new Vector3(0, 0, -Time.deltaTime * _carSpeed));
                yield return null;
            }
        }

        private IEnumerator CarOutcome()
        {
            while (transform.position.z >= _endRoadPos.transform.position.z)
            {
                transform.Translate(new Vector3(0, 0, -Time.deltaTime * _carSpeed));
                yield return null;
            }
            transform.position = _startRoadPos.transform.position;
            yield return null;
        }
    }
}