using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour {

    internal PlayerManager[] players;

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

    PlayerManager gameWinner = new PlayerManager();

    [SerializeField] Text MessageText;
    [SerializeField] Button PlayAgainButton;
    [SerializeField] Button PauseGameButton;

    void Start ()
    {
        MessageText.enabled = false;
        StartWait = new WaitForSeconds(StartingDelay);
        EndWait = new WaitForSeconds(EndingDelay);
   
        SpawnAllPlayers();

        StartCoroutine(GameLoop());

       // PlayAgainButton.onClick.AddListener(delegate { StartCoroutine(GameLoop()); });
       // PauseGameButton.onClick.AddListener(PauseGame);
    }

    private void PauseGame()
    {
        throw new NotImplementedException();
    }

    IEnumerator GameLoop()
    {
        ResetPlayers();
        MessageText.enabled = true;
        MessageText.text = "Game Start";
        yield return StartWait;
        MessageText.enabled = false;

        if (gameWinner != null)
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

        if (!GameStaticValues.multiplayer)
        {
            players[0].instance.GetComponent<PlayerScriptKim>().ResetMaxHealth();
            players[1].instance.GetComponent<BotHealth>().ResetMaxHealth();
        }
        else
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].instance.GetComponent<PlayerScriptKim>().ResetMaxHealth();
            }
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
        players[0].playerIndex = 1;
        players[0].spawnPosition = spawner1;

        if (GameStaticValues.multiplayer)
        {
            players[1].instance = Instantiate(CharactersForPlayer[PlayerPrefs.GetInt("selectedCharacterP2")].CharacterPrefab,
                    spawner2.position, spawner2.rotation) as GameObject;
            players[1].playerIndex = 2;
            players[1].spawnPosition = spawner2;
        }

        else
        {
            players[1].instance =  Instantiate(CharactersForBot[(int)GameStaticValues.bot].CharacterPrefab,
                    spawner2.position, spawner2.rotation) as GameObject;
            players[1].spawnPosition = spawner2;
        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
