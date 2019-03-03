using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneScript : MonoBehaviour
{
    GameObject GameSceneManagerRef;
    GameSceneManager gameSceneManager;

    private void Start()
    {
        GameSceneManagerRef = GameObject.FindGameObjectWithTag("GameManager");
        gameSceneManager = GameSceneManagerRef.GetComponent<GameSceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameStaticValues.multiplayer)
        { 
            for (int i = 0; i < gameSceneManager.players.Length; i++)
            {
                if (other.transform.root.gameObject == gameSceneManager.players[i].instance)
                {
                    gameSceneManager.players[i].instance.GetComponent<PlayerHealth>().isDead = true;
                    gameSceneManager.players[i].instance.SetActive(false);
                }
            }
        }

        else
        {
            if (other.transform.root.gameObject == gameSceneManager.players[0].instance)
            {
                gameSceneManager.players[0].instance.GetComponent<PlayerHealth>().isDead = true;
                gameSceneManager.players[0].instance.SetActive(false);
            }

            if (other.transform.root.gameObject == gameSceneManager.players[1].instance)
            {
                gameSceneManager.players[1].instance.GetComponent<BotHealth>().isBotDead = true;
                gameSceneManager.players[1].instance.SetActive(false);
            }
        }
    }
}
