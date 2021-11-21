using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileState
{
    Charging,
    Aiming,
    Firing
};

public class Projectile : MonoBehaviour
{



    ParticleSystem[] particles;
    public GameObject line;
    GameObject trail, player;
    ParticleSystem expire;
    SFXPlayer sfxPlayer;
    Vector3 targetPosition, lastVelocity;
    public ProjectileState projectileState;
    Rigidbody thisRigidbody;
    public float projectileSpeed;
    public float lifeSpam, lifeSpamElapse;
    public float bounceForce;

    void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
        thisRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        sfxPlayer = GetComponentInChildren<SFXPlayer>();
        foreach (ParticleSystem particle in particles)
        {
            if (particle.name == "Trail")
            {
                trail = particle.gameObject;
            }
            else if(particle.name == "Expire")
            {
                expire = particle;
            }
        }
        trail.transform.parent = null;
        line.transform.parent = null;
        projectileState = ProjectileState.Charging;
        StartCoroutine(ChargeAimFire());
    }

    // Update is called once per frame
    void Update()
    {
        trail.transform.position = transform.position;
        lastVelocity = thisRigidbody.velocity;
    }

    public void ChangeColor()
    {
        gameObject.tag = "Good Projectile";
        gameObject.layer = LayerMask.NameToLayer("GoodProjectile");
        lifeSpamElapse = 0;
        sfxPlayer.PlayClip(4);
        foreach (ParticleSystem particle in particles)
        {
            var main = particle.main;
            if (particle.gameObject.name == "Core" || particle.gameObject.name == "Trail")
            {
                main.startColor = Color.cyan;
            }
            else
            {
                main.startColor = Color.green;
            }
            
        }
    }
    public void Strech(GameObject sprite, Vector3 initialPosition, Vector3 finalPosition, bool isBig = false)
    {
        Vector3 centerPos = (initialPosition + finalPosition) / 2f;
        sprite.transform.position = centerPos;
        Vector3 direction = finalPosition - initialPosition;
        direction = Vector3.Normalize(direction);
        sprite.transform.right = direction;
        float yScale = 10;
        if (isBig) yScale = 200;
        Vector3 scale = new Vector3(10000, yScale, 1);
        sprite.transform.localScale = scale;
    }

    IEnumerator ChargeAimFire()
    {
        bool isDone = false;
        SpriteRenderer lineSpriteRenderer = line.GetComponent<SpriteRenderer>();
        while (!isDone)
        {
            switch (projectileState)
            {
                case ProjectileState.Charging:
                    yield return new WaitForSeconds(2);
                    projectileState = ProjectileState.Aiming;
                    break;
                case ProjectileState.Aiming:
                    targetPosition = player.transform.position;
                    Strech(line,transform.position,targetPosition);
                    for (float alpha = 0f; alpha <= 1; alpha += .1f)
                    {
                        Color color = lineSpriteRenderer.color;
                        color.a = alpha;
                        lineSpriteRenderer.color = color;
                        yield return new WaitForSeconds(.1f);
                    }
                    yield return new WaitForSeconds(0.25f);
                    projectileState = ProjectileState.Firing;
                    break;
                case ProjectileState.Firing:
                    Vector3 direction = (targetPosition - transform.position).normalized;
                    thisRigidbody.AddForce(direction*projectileSpeed,ForceMode.Impulse);
                    sfxPlayer.PlayClip(3);
                    for (float alpha = 1f; alpha >= 0; alpha -= .33f)
                    {
                        Color color = lineSpriteRenderer.color;
                        color.a = alpha;
                        lineSpriteRenderer.color = color;
                        yield return new WaitForSeconds(.1f);
                    }
                    isDone = true;
                    StartCoroutine("LifeSpam");
                    break;
            }
        }

    }

    public void SelfDestroy()
    {
        Destroy(line);
        Destroy(trail);
        expire.transform.parent = null;
        expire.Play();
        sfxPlayer.PlayClip(2);
        sfxPlayer.transform.parent = null;
        Destroy(gameObject);
    }

    IEnumerator LifeSpam(){
        while (true)
        {
            lifeSpamElapse += 1;
            yield return new WaitForSeconds(1);
            if (lifeSpamElapse >= lifeSpam)
            {
                SelfDestroy();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            thisRigidbody.velocity = direction*bounceForce;
            //thisRigidbody.AddForce(direction * bounceForce, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Damage(1000);
            sfxPlayer.PlayClip(1);
            
            SelfDestroy();
        }
    }
}
