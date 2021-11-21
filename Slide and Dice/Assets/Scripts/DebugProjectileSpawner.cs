using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugProjectileSpawner : MonoBehaviour
{
    public float rangeX = 6;
    public GameObject projectilePrefab;
    void Start()
    {
        InvokeRepeating("SpawnProjectile",3,3);
    }

    void SpawnProjectile()
    {
        Instantiate(projectilePrefab, new Vector3(Random.Range(-rangeX, rangeX), 5, 0),projectilePrefab.transform.rotation);
    }
}
