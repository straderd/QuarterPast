using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextLogic : MonoBehaviour
{
    [SerializeField] GameObject damageTextParent;

    public void ActivateDamageText()
    {
        damageTextParent.SetActive(true);
    }
}

