using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts
{
    public static class CollectionsRandomUtils
    {
        public static T GetRandom<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static T[] GetRandomArray<T>(this List<T> list, int count)
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = list.GetRandom();
                list.Remove(result[i]);
            }
            return result;
        }

        public static float GetWeightsSum<T>(this Dictionary<T, float> weights)
        {
            float result = 0;
            foreach (float weight in weights.Values)
                result += weight;
            return result;
        }

        public static T GetRandomWithWeights<T>(this Dictionary<T, float> weights)
        {
            float value = Random.value * weights.GetWeightsSum();
            foreach (KeyValuePair<T, float> pair in weights)
            {
                if (pair.Value > value)
                    return pair.Key;
                value -= pair.Value;
            }
            return weights.First().Key;
        }

        public static T[] GetRandomArrayWithWeights<T>(this Dictionary<T, float> weights, int count)
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = weights.GetRandomWithWeights();
                weights.Remove(result[i]);
            }
            return result;
        }

        public static void AddWeight<T>(this Dictionary<T, float> weights, T key, float weight)
        {
            if (weights.ContainsKey(key))
                weights[key] += weight;
            else
                weights.Add(key, weight);
        }
    }
}
