using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float effectsVolume = 0.5f;
    [SerializeField] AudioClip menuSelectSound;
    [SerializeField] AudioClip goodHitSound;
    [SerializeField] AudioClip badHitSound;
    [SerializeField] AudioClip hourglassHitSound;
    [SerializeField] AudioClip gameOverSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

        public void PlayMenuSelect()
    {
        audioSource.PlayOneShot(menuSelectSound, effectsVolume);
    }

    public void PlayGoodHit()
    {
        audioSource.PlayOneShot(goodHitSound, effectsVolume);
    }

    public void PlayBadHit()
    {
        audioSource.PlayOneShot(badHitSound, effectsVolume);
    }

    public void PlayHourGlassHit()
    {
        audioSource.PlayOneShot(hourglassHitSound, effectsVolume);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverSound, effectsVolume);
    }
}
