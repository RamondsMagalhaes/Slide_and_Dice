using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    AudioSource thisAudioSource;
    public AudioClip laser, pop, fire, reflect;
    bool isPlay;

    private void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        isPlay = false;
    }
    IEnumerator PlayAndDestroy(AudioClip clip)
    {
        if (!isPlay)
        {
            isPlay = true;
            thisAudioSource.PlayOneShot(clip);
            while (thisAudioSource.isPlaying)
            {
                yield return new WaitForSeconds(1);
            }
            Destroy(gameObject);
        }

    }
    public void PlayClip(int index)
    {
        if (thisAudioSource.isPlaying && !isPlay) this.thisAudioSource.Stop();
        if (index == 1)
        {
            StartCoroutine(PlayAndDestroy(laser));
        }
        else if (index == 2)
        {
            StartCoroutine(PlayAndDestroy(pop));
        }
        else if (index == 3)
        {
            thisAudioSource.PlayOneShot(fire);
        }
        else if (index == 4)
        {
            thisAudioSource.PlayOneShot(reflect);
        }
    }
}
