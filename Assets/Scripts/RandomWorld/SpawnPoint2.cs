using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint2 : MonoBehaviour
{
    public static List<SpawnPoint2> SpawnPoints = new();

    private void Start()
    {
        SpawnPoints.Add(this);
    }
}
