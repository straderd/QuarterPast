using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;
    [SerializeField] TextMeshProUGUI healthText;
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealth();
    }

   private void UpdateHealth()
    {
        healthText.text = (currentHealth + "/" + maxHealth);
    }
    
    public void TakeHit()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        UpdateHealth();
        if (currentHealth <= 0)
        {
            TriggerGameOver();
        }
    }

    public void AddHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHealth();
        }
    }

    private void TriggerGameOver()
    {
        FindObjectOfType<SceneLogic>().GameOverTriggered();
        Destroy(GameObject.Find("Player Clock"));
    }
}
