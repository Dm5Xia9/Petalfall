using System.Collections;

using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _objectCollection;
    [SerializeField] private GameObject[] _prefabs;

    [SerializeField] private float _minY = 0.7f;
    [SerializeField] private float _minHeight = -20.0f;
    [SerializeField] private float _maxHeight = 20.0f;
    [SerializeField] private float _terrainNoiseScale = 0.05f;
    [SerializeField] private float _roughness = 3.0f;
    [SerializeField] private int _octaves = 4;
    [SerializeField] private float _persistence = 0.5f;
    [SerializeField] private float _lacunarity = 2.0f;

    [SerializeField] private float _spawnDensity = 0.1f;
    [SerializeField] private float _heightOffset = 0.5f;
    [SerializeField] private float _prefabNoiseScale = 1.0f;
    [SerializeField] private int _minObjectsPerPrefab = 1;
    [SerializeField] private int _maxObjectsPerPrefab = 5;

    private Terrain _terrain;

    private void Start()
    {
        _terrain = GetComponent<Terrain>();
        Generate();
    }

    public void Generate()
    {
        StartCoroutine(GeneratorCoroutine());
    }

    private IEnumerator GeneratorCoroutine()
    {
        DestroyObjects();
        yield return null;

        //GenerateTerrain();
        yield return null;

        foreach (GameObject prefab in _prefabs)
        {
            int objectsCount = Random.Range(_minObjectsPerPrefab, _maxObjectsPerPrefab + 1);
            for (int i = 0; i < objectsCount; i++)
                GenerateObject(prefab);
        }
        yield return null;
    }

    private void DestroyObjects()
    {
        Transform[] allObjects = _objectCollection.GetComponentsInChildren<Transform>();
        foreach (Transform destroyObject in allObjects)
        {
            if (destroyObject == _objectCollection.transform)
                continue;
            Destroy(destroyObject.gameObject);
        }
    }

    private void GenerateTerrain()
    {
        TerrainData terrainData = _terrain.terrainData;
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                float noise = 0f;
                float frequency = _terrainNoiseScale;
                float amplitude = 1f;

                for (int i = 0; i < _octaves; i++)
                {
                    noise += Mathf.PerlinNoise((float)x / terrainData.heightmapResolution * frequency, (float)z / terrainData.heightmapResolution * frequency) * amplitude;
                    frequency *= _lacunarity;
                    amplitude *= _persistence;
                }

                noise = Mathf.Pow(noise, _roughness);
                heightMap[x, z] = Mathf.Lerp(_minHeight, _maxHeight, noise);
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    private bool GenerateObject(GameObject prefab)
    {
        float x = Random.Range(-50, 50);
        float z = Random.Range(-50, 50);
        Vector3 globalPosition = _objectCollection.transform.TransformPoint(new Vector3(x, 0f, z));
        float y = _terrain.SampleHeight(globalPosition) + _heightOffset;
        if (y <= _minY)
        {
            return GenerateObject(prefab);
        }
        float noise = Mathf.PerlinNoise(x / _prefabNoiseScale, z / _prefabNoiseScale);
        if (noise > _spawnDensity)
        {
            GameObject generatedObject = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
            generatedObject.transform.SetParent(_objectCollection.transform);
            generatedObject.transform.localPosition = new Vector3(x, y, z);
            return true;
        }

        return GenerateObject(prefab);
    }
}
