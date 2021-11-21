using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier4 : Enemy
{
    private void Start()
    {
        StartCoroutine(Tier4SpawnProjectileCoroutine());
    }

    IEnumerator Tier4SpawnProjectileCoroutine()
    {
        while (true)
        {
            SpawnProjectile(3, 0.33f);
            yield return new WaitForSeconds(5);
        }
    }
}
