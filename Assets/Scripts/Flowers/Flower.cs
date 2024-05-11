using System.Collections;

using Assets.Scripts.Equipment;

using UnityEngine;

namespace Assets.Scripts.Flowers
{
    public class Flower : ActivationEvent
    {
        [SerializeField] private FlowerModelGenerator _flowerGenerator;
        [SerializeField] private FlowerSeeds _baseSeeds;

        [SerializeField] private FlowerParameters _flowerParameters;

        [SerializeField] private int _maxSeeds;

        private int _seed;

        public override bool TriggerEnable => _person.InHandObject == null;

        protected override void ProtectedStart()
        {
            _seed = Random.Range(0, 1_000_000);
            _flowerParameters = FlowerParameters.GetRandom();

            FlowerModelGenerator flowerModel = Instantiate(_flowerGenerator, transform.position, transform.rotation, transform);
            flowerModel.GenerateFlower(_flowerParameters, _seed);

            base.ProtectedStart();
        }

        protected override void OnActive()
        {
            StartCoroutine(CreateSeed());
            StartCoroutine(DestroyFlower());
        }

        private IEnumerator CreateSeed()
        {
            FlowerSeeds seeds = Instantiate(_baseSeeds, transform.parent);
            seeds.Init(_flowerParameters, Random.Range(1, _maxSeeds + 1));
            yield return null;

            seeds.Pickup();
            yield return null;
        }

        private IEnumerator DestroyFlower()
        {
            yield return null;

            Destroy(gameObject);
            yield return null;
        }
    }
}
