using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    [SerializeField] float enemyMinuteValue;
    [SerializeField] bool fadingRed = false;
    [SerializeField] ParticleSystem clockParticles;

    /* DEBUG ONLY
    private void Update()
    {
        
        if (Input.GetKeyDown("space"))
        {
            FindObjectOfType<ScoreKeeper>().IncrementHour();
            GameObject.Find("Player Clock").GetComponent<HandRotator>().AdvancePlayerTime();
        }
        
    }
    */

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy Clock") && otherCollider.isTrigger)
        {
            //Shake screen on all enemy clock hits, from both player and laser.
            FindObjectOfType<ScreenShake>().TriggerShake();

            //Only calculate enemy clock time if struck by player. Otherwise, enemy value is zero.
            if (gameObject.CompareTag("Player"))
            {
                //Get enemy minuteValue
                enemyMinuteValue = otherCollider.gameObject.GetComponent<HandRotator>().MinuteReturner();
            }

            if (enemyMinuteValue <= CalculateOwnMinutes())
            {
                if (otherCollider.isTrigger)
                {
                    //Turn off all circle colliders. 
                    StartCoroutine(DeactivateColliders(otherCollider.gameObject));

                    //Shoot laser.
                    otherCollider.gameObject.GetComponent<LaserLogic>().ActivateLaser();

                    //Trigger damage text.
                    otherCollider.gameObject.GetComponent<DamageTextLogic>().ActivateDamageText();

                    //Create clock bit particles.
                    Instantiate(clockParticles, otherCollider.gameObject.transform.position, Quaternion.identity);

                    //Play good hit audio.
                    FindObjectOfType<AudioController>().PlayGoodHit();

                    //Fade and destroy targeted clock.
                    FadeRenderers(otherCollider.gameObject);

                    //Increment score.
                    FindObjectOfType<ScoreKeeper>().IncrementHour();

                    //Move player hands forward rapidly.
                    if (gameObject.CompareTag("Player"))
                    {
                        GetComponent<HandRotator>().AdvancePlayerTime();
                    }
                    else
                    {
                        GameObject.Find("Player Clock").GetComponent<HandRotator>().AdvancePlayerTime();
                    }
                }
            }
            else if (enemyMinuteValue > CalculateOwnMinutes() && !fadingRed)
            {
                FindObjectOfType<PlayerHealth>().TakeHit();
                
                //Play bad hit audio.
                FindObjectOfType<AudioController>().PlayBadHit();
                fadingRed = true;
                GetRenderersToRed(gameObject);
            }
        }
    }

    private float CalculateOwnMinutes()
    {
        if (!GetComponent<HandRotator>())
        {
            return 720;
        }
        else
        {
            return GetComponent<HandRotator>().MinuteReturner();
        }
    }

    IEnumerator DeactivateColliders(GameObject clockWithColliders)
    {
        CircleCollider2D[] circleColliders = clockWithColliders.GetComponents<CircleCollider2D>();
        foreach (CircleCollider2D circleCollider in circleColliders)
        {
            circleCollider.enabled = false;
            //wait a moment after trigger collider is disabled, to ensure that the player bounces off of the non-trigger collider. 
            yield return new WaitForSeconds(0.1f);
        }  
    }

    private void FadeRenderers(GameObject objectToFade)
    {
        Renderer[] Renderers = objectToFade.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in Renderers)
        {
            if (!renderer.gameObject.CompareTag("Laser"))
            {
                StartCoroutine(Fader(renderer));
            }
        }
    }

    public IEnumerator Fader(Renderer rendererToFade)
    {
        Color color = rendererToFade.material.color;
        while (color.a > 0)
        {
            yield return new WaitForSeconds(0.05f);
            color.a -= 0.1f;
            rendererToFade.GetComponent<Renderer>().material.color = color;
        }
        yield return new WaitForSeconds(2f);
        Destroy(rendererToFade.gameObject);
    }

    private void GetRenderersToRed(GameObject objectToRed)
    {
        Renderer[] Renderers = objectToRed.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in Renderers)
        {
            StartCoroutine(RedFader(renderer));
        }
    }
    
    IEnumerator RedFader(Renderer rendererToRed)
    {        
        Color color = rendererToRed.GetComponent<Renderer>().material.color;
        color = Color.red;
        while (color.g< 1 && color.b< 1)
        {
            yield return new WaitForSeconds(0.05f);
            color.g += 0.1f;
            color.b += 0.1f;
            rendererToRed.GetComponent<Renderer>().material.color = color;
        }
        fadingRed = false;
    }
}
