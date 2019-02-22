using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptKim : MonoBehaviour
{
    GameObject GameSceneManagerRef;
    GameSceneManager gameSceneManager;

    public int playerIndex;

  //  [SerializeField] Image healthBarImage;
    [SerializeField] Text playerNameTag;

    //damage will be deleted when player has healthscript
    public float damage;

    void Awake()
    {
        GameSceneManagerRef = GameObject.FindGameObjectWithTag("GameManager");
        gameSceneManager = GameSceneManagerRef.GetComponent<GameSceneManager>();

        for (int i = 0; i < gameSceneManager.players.Length; i++)
        {
            if (gameObject == gameSceneManager.players[i].instance)
            {
                playerIndex = gameSceneManager.players[i].playerIndex;
            }
        }

        playerNameTag.text = "P" + (playerIndex +1).ToString();
    }

    //also this method no need when player has 
    public void ResetMaxHealth()
    { }

}
