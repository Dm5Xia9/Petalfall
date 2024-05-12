using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Flowers
{
    [System.Serializable]
    public struct FlowerParameters
    {
        public Color[] BudColors;

        public int StemType;
        public int BudType;
        public int StemLength;
        public int LeafCount;
        public float BranchCount;

        public float LightPowerBud;

        public static readonly float MaxStemLength = 6;
        public static readonly float MaxLeafCount = 12;

        public FlowerParameters(Color[] budColors, int stemType, int budType, int stemLength, int leafCount, float branchCount, float lightPowerBud)
        {
            BudColors = budColors;
            StemType = stemType;
            BudType = budType;
            StemLength = stemLength;
            LeafCount = leafCount;
            LightPowerBud = lightPowerBud;
            BranchCount = branchCount;
        }

        public static float RandomGene => 0.8f + 0.4f * Random.value;

        public static FlowerParameters Combine(Dictionary<FlowerParameters, float> parents)
        {
            float weightSum = parents.GetWeightsSum();
            Dictionary<Color, float> budColors = new();
            float colorCount = 0.0f;
            float stemLength = 0.0f;
            float leafCount = 0.0f;
            float branchCount = 0.0f;
            float lightPowerBud = 0.0f;
            foreach (KeyValuePair<FlowerParameters, float> parent in parents)
            {
                float weight = parent.Value / weightSum;
                foreach (Color color in parent.Key.BudColors)
                    budColors.AddWeight(color, weight * RandomGene);
                colorCount += parent.Key.BudColors.Length * weight * RandomGene;
                stemLength += parent.Key.StemLength * weight * RandomGene;
                leafCount += parent.Key.LeafCount * weight * RandomGene;
                branchCount += parent.Key.BranchCount * weight * RandomGene;
                lightPowerBud += parent.Key.LightPowerBud * weight * RandomGene;
            }
            int stemType = parents.GetRandomWithWeights().StemType;
            int budType = parents.GetRandomWithWeights().BudType;
            return new FlowerParameters(budColors.GetRandomArrayWithWeights((int)Mathf.Round(colorCount)), stemType, budType, (int)Mathf.Round(stemLength), (int)Mathf.Round(leafCount), branchCount, lightPowerBud);
        }

        public static FlowerParameters GetRandom()
        {
            List<Color> budColors = new();
            do
                budColors.Add(Random.ColorHSV(0.0f, 1.0f, 0.4f, 0.8f, 0.4f, 0.8f, 1.0f, 1.0f));
            while (Random.value < 0.5f);

            int stemLength = 1;
            while (Random.value < 0.6f && stemLength <= MaxStemLength)
                stemLength++;

            int leafCount = 1;
            while (Random.value < 0.7f && leafCount <= MaxLeafCount)
                leafCount++;

            return new FlowerParameters(budColors.ToArray(), 0, Random.Range(0, 2), stemLength, leafCount, 2 * Mathf.Pow(Random.value, 2), 0.0f);
        }
    }
}
