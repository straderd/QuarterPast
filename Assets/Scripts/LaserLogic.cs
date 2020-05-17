using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLogic : MonoBehaviour
{
    [SerializeField] GameObject minuteLaser;
    [SerializeField] bool detachChild = false;
    [SerializeField] float laserSpeed = 15f;

    private void Update()
    {
        if (detachChild == true && minuteLaser != null)
        {
            minuteLaser.transform.parent = null;
            minuteLaser.transform.Translate(Vector3.up * Time.deltaTime * laserSpeed);
        }
        else
        {
            return;
        }

    }

    public void ActivateLaser()
    {
        minuteLaser.SetActive(true);
        detachChild = true;
    }
}
