using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuSceneManager : MonoBehaviour {
    
    [SerializeField]
    List<CharacterSO> characters = new List<CharacterSO>();

    [SerializeField]
    GameObject LevelMapCanvas;
    [SerializeField]
    GameObject Player1BansheeCanvas;
    [SerializeField]
    GameObject Player2BansheeCanvas;

    [Header("Main Canvas")]
    [SerializeField]
    GameObject Player2Panel;
    [SerializeField]
    Button LevelSelectionButton;
    [SerializeField]
    Button CharacterSelectionButton;
    [SerializeField]
    Button CharacterSelectionButtonForPlayer2;
    [SerializeField]
    Button Player1ReadyButton;
    [SerializeField]
    Button Player2ReadyButton;

    [SerializeField] Toggle MultiplayerToggle;
    [SerializeField] GameObject BotToggles;
    [SerializeField]
    Image CharacterImageP1InMainCanvas;
    [SerializeField]
    Image CharacterImageP2InMainCanvas;
    [SerializeField]
    Text ScoreTextP1;
    [SerializeField]
    Text ScoreTextP2;
    [SerializeField]
    Text CoinTextP1;
    [SerializeField]
    Text CoinTextP2;

    [SerializeField]
    Text NoticeText;
    [SerializeField]
    Button DataResetButton;

    [Header("Player1 Canvas")]
    [SerializeField]
    GameObject CharacterPanelInPlayer1;
    [SerializeField]
    Button BackButtonInCharactersPanel;
    [SerializeField]
    Button BackButtonInCharacterPanel;
    [SerializeField]
    Button SelectButtonInCharacterPanel;
    [SerializeField]
    Button UpgradeButtonInCharacterPanel;
    [SerializeField]
    List<Button> CharacterButtons = new List<Button>();
    [SerializeField]
    Image CharacterImageInCharacterPanel;
    [SerializeField]
    Text CharacterDetais;

    [SerializeField]
    Image WeaponImageInCharacterPanel;
    [SerializeField]
    Text CoinNeeded;
    [SerializeField]
    Text MessageTextP1;

    [Header("Player2 Canvas")]
    [SerializeField]
    GameObject CharacterPanelInPlayer2;
    [SerializeField]
    Button BackButtonInCharactersPanelP2;
    [SerializeField]
    Button BackButtonInCharacterPanelP2;
    [SerializeField]
    Button SelectButtonInCharacterPanelP2;
    [SerializeField]
    Button UpgradeButtonInCharacterPanelP2;
    [SerializeField]
    List<Button> CharacterButtonsP2 = new List<Button>();

    [SerializeField]
    Image CharacterImageInCharacterPanelP2;
    [SerializeField]
    Text CharacterDetaisP2;
    [SerializeField]
    Image WeaponImageInCharacterPanelP2;
    [SerializeField]
    Text CoinNeededP2;
    [SerializeField]
    Text MessageTextP2;

    [Header("Level Selection Canvas")]
    [SerializeField]
    Button BackButtonInLevelMapCanvas;
    [SerializeField]
    GameObject LevelToggles;

    int tempIndex;
    int tempIndexP2;
    bool isPlayer1Ready, isPlayer2Ready;

    private void Awake()
    {

        if (!PlayerPrefs.HasKey("selectedCharacter"))
        {
            PlayerPrefs.SetInt("selectedCharacter", 0);
        }
        if (!PlayerPrefs.HasKey("selectedCharacterP2"))
        {
            PlayerPrefs.SetInt("selectedCharacterP2", 0);
        }

        if (!PlayerPrefs.HasKey("PlayedLevel"))
        {
            PlayerPrefs.SetInt("PlayedLevel", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("PlayedLevel") == 0)
                GameStaticValues.level = Level.Level1;
            else if (PlayerPrefs.GetInt("PlayedLevel") == 1)
                GameStaticValues.level = Level.Level2;
            else if (PlayerPrefs.GetInt("PlayedLevel") == 2)
                GameStaticValues.level = Level.Level3;

            else
                GameStaticValues.level = Level.Level1;
        }

        if (!PlayerPrefs.HasKey("Player1WinScore"))
        {
            PlayerPrefs.SetInt("Player1WinScore", GameStaticValues.player1Win);
        }
        else
        {
            GameStaticValues.player1Win = PlayerPrefs.GetInt("Player1WinScore");
        }
        if (!PlayerPrefs.HasKey("Player2WinScore"))
        {
            PlayerPrefs.SetInt("Player2WinScore", GameStaticValues.player2Win);
        }
        else
        {
            GameStaticValues.player2Win = PlayerPrefs.GetInt("Player2WinScore");
        }


        if (!PlayerPrefs.HasKey("Player1Coin"))
        {
            PlayerPrefs.SetInt("Player1Coin", GameStaticValues.player1Coin);
        }
        else
        {
            GameStaticValues.player1Coin = PlayerPrefs.GetInt("Player1Coin");
        }
        if (!PlayerPrefs.HasKey("Player2Coin"))
        {
            PlayerPrefs.SetInt("Player2Coin", GameStaticValues.player2Coin);
        }
        else
        {
            GameStaticValues.player2Coin = PlayerPrefs.GetInt("Player2Coin");
        }

        if (!PlayerPrefs.HasKey("Player1WeaponLevel"))
        {
            PlayerPrefs.SetInt("Player1WeaponLevel", GameStaticValues.player1WeaponLevel);
        }
        else
        {
            GameStaticValues.player1WeaponLevel = PlayerPrefs.GetInt("Player1WeaponLevel");
        }
        if (!PlayerPrefs.HasKey("Player2WeaponLevel"))
        {
            PlayerPrefs.SetInt("Player2WeaponLevel", GameStaticValues.player2WeaponLevel);
        }
        else
        {
            GameStaticValues.player2WeaponLevel = PlayerPrefs.GetInt("Player2WeaponLevel");
        }
    }

    void Start ()
    {
        DataResetButton.onClick.AddListener(ResetData);
        Player1BansheeCanvas.SetActive(false);
        Player2BansheeCanvas.SetActive(false);
        CharacterPanelInPlayer1.SetActive(false);
        CharacterPanelInPlayer2.SetActive(false);
        LevelMapCanvas.SetActive(false);
        NoticeText.enabled = false;

        for (int i = 0; i < CharacterButtons.Count; i++)
        {
            int index = i;
            CharacterButtons[i].onClick.AddListener( delegate {OpenCharacterPanel(1, index);} );
        }

        for (int i = 0; i < CharacterButtons.Count; i++)
        {
            int index = i;
            CharacterButtonsP2[i].onClick.AddListener(delegate { OpenCharacterPanel(2, index); });
        }
        LevelSelectionButton.onClick.AddListener(OpenLevelMapPanel);
        BackButtonInCharacterPanel.onClick.AddListener(delegate { ClosePanel("CharacterPanel"); });
        BackButtonInCharactersPanel.onClick.AddListener(delegate { ClosePanel("CharactersPanel"); });
        BackButtonInCharacterPanelP2.onClick.AddListener(delegate { ClosePanel("CharacterPanelP2"); });
        BackButtonInCharactersPanelP2.onClick.AddListener(delegate { ClosePanel("CharactersPanelP2"); });
        CharacterSelectionButton.onClick.AddListener(OpenCharactersPanel);
        CharacterSelectionButtonForPlayer2.onClick.AddListener(OpenCharactersPanelP2);

        Player1ReadyButton.onClick.AddListener(SetPlayer1ReadyForPlay);
        Player2ReadyButton.onClick.AddListener(SetPlayer2ReadyForPlay);

        SelectButtonInCharacterPanel.onClick.AddListener(delegate { Select(1); });
        SelectButtonInCharacterPanelP2.onClick.AddListener(delegate { Select(2); });

        UpgradeButtonInCharacterPanel.onClick.AddListener(delegate { UpgradeWeapon(1); });
        UpgradeButtonInCharacterPanelP2.onClick.AddListener(delegate { UpgradeWeapon(2); });
       

        MultiplayerToggle.onValueChanged.AddListener((value) => { SetMultiplayer(value); });
        MultiplayerToggle.onValueChanged.AddListener(isMultiplayerOn => BotToggles.SetActive(!isMultiplayerOn));
        MultiplayerToggle.onValueChanged.AddListener(isMultiplayerOn => Player2Panel.SetActive(isMultiplayerOn));

        BotToggles.transform.GetChild(0).GetComponent<Toggle>().onValueChanged.AddListener
            (value => { SetBot1(value); });
        BotToggles.transform.GetChild(1).GetComponent<Toggle>().onValueChanged.AddListener
            (value => { SetBot2(value); });
        BotToggles.transform.GetChild(2).GetComponent<Toggle>().onValueChanged.AddListener
            (value => { SetBot3(value); });
        BotToggles.transform.GetChild(2).GetComponent<Toggle>().onValueChanged.AddListener
            (value => { SetBot4(value); });

        CharacterImageP1InMainCanvas.sprite = characters[PlayerPrefs.GetInt("selectedCharacter")].CharacterImage;
        CharacterImageP2InMainCanvas.sprite = characters[PlayerPrefs.GetInt("selectedCharacterP2")].CharacterImage;

    
        BotToggles.transform.GetChild((int)GameStaticValues.bot).GetComponent<Toggle>().isOn = true;

        MultiplayerToggle.isOn = GameStaticValues.multiplayer;

        LevelToggles.transform.GetChild(0).GetComponent<Toggle>().onValueChanged.AddListener
            (value => { SetLevel1(value); });
        LevelToggles.transform.GetChild(1).GetComponent<Toggle>().onValueChanged.AddListener
          (value => { SetLevel2(value); });
        LevelToggles.transform.GetChild(2).GetComponent<Toggle>().onValueChanged.AddListener
          (value => { SetLevel3(value); });

        LevelToggles.transform.GetChild((int)GameStaticValues.level).GetComponent<Toggle>().isOn = true;
        BackButtonInLevelMapCanvas.onClick.AddListener(delegate { ClosePanel("LevelMap"); } );
    }

    private void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        ScoreTextP1.text = "SCORE: " + GameStaticValues.player1Win.ToString();
        ScoreTextP2.text = "SCORE: " + GameStaticValues.player2Win.ToString();

        CoinTextP1.text = GameStaticValues.player1Coin.ToString();
        CoinTextP2.text = GameStaticValues.player2Coin.ToString();
    }

    private void OpenLevelMapPanel()
    {
        LevelMapCanvas.SetActive(true);
    }

    private void SetLevel1(bool value)
    {
        if(value)
        {
            GameStaticValues.level = Level.Level1;
        }
    }
    private void SetLevel2(bool value)
    {
        if (value)
        {
            GameStaticValues.level = Level.Level2;
        }
    }
    private void SetLevel3(bool value)
    {
        if (value)
        {
            GameStaticValues.level = Level.Level3;
        }
    }

    private void SetPlayer1ReadyForPlay()
    {
        isPlayer1Ready = true;

        if(!GameStaticValues.multiplayer)
        {
            StartGame();
            return;
        }

        if (isPlayer2Ready)
        {
            StartGame();
        }
        else
        {
            StartCoroutine(ShowMessage("Wait for Player2", 2, NoticeText));
            return;
        }
    }

    private void SetPlayer2ReadyForPlay()
    {
        isPlayer2Ready = true;

        if(isPlayer1Ready)
        {
            StartGame();
        }
        else
        {
            StartCoroutine(ShowMessage("Wait for Player1", 2, NoticeText));
            return;
        }
    }

   IEnumerator ShowMessage(string message, float second, Text text)
   {
        //NoticeText.enabled = true;
        //NoticeText.text = message;
        //yield return new WaitForSeconds(second);
        //NoticeText.enabled = false;

        text.enabled = true;
        text.text = message;
        yield return new WaitForSeconds(second);
        text.enabled = false;
    }

   

    private void SetBot1(bool value)
    {
        if(value)
        {
            GameStaticValues.bot = Character.Character1;
        }
    }
    private void SetBot2(bool value)
    {
        if (value)
        {
            GameStaticValues.bot = Character.Character2;
        }
    }
    private void SetBot3(bool value)
    {
        if (value)
        {
            GameStaticValues.bot = Character.Character3;
        }
    }
    private void SetBot4(bool value)
    {
        if (value)
        {
            GameStaticValues.bot = Character.Character4;
        }
    }

    private void OpenCharactersPanel()
    {
        Player1BansheeCanvas.SetActive(true);
    }
    private void OpenCharactersPanelP2()
    {
        Player2BansheeCanvas.SetActive(true);
    }

    private void SetMultiplayer(bool value)
    {
        GameStaticValues.multiplayer = value;
    }

    public void StartGame()
    {
        int sceneNumber = (int)GameStaticValues.level + 2;
        SceneManager.LoadScene(sceneNumber);
    }

    private void ClosePanel(string panelName)
    {
        if(panelName == "CharacterPanel")
        {
            CharacterPanelInPlayer1.SetActive(false);
        }
        if(panelName == "CharactersPanel")
        {
            Player1BansheeCanvas.SetActive(false);
        }
        if (panelName == "CharacterPanelP2")
        {
            CharacterPanelInPlayer2.SetActive(false);
        }
        if (panelName == "CharactersPanelP2")
        {
            Player2BansheeCanvas.SetActive(false);
        }
        if (panelName == "LevelMap")
        {
            LevelMapCanvas.SetActive(false);
        }
    }

    private void OpenCharacterPanel(int playerIndex, int characterIndex)
    {
        if(playerIndex == 1)
        {
            CharacterPanelInPlayer1.SetActive(true);

            CharacterImageInCharacterPanel.sprite = characters[characterIndex].CharacterImage;
//            CharacterDetais.text = characters[characterIndex].ToString();

            if (GameStaticValues.player1WeaponLevel == 1)
            {
                WeaponImageInCharacterPanel.sprite = characters[characterIndex].WeaponBasicImage;
                CoinNeeded.text = GameStaticValues.player1Coin.ToString() + "/" +
                    characters[characterIndex].WeaponTo1UpgradeCost.ToString();
                CharacterDetais.text = characters[characterIndex].ToString() + " + " + 
                    characters[characterIndex].WeaponBasicPower.ToString();
            }
            if (GameStaticValues.player1WeaponLevel == 2)
            {
                WeaponImageInCharacterPanel.sprite = characters[characterIndex].WeaponUp1Image;
                CoinNeeded.text = GameStaticValues.player1Coin.ToString() + "/" 
                    + characters[characterIndex].WeaponTo2UpgradeCost.ToString();
                CharacterDetais.text = characters[characterIndex].ToString() + " + " +
                   characters[characterIndex].WeaponUp1Power.ToString();
            }
            if (GameStaticValues.player1WeaponLevel == 3)
            {
                WeaponImageInCharacterPanel.sprite = characters[characterIndex].WeaponUp2Image;
                CoinNeeded.text = "MAXED";
                CharacterDetais.text = characters[characterIndex].ToString() + " + " +
                   characters[characterIndex].WeaponUp2Power.ToString();
            }

            tempIndex = characterIndex;
        }

        if(playerIndex == 2)
        {
            CharacterPanelInPlayer2.SetActive(true);

            CharacterImageInCharacterPanelP2.sprite = characters[characterIndex].CharacterImage;
//            CharacterDetaisP2.text = characters[characterIndex].ToString();

            if (GameStaticValues.player2WeaponLevel == 1)
            {
                WeaponImageInCharacterPanelP2.sprite = characters[characterIndex].WeaponBasicImage;
                CoinNeededP2.text = GameStaticValues.player2Coin.ToString() + "/" +
                    characters[characterIndex].WeaponTo1UpgradeCost.ToString();
                CharacterDetaisP2.text = characters[characterIndex].ToString() + " + " +
                   characters[characterIndex].WeaponBasicPower.ToString();
            }
            if (GameStaticValues.player2WeaponLevel == 2)
            {
                WeaponImageInCharacterPanelP2.sprite = characters[characterIndex].WeaponUp1Image;
                CoinNeededP2.text = GameStaticValues.player2Coin.ToString() + "/" +
                    characters[characterIndex].WeaponTo2UpgradeCost.ToString();
                CharacterDetaisP2.text = characters[characterIndex].ToString() + " + " +
                    characters[characterIndex].WeaponUp1Power.ToString();
            }
            if (GameStaticValues.player2WeaponLevel == 3)
            {
                WeaponImageInCharacterPanelP2.sprite = characters[characterIndex].WeaponUp2Image;
                CoinNeededP2.text = "MAXED";
                CharacterDetaisP2.text = characters[characterIndex].ToString() + " + " +
                  characters[characterIndex].WeaponUp2Power.ToString();
            }

            tempIndexP2 = characterIndex;
        }
    }

    private void Select(int playerNumber)
    {
        if(playerNumber == 1)
        {
            PlayerPrefs.SetInt("selectedCharacter", tempIndex);
            CharacterImageP1InMainCanvas.sprite = characters[tempIndex].CharacterImage;
            ClosePanel("CharacterPanel");
            ClosePanel("CharactersPanel");
        }
        if(playerNumber == 2)
        {
            PlayerPrefs.SetInt("selectedCharacterP2", tempIndexP2);
            CharacterImageP2InMainCanvas.sprite = characters[tempIndexP2].CharacterImage;
            ClosePanel("CharacterPanelP2");
            ClosePanel("CharactersPanelP2");
        }
    }

    private void UpgradeWeapon(int playerNumber)
    {
        if (playerNumber == 1)
        {
            if(GameStaticValues.player1WeaponLevel == 1 && GameStaticValues.player1Coin >= 500)
            {
                GameStaticValues.player1WeaponLevel = 2;
                GameStaticValues.player1Coin -= 500;
                PlayerPrefs.SetInt("Player1WeaponLevel", 2);
                PlayerPrefs.SetInt("Player1Coin", GameStaticValues.player1Coin);

                WeaponImageInCharacterPanel.sprite = characters[tempIndex].WeaponUp1Image;
                CoinNeeded.text = GameStaticValues.player1Coin.ToString() + "/"
                    + characters[tempIndex].WeaponTo2UpgradeCost.ToString();
                CharacterDetais.text = characters[tempIndex].ToString() + " + " +
                   characters[tempIndex].WeaponUp1Power.ToString();
            }       

            else if(GameStaticValues.player1WeaponLevel == 2 && GameStaticValues.player1Coin >= 5000)
            {
                GameStaticValues.player1WeaponLevel = 3;
                GameStaticValues.player1Coin -= 5000;
                PlayerPrefs.SetInt("Player1WeaponLevel", 3);
                PlayerPrefs.SetInt("Player1Coin", GameStaticValues.player1Coin);

                WeaponImageInCharacterPanel.sprite = characters[tempIndex].WeaponUp2Image;
                CoinNeeded.text = "MAXED";
                CharacterDetais.text = characters[tempIndex].ToString() + " + " +
                   characters[tempIndex].WeaponUp2Power.ToString();
            }

            else if ((GameStaticValues.player1WeaponLevel == 1 && GameStaticValues.player1Coin < 500) ||
                (GameStaticValues.player1WeaponLevel == 2 && GameStaticValues.player1Coin < 5000))
            {
                StartCoroutine(ShowMessage("Not Enough Coin", 2, MessageTextP1));
            }
        }

        if (playerNumber == 2)
        {
            if (GameStaticValues.player2WeaponLevel == 1 && GameStaticValues.player2Coin >= 500)
            {
                GameStaticValues.player2WeaponLevel = 2;
                GameStaticValues.player2Coin -= 500;
                PlayerPrefs.SetInt("Player2WeaponLevel", 2);
                PlayerPrefs.SetInt("Player2Coin", GameStaticValues.player2Coin);

                WeaponImageInCharacterPanelP2.sprite = characters[tempIndex].WeaponUp1Image;
                CoinNeededP2.text = GameStaticValues.player2Coin.ToString() + "/"
                    + characters[tempIndex].WeaponTo2UpgradeCost.ToString();
                CharacterDetaisP2.text = characters[tempIndex].ToString() + " + " +
                   characters[tempIndex].WeaponUp1Power.ToString();
            }

            else if (GameStaticValues.player2WeaponLevel == 2 && GameStaticValues.player2Coin >= 5000)
            {
                GameStaticValues.player2WeaponLevel = 3;
                GameStaticValues.player2Coin -= 5000;
                PlayerPrefs.SetInt("Player2WeaponLevel", 3);
                PlayerPrefs.SetInt("Player2Coin", GameStaticValues.player2Coin);

                WeaponImageInCharacterPanelP2.sprite = characters[tempIndex].WeaponUp2Image;
                CoinNeededP2.text = "MAXED";
                CharacterDetaisP2.text = characters[tempIndex].ToString() + " + " +
                   characters[tempIndex].WeaponUp2Power.ToString();
            }

            else if ((GameStaticValues.player2WeaponLevel == 1 && GameStaticValues.player2Coin < 500) ||
                (GameStaticValues.player2WeaponLevel == 2 && GameStaticValues.player2Coin < 5000))
            {
                StartCoroutine(ShowMessage("Not Enough Coin", 2, MessageTextP2));
            }
        }
    }
}
