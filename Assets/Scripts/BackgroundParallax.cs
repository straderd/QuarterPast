using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{

    [SerializeField] float backgroundSpeed = 0.5f;
    [SerializeField] float parallaxVariable = 0.5f;
    [SerializeField] GameObject secondaryBackground;
    [SerializeField] GameObject tertiaryBackground;

    void Update()
    {
        gameObject.transform.Translate(Vector3.left * Time.deltaTime * backgroundSpeed);
        ParallaxReset(gameObject);
        secondaryBackground.transform.Translate(Vector3.left * Time.deltaTime * backgroundSpeed * parallaxVariable);
        ParallaxReset(secondaryBackground);
        tertiaryBackground.transform.Translate(Vector3.left * Time.deltaTime * backgroundSpeed * parallaxVariable * parallaxVariable);
        ParallaxReset(tertiaryBackground);
    }

    private void ParallaxReset(GameObject backgroundToReset)
    {
        if (backgroundToReset.transform.position.x <= -17.775)
        {
            backgroundToReset.transform.position = new Vector3(0, 0, 0);
        }
    }

    public void IncreaseBackgroundSpeed()
    {
        backgroundSpeed += 0.25f;
    }
}
