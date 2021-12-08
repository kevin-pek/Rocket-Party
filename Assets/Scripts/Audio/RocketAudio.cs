using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAudio : MonoBehaviour
{
    public AudioClip explosionSound;
    public AudioClip fireSound;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayExplosionSound()
    {
        source.PlayOneShot(explosionSound);
        Destroy(gameObject, 3f);
    }

    public void PlayFireSound()
    {
        source.PlayOneShot(fireSound);
    }
}
