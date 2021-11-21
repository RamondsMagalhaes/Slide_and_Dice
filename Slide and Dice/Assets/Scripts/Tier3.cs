using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier3 : Enemy
{
    private void Start()
    {
        StartCoroutine(Tier3SpawnProjectileCoroutine());
    }

    IEnumerator Tier3SpawnProjectileCoroutine()
    {
        while (true)
        {
            SpawnProjectile(1, 1);
            yield return new WaitForSeconds(5);
        }
    }
}
