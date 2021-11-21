using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    [SerializeField] private GameObject projectilePrefab, model;
    public int hpMax, hp;
    public Enemy[] fragments;
    public Rigidbody thisRigidbody;
    [SerializeField] float moveSpeed;
    private float invincibleFrame = 1, invincibleFrameElapse = 0; 
    public float sinCosElapse;
    

    void Awake()
    {
        player = GameObject.Find("Player");
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invincibleFrameElapse > invincibleFrame)
        {
            Move();
        }
        else
        {
            invincibleFrameElapse += Time.fixedDeltaTime;
        }

    }
    private void Update()
    {
        sinCosElapse += Time.deltaTime;
    }

    private void Move()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        thisRigidbody.velocity = lookDirection * moveSpeed;
    }

    public bool Damage(int damage = 1)
    {
        if (invincibleFrameElapse < invincibleFrame)
        {
            return false;
        }
        hp -= damage;
        GameManager.exp += (hpMax * 5);
        if (hp <= 0)
        {
            return DestroySelf();
        }
        else
        {
            invincibleFrameElapse = 0;
            Pushback(0);
            return true;
        }

    }
    bool DestroySelf()
    {
        int index = 0;
        foreach (Enemy enemy in fragments)
        {
            if (enemy != this)
            {
                int side = 45;
                if (index > 0) side = -45;
                enemy.gameObject.SetActive(true);
                Vector3 scale = enemy.transform.localScale;
                enemy.transform.parent = null;
                enemy.transform.localScale = scale;
                enemy.Pushback(side);

            }
            index++;

        }
        Destroy(gameObject);
        return true;
    }

    private void Pushback(int side)
    {
        Vector3 pushbackDirection = ( (transform.position - player.transform.position).normalized * 10);
        //pushbackDirection = Quaternion.AngleAxis(side, Vector3.right) * pushbackDirection;
        pushbackDirection = Quaternion.Euler(0,0,side) * pushbackDirection;
        //Vector3 pushbackDirection = ((transform.position - player.transform.position).normalized +
        //            new Vector3(side, 1, 0)) * Random.Range(1, 5);
        thisRigidbody.velocity = pushbackDirection;
        StartCoroutine(InvincibleBlink());
    }

    public void Bunk()
    {
        invincibleFrameElapse = 0;
        Pushback(0);
    }

    public void SpawnProjectile(int quantity, float delay)
    {
        StartCoroutine(SpawnProjectileCoroutine(quantity,delay));
    }
    public Vector3 FindProjectilePosition()
    {
        Vector3 position = transform.position;
        position.x += 2.5f * Mathf.Sin(sinCosElapse);
        position.y += 2.5f * Mathf.Cos(sinCosElapse);
        return position;
    }

    IEnumerator SpawnProjectileCoroutine(int quantity, float delay)
    {
        for (int i = 0; i < quantity; i++)
        {
            Instantiate(projectilePrefab,FindProjectilePosition(), projectilePrefab.transform.rotation);
            yield return new WaitForSeconds(delay);
        }
    }
    private IEnumerator InvincibleBlink()
    {
        while (invincibleFrameElapse <= invincibleFrame)
        {
            if (model.transform.localScale == Vector3.one)
            {
                model.transform.localScale = Vector3.zero;
            }
            else
            {
                model.transform.localScale = Vector3.one;
            }
            yield return new WaitForSeconds(0.1f);
        }
        model.transform.localScale = Vector3.one;
    }
}
