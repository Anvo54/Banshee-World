using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{

    public PlayerManager[] players;
    
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
    [SerializeField] Text GameOverText;
    [SerializeField] Button PlayAgainButton;
    [SerializeField] Button PauseGameButton;

    [SerializeField] GameObject cinemachineTargetGroup;
    CinemachineTargetGroup targetGroup;
    List<CinemachineTargetGroup.Target> targets = new List<CinemachineTargetGroup.Target>();

    [SerializeField] float durationOfMatching = 120;
    internal float gameTimer;

    [SerializeField] float durationOfBox = 10;
    float boxTimer = 0;
    [SerializeField] float durationOfWeaponHolding = 30;
    float weaponHoldingTimer = 0;

    bool gameStarted;
    bool isBoxSpawned;

    public bool isGameOver;
    public string gameOverMessage = "WHO WINS?";

    int winGoldPointMultiplayer = 10;
    int winGoldPointWithBot = 5;
    int loseGoldPointMultiplayer = 5;

    [SerializeField] float firstBoxDropTime = 0;
    [SerializeField] float secondBoxDropDelay = 60;
    [SerializeField] Transform[] spawningPositions;
    [SerializeField] GameObject weaponBox;
    GameObject box;

    internal bool weaponInHand;
    public int whoHasWeapon;


    int headDamageGlobal = 30;
    int bodyDamageGlobal = 20;

    void Start ()
    {
        isGameOver = false;
        gameStarted = false;
        isBoxSpawned = false;
        targetGroup = cinemachineTargetGroup.GetComponent<CinemachineTargetGroup>();

        MessageText.enabled = false;
        StartWait = new WaitForSeconds(StartingDelay);
        EndWait = new WaitForSeconds(EndingDelay);
   
        SpawnAllPlayers();
        
        StartCoroutine(GameLoop());

        PlayAgainButton.onClick.AddListener(delegate { StartCoroutine(GameLoop()); });
        PauseGameButton.onClick.AddListener(PauseGame);
 //       GameOverText.text = gameOverMessage;   
    }

    private void PauseGame()
    {
     
    }

    public void PlayAgain()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        gameWinner = null;
        gameOverMessage = "WHO WINS?";
        isGameOver = false;

        gameTimer = durationOfMatching;

        ResetPlayers();
        MessageText.enabled = true;
        MessageText.text = "Game Start";
        yield return StartWait;
        MessageText.enabled = false;

        gameStarted = true;

        firstBoxDropTime = UnityEngine.Random.Range(9, 19);

        yield return new WaitForSecondsRealtime(firstBoxDropTime);
        SpawnBox();

        yield return new WaitForSecondsRealtime(secondBoxDropDelay);
        SpawnBox();
        
        if (gameWinner != null)
        {
            if (gameWinner == players[0])
            {
                gameOverMessage = "Player1 wins";

                if (GameStaticValues.multiplayer)
                {
                    GameStaticValues.player1Win++;
                    GameStaticValues.player1Coin += winGoldPointMultiplayer;

                    GameStaticValues.player2Coin -= loseGoldPointMultiplayer;
                    if(GameStaticValues.player2Coin <=0)
                    {
                        GameStaticValues.player2Coin = 0;
                    }
                }
                else
                {
                    GameStaticValues.player1Win++;
                    GameStaticValues.player1Coin += winGoldPointWithBot;
                }
            }

            else if(gameWinner == players[1])
            {
                if (GameStaticValues.multiplayer)
                {
                    gameOverMessage = "Player2 wins";
                    GameStaticValues.player2Win++;
                    GameStaticValues.player2Coin += winGoldPointMultiplayer;

                    GameStaticValues.player1Coin -= loseGoldPointMultiplayer;
                    if (GameStaticValues.player1Coin <= 0)
                    {
                        GameStaticValues.player1Coin = 0;
                    }
                }
                else
                {
                    gameOverMessage = "Bot wins";
                }
            }

            GameOverText.text = gameOverMessage;

            PlayerPrefs.SetInt("Player1Coin", GameStaticValues.player1Coin);
            PlayerPrefs.SetInt("Player1WinScore", GameStaticValues.player1Win);
            PlayerPrefs.SetInt("Player2Coin", GameStaticValues.player2Coin);
            PlayerPrefs.SetInt("Player2WinScore", GameStaticValues.player2Win);
        }

        
        yield return EndWait;
    }

    private void SpawnBox()
    {
        isBoxSpawned = true;

        int tempIndex = UnityEngine.Random.Range(0, spawningPositions.Length);
        box = Instantiate(weaponBox, spawningPositions[tempIndex].position, spawningPositions[tempIndex].rotation);
    }

    private void RemoveBox()
    {
        if(box != null)
        {
            Destroy(box);
        }
    }

    private void ResetPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }

        if (!GameStaticValues.multiplayer)
        {
            players[0].instance.GetComponent<PlayerHealth>().ResetMaxHealth();
            players[1].instance.GetComponent<BotHealth>().ResetMaxHealth();
        }
        else
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].instance.GetComponent<PlayerHealth>().ResetMaxHealth();
            }
        }
    }

    PlayerManager GetGameWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].instance.activeSelf)
            {
                gameWinner = players[i];
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


        targets.Add(new CinemachineTargetGroup.Target { target = players[0].instance.transform, radius = 2f, weight = 1f });

        if (GameStaticValues.multiplayer)
        {
            players[1].instance = Instantiate(CharactersForPlayer[PlayerPrefs.GetInt("selectedCharacterP2")].CharacterPrefab,
                    spawner2.position, spawner2.rotation) as GameObject;
            players[1].playerIndex = 2;
            players[1].spawnPosition = spawner2;

           
            targets.Add(new CinemachineTargetGroup.Target { target = players[1].instance.transform, radius = 2f, weight = 1f });
        }

        else
        {
            players[1].instance =  Instantiate(CharactersForBot[(int)GameStaticValues.bot].CharacterPrefab,
                    spawner2.position, spawner2.rotation) as GameObject;
            players[1].spawnPosition = spawner2;

           
            targets.Add(new CinemachineTargetGroup.Target { target = players[1].instance.transform, radius = 2f, weight = 1f });
        }

        targetGroup.m_Targets = targets.ToArray();
    }

   
    void FixedUpdate ()
    {
        GameOverText.text = gameOverMessage;

        if(weaponInHand)
        {
            weaponHoldingTimer += Time.deltaTime;

            if (whoHasWeapon == 1)
            {
                players[0].instance.GetComponent<PlayerMovement>().bodyDamage  =
                            bodyDamageGlobal * (1 + GameStaticValues.player1WeaponLevel * 0.1f);
                players[0].instance.GetComponent<PlayerMovement>().headDamage =
                            headDamageGlobal * (1 + GameStaticValues.player1WeaponLevel * 0.1f);
            }
            else if (whoHasWeapon == 2)
            {
                players[1].instance.GetComponent<PlayerMovement>().bodyDamage =
                           bodyDamageGlobal * (1 + GameStaticValues.player2WeaponLevel * 0.1f);
                players[1].instance.GetComponent<PlayerMovement>().headDamage =
                           headDamageGlobal * (1 + GameStaticValues.player2WeaponLevel * 0.1f);

                //players[1].instance.GetComponent<PlayerMovement>().damage =
                //    CharactersForPlayer[PlayerPrefs.GetInt("selectedCharacterP2")].AttackPoint *
                //    (1 + GameStaticValues.player2WeaponLevel * 0.1f);
            }
                       
            if (weaponHoldingTimer >= durationOfWeaponHolding)
            {
                whoHasWeapon = 0;
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].instance.GetComponent<PlayerMovement>().bodyDamage = bodyDamageGlobal;
                    players[i].instance.GetComponent<PlayerMovement>().headDamage = headDamageGlobal;
                }
                weaponInHand = false;
            }
        }

        if (gameStarted)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer <= 0)
            {
                isGameOver = true;
                if (players[0].instance.activeSelf && players[1].instance.activeSelf)
                {
                    gameOverMessage = "Draw";
                    GameOverText.text = gameOverMessage;
                }
               
                gameStarted = false;
                gameTimer = durationOfMatching;
            }
        }

        if (isBoxSpawned)
        {
            boxTimer += Time.deltaTime;
            if(boxTimer >= durationOfBox)
            {
                RemoveBox();
                isBoxSpawned = false;
                boxTimer = 0;
            }
        }

        if (isGameOver)
        {
            StopAllCoroutines();
            return;
        }
        else
        {
            if (GameStaticValues.multiplayer)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    if(players[i].instance.GetComponent<PlayerHealth>().isDead)
                    {
                        GetGameWinner();
                        isGameOver = true;
                    }
                }
            }

            else
            {
                if (players[0].instance.GetComponent<PlayerHealth>().isDead)
                {
                    GetGameWinner();
                    isGameOver = true;
                }
                if (players[1].instance.GetComponent<BotHealth>().isBotDead)
                {
                    GetGameWinner();
                    isGameOver = true;
                }
            }
        }
    }
}
