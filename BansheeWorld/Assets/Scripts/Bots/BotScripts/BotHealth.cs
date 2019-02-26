using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotHealth : MonoBehaviour
{

    [SerializeField] List<CharacterSO> bots = new List<CharacterSO>();
    int selectedBotIndex;

    [SerializeField] GameObject healthBarCanvas;

    int maxHealth;
    float currentHealth;
    float healthRatio;

    public bool isBotDead;

    [SerializeField] Image healthBarImage;
    [SerializeField] Text playerNameTag;

    void Start()
    {
        isBotDead = false;
        selectedBotIndex = (int)GameStaticValues.bot;

        healthBarCanvas.GetComponent<Canvas>().worldCamera = 
                 GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        maxHealth = bots[selectedBotIndex].HealthPoint;
        currentHealth = maxHealth;

        playerNameTag.text = "Bot";
    }


    internal void AddjustCurrentHealth(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthRatio = currentHealth / maxHealth;
        
        healthBarImage.rectTransform.localScale = new Vector3(healthRatio, 1, 1);
    }

    internal void Die()
    {
        isBotDead = true;
        gameObject.SetActive(false);
    }

    internal void ResetMaxHealth()
    {
        isBotDead = false;
        currentHealth = maxHealth;
    }
}