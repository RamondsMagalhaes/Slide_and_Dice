using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> swords, shields;
    [SerializeField] UIStage uiStage;
    public ParticleSystem impactParticleSystem;
    private AudioSource thisAudioSource;
    [SerializeField] private GameObject model;
    int hp;
    private float invincibleFrame = 3, invincibleFrameElapse = 0;
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        hp = GameManager.selectedShield;
        for (int i = 0; i < hp; i++)
        {
            shields[i].SetActive(true);
        }
        swords[GameManager.selectedSword-1].SetActive(true);
    }

    public void Damage(int damage = 1)
    {
        if (invincibleFrameElapse <= invincibleFrame) return;
        hp -= damage;
        thisAudioSource.Play();
        impactParticleSystem.gameObject.SetActive(true);
        invincibleFrameElapse = 0;
        StartCoroutine(InvincibleBlink());
        if (hp <= 2)
        {
            shields[2].SetActive(false);
        }
        if (hp <= 1)
        {
            shields[1].SetActive(false);
        }
        if (hp <= 0)
        {
            shields[0].SetActive(false);
        }
        if (hp <= -1)
        {
            Die();
        }
    }
    void Die()
    {
        GameManager.isDead = true;
        uiStage.DeathScreen();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.Bunk();
                Damage(1);
                break;
            case "Bad Projectile":
                Projectile projectile = collision.gameObject.GetComponent<Projectile>();
                projectile.SelfDestroy();
                Damage(1);
                break;
            case "Death Ray":
                invincibleFrameElapse = 999;
                Damage(999);
                break;
        }
    }
    private void Update()
    {
        if (invincibleFrameElapse <= invincibleFrame) invincibleFrameElapse += Time.deltaTime;
        
    }



    private IEnumerator InvincibleBlink()
    {
        while(invincibleFrameElapse <= invincibleFrame)
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
