using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier5 : Enemy
{
    private void Start()
    {
        StartCoroutine(Tier5SpawnProjectileCoroutine());
    }

    IEnumerator Tier5SpawnProjectileCoroutine()
    {
        while (true)
        {
            SpawnProjectile(40, 0.25f);
            yield return new WaitForSeconds(10);
        }
    }
}
