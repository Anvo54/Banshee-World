using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptKim : MonoBehaviour
{
    GameObject GameSceneManagerRef;
    GameSceneManager gameSceneManager;

    public static int playerIndex;

    public float damage;

    void Start()
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
    }

    public void ResetMaxHealth()
    { }

}
