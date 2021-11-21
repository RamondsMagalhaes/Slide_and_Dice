using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    Transform pivot;
    [SerializeField] GameObject slash, bunk;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    float speed, deltaSpeed;
    float impactThreshold = 3;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        pivot = transform.parent;

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                string parent = pool.tag + " Pool";
                GameObject obj = Instantiate(pool.prefab, GameObject.Find(parent).transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }

    }
    private void Update()
    {
        deltaSpeed = Mathf.Abs(Mathf.Abs(pivot.rotation.z) - Mathf.Abs(speed)) * Time.fixedDeltaTime * 10000;
        speed = pivot.rotation.z;
        //if (deltaSpeed >0)
        //Debug.Log(deltaSpeed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bad Projectile") || other.gameObject.CompareTag("Good Projectile"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile.projectileState == ProjectileState.Firing)
            {
                if (deltaSpeed <= impactThreshold)
                {
                    projectile.ChangeColor();
                }
                else if (projectile.CompareTag("Bad Projectile"))
                {
                    projectile.SelfDestroy();
                }
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (deltaSpeed >= impactThreshold)
            {
                bool hit = enemy.Damage();
                if (hit) SpawnFromPool("Slash", enemy.transform.position, Quaternion.identity);
            }
            else
            {
                enemy.Bunk();
                SpawnFromPool("Bunk", enemy.transform.position, Quaternion.identity);
            }

        }



    }

    //private void OnTriggerStay(Collider other)
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bad Projectile") || other.gameObject.CompareTag("Good Projectile"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile.projectileState == ProjectileState.Firing)
            {
                Vector3 direction = (other.gameObject.transform.position - pivot.transform.position).normalized;
                other.GetComponent<Rigidbody>().velocity = direction * 10;
                //collision.rigidbody.AddForce(direction * 10, ForceMode.Impulse);
            }
        }


    }

    private GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject spawn = poolDictionary[tag].Dequeue();
        spawn.SetActive(true);
        spawn.transform.position = position;
        spawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(spawn);

        return spawn;
    }

}
