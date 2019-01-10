using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    [SerializeField] PlayerManager[] players;

    [SerializeField]
    Transform spawner1, spawner2;

    [SerializeField] List<CharacterSO> CharactersForPlayer;
    [SerializeField] List<CharacterSO> CharactersForBot;

    [SerializeField] float StartDelay;

    private WaitForSeconds StartWait;

 
    void Start ()
    {
        StartWait = new WaitForSeconds(StartDelay);
   
        SpawnAllPlayers();	
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
