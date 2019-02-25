using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] List<CharacterSO> characters = new List<CharacterSO>();
    int playerIndex;
    int selectedCharacterIndex;
    
    [SerializeField] GameObject healthBarCanvas;

    int maxHealth;
    float currentHealth;
    float healthRatio;

    public bool isDead;

    [SerializeField] Image healthBarImage;
    [SerializeField] Text playerNameTag;

    void Start()
    {
        isDead = false;

        playerIndex = gameObject.GetComponent<PlayerScriptKim>().playerIndex;

        if (playerIndex == 1)
            selectedCharacterIndex = PlayerPrefs.GetInt("selectedCharacter");
        else
            selectedCharacterIndex = PlayerPrefs.GetInt("selectedCharacterP2");


        healthBarCanvas.GetComponent<Canvas>().worldCamera =
                 GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        maxHealth = characters[selectedCharacterIndex].HealthPoint;
        currentHealth = maxHealth;

        playerNameTag.text = "P" + playerIndex.ToString();
    }

    internal void TakeDamage(float damage)
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
        isDead = true;
        gameObject.SetActive(false);
    }

    internal void ResetMaxHealth()
    {
        isDead = false;
        currentHealth = maxHealth;
    }
}
