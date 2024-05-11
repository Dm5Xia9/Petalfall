using System.Collections.Generic;

using Assets.Scripts.WorldDevice;

using UnityEngine;

namespace Assets.Scripts.Flowers
{
    public class FlowerModelGenerator : MonoBehaviour
    {
        [SerializeField] private float _stemHeight;

        [SerializeField] private GameObject[] _stems;
        [SerializeField] private GameObject[] _buds;
        [SerializeField] private GameObject _leave;
        [SerializeField] private GameObject _baseCompost;

        public void Generate(FlowerbedStage stage, FlowerParameters flower, int seed)
        {
            Random.InitState(seed);
            switch (stage)
            {
                case FlowerbedStage.Maturation:
                    GenerateSprout(flower, seed);
                    break;
                case FlowerbedStage.Flowering:
                    GenerateFlower(flower, seed);
                    break;
                case FlowerbedStage.Harvest:
                    GenerateBulb(flower, seed);
                    break;
                case FlowerbedStage.Compost:
                    GenerateCompost(flower, seed);
                    break;
            }
        }

        public void GenerateSprout(FlowerParameters flower, int seed)
        {
            Random.InitState(seed);

            GameObject stemObject = Instantiate(_stems[flower.StemType], transform.position, Quaternion.identity, transform);
        }

        public void GenerateFlower(FlowerParameters flower, int seed)
        {
            Random.InitState(seed);

            List<Transform> leafStems = new();
            GameObject stemObject = Instantiate(_stems[flower.StemType], transform.position, Quaternion.identity, transform);
            leafStems.Add(stemObject.transform);
            GenerateStem(flower, stemObject.transform, flower.StemLength, leafStems);

            for (int i = 0; i < flower.LeafCount; i++)
            {
                Transform randomStem = leafStems.GetRandom();
                GameObject leave = Instantiate(_leave, randomStem.position + randomStem.rotation * new Vector3(0, _stemHeight * Random.value, 0),
                    Quaternion.Euler(Random.Range(60.0f, 80.0f), Random.Range(-180.0f, 180.0f), 0.0f), randomStem);
            }
        }

        public void GenerateBulb(FlowerParameters flower, int seed)
        {
            Random.InitState(seed);

            GameObject bud = Instantiate(_buds[flower.BudType], transform.position, Quaternion.identity, transform);
            Renderer budRenderer = bud.GetComponent<Renderer>();
            budRenderer.material.color = flower.BudColors.GetRandom();
            bud.transform.localScale = 0.25f * Vector3.one;
        }

        public void GenerateCompost(FlowerParameters flower, int seed)
        {
            Random.InitState(seed);

            GameObject compost = Instantiate(_baseCompost, transform.position - new Vector3(0.0f, 0.1f, -0.35f), Quaternion.identity, transform);
        }

        private void GenerateStem(FlowerParameters flower, Transform prevStem, int length, List<Transform> leafStems)
        {
            GameObject stem = Instantiate(_stems[flower.StemType], prevStem.position + prevStem.rotation * new Vector3(0, _stemHeight, 0),
            Quaternion.Euler(Random.Range(-30.0f, 30.0f), Random.Range(-180.0f, 180.0f), 0.0f), prevStem);

            length--;
            if (length <= 0)
            {
                GameObject bud = Instantiate(_buds[flower.BudType], stem.transform.position + stem.transform.rotation * new Vector3(0, _stemHeight, 0), stem.transform.rotation, stem.transform);
                Renderer budRenderer = bud.GetComponent<Renderer>();
                budRenderer.material.color = flower.BudColors.GetRandom();
            }
            else
            {
                leafStems.Add(stem.transform);
                GenerateStem(flower, stem.transform, length, leafStems);

                float branch = flower.BranchCount;
                while (Random.value <= branch)
                {
                    GenerateStem(flower, stem.transform, length, leafStems);
                    branch -= 1.0f;
                }
            }
        }
    }
}
