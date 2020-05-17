using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : MonoBehaviour
{
    [SerializeField] float spinSpeed = -30f;
    [SerializeField] ParticleSystem sandParticles;
    
    private void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject.Find("Player Clock").GetComponent<HandRotator>().AdvancePlayerTime();
                FindObjectOfType<ScoreKeeper>().IncrementHour();
            }
            FindObjectOfType<PlayerHealth>().AddHealth();
            FindObjectOfType<ScreenShake>().TriggerShake();
            Instantiate(sandParticles, transform.position, Quaternion.identity);
            FindObjectOfType<AudioController>().PlayHourGlassHit();
            Destroy(gameObject);
        }
    }
}
