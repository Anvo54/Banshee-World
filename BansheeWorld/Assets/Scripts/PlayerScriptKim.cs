using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptKim : MonoBehaviour
{
    GameObject GameSceneManagerRef;
    GameSceneManager gameSceneManager;

    public int playerIndex;

    void Start()
    {
        GameSceneManagerRef = GameObject.FindGameObjectWithTag("GameManager");
        gameSceneManager = GameSceneManagerRef.GetComponent<GameSceneManager>();
        
        for (int i = 0; i < gameSceneManager.players.Length; i++)
        {
            if (gameObject == gameSceneManager.players[i].instance)
            {
                playerIndex = gameSceneManager.players[i].playerIndex;
                Debug.Log("Player1 index: " + playerIndex);
            }
        }
    }

}
