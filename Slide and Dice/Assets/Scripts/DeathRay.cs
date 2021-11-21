using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRay : MonoBehaviour
{
    //public ParticleSystem rayParticleSystem;
    public GameObject line, rayCollider;
    SpriteRenderer lineSpriteRenderer;
    CameraShake cameraShake;
    private Transform player;
    float speed;
    public ProjectileState projectileState;
    bool isDone;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        speed = 8f;
        lineSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        isDone = false;
        projectileState = ProjectileState.Charging;
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
        StartCoroutine(ChargeAndFire());
    }

    private void Update()
    {
        Aim();
    }
    void Aim()
    {
        //Vector3 difference = player.position - transform.position;
        Vector3 difference = transform.position - player.position;
        float rotationZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0.0f, 0.0f, -rotationZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, -rotationZ), Time.deltaTime * speed);
        //yield return new WaitForSeconds(0.0f);
    }
    IEnumerator ChargeAndFire()
    {
        while (!isDone)
        {
            switch (projectileState)
            {
                case ProjectileState.Charging:
                    for (float alpha = 0; alpha <= 1.1f; alpha+=0.1f)
                    {
                        Color color = lineSpriteRenderer.color;
                        color.a = alpha;
                        lineSpriteRenderer.color = color;
                        yield return new WaitForSeconds(0.1f);
                    }
                    projectileState = ProjectileState.Aiming;
                    break;
                case ProjectileState.Aiming:
                    speed = 0;
                    for (float alpha = 1; alpha >= -0.1f; alpha -= 0.1f)
                    {
                        Color color = lineSpriteRenderer.color;
                        color.a = alpha;
                        lineSpriteRenderer.color = color;
                        yield return new WaitForSeconds(0.1f);
                    }
                    yield return new WaitForSeconds(2.5f);
                    projectileState = ProjectileState.Firing;
                    break;
                case ProjectileState.Firing:
                    speed = 0.125f;
                    rayCollider.SetActive(true);
                    StartCoroutine(cameraShake.Shake(7, 0.5f));
                    yield return new WaitForSeconds(7);
                    rayCollider.SetActive(false);
                    isDone = true;
                    break;
                default:
                    break;
            }
        }
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
