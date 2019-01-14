using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour {

    [SerializeField] PlayerManager[] players;

    [SerializeField]
    Transform spawner1;
    [SerializeField]
    Transform spawner2;

    [SerializeField] List<CharacterSO> CharactersForPlayer;
    [SerializeField] List<CharacterSO> CharactersForBot;

    [SerializeField] float StartingDelay;
    private WaitForSeconds StartWait;
    [SerializeField] float EndingDelay;
    WaitForSeconds EndWait;

    PlayerManager gameWinner;

    [SerializeField] Text MessageText;
 
    void Start ()
    {
        StartWait = new WaitForSeconds(StartingDelay);
        EndWait = new WaitForSeconds(EndingDelay);
   
        SpawnAllPlayers();

        StartCoroutine(GameLoop());
	}

    IEnumerator GameLoop()
    {
        ResetPlayers();
        MessageText.text = "Game Start";
        yield return StartWait;

        if(gameWinner != null)
        {
            //gameWinner.numberOfWins++;
            if(gameWinner == players[0])
            {
                GameStaticValues.player1Win++;
            }
            else
            {
                GameStaticValues.player2Win++;
            }
        }
        

        yield return EndWait;
    }

    private void ResetPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }
    }

    PlayerManager GetGameWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].instance.activeSelf)
            {
                return players[i];
            }
        }

        return null;
    }

    private void SpawnAllPlayers()
    {
        players[0].instance = Instantiate(CharactersForPlayer[PlayerPrefs.GetInt("selectedCharacter")].CharacterPrefab,
                    spawner1.position, spawner1.rotation) as GameObject;
        players[0].playerNumber = 1;

        if(GameStaticValues.multiplayer)
        {
            players[1].instance = Instantiate(CharactersForPlayer[PlayerPrefs.GetInt("selectedCharacterP2")].CharacterPrefab,
                    spawner2.position, spawner2.rotation) as GameObject;
            players[1].playerNumber = 2;
        }

        else
        {
            players[1].instance =  Instantiate(CharactersForBot[(int)GameStaticValues.bot].CharacterPrefab,
                    spawner2.position, spawner2.rotation) as GameObject;
        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
