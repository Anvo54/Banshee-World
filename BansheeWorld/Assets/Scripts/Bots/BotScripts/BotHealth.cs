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

    GameObject target;

    int maxHealth;
    float currentHealth;
    float healthRatio;

    [SerializeField] Image healthBarImage;
    [SerializeField] Text playerNameTag;
    float damageFromPlayer =0;
    float damageToPlayer;

    //GameObject GameSceneManagerRef;
    //GameSceneManager gameSceneManager;

    void Start()
    {
        selectedBotIndex = (int)GameStaticValues.bot;

        healthBarCanvas.GetComponent<Canvas>().worldCamera = 
                 GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        target = GameObject.FindGameObjectWithTag("Player");

        maxHealth = bots[selectedBotIndex].HealthPoint;
        
        currentHealth = maxHealth;
        damageFromPlayer = target.GetComponent<PlayerScriptKim>().damage;

        playerNameTag.text = "Bot";
    }


    void Update()
    {
        AddjustCurrentHealth(damageFromPlayer);
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

    private void Die()
    {
        gameObject.SetActive(false);
    }

    internal void ResetMaxHealth()
    {
        currentHealth = maxHealth;
    }
}