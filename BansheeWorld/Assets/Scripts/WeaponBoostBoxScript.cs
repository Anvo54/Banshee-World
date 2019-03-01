using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoostBoxScript : MonoBehaviour
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
        Destroy(gameObject);

        if (other.transform.root.tag == "Player")
        {
            gameSceneManager.weaponInHand = true;

            for (int i = 0; i < gameSceneManager.players.Length; i++)
            {
                if (other.transform.root.gameObject == gameSceneManager.players[i].instance)
                {
                    gameSceneManager.whoHasWeapon = gameSceneManager.players[i].playerIndex;   
                }   
            }
        }

        else
        {
            return;
        }
    }
}