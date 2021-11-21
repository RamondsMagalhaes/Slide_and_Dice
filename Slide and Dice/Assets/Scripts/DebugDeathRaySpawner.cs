using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDeathRaySpawner : MonoBehaviour
{
    public float rangeX = 6;
    public GameObject rayPrefab;
    void Start()
    {
        InvokeRepeating("SpawnProjectile", 3, 8);
    }

    void SpawnProjectile()
    {
        Instantiate(rayPrefab, new Vector3(Random.Range(-rangeX, rangeX), 5, 0), rayPrefab.transform.rotation);
    }
}
