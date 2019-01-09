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
    GameObject BansheeCanvas;
    [SerializeField]
    GameObject CharacterPanel;
    [SerializeField]
    GameObject Player2Panel;


    [SerializeField]
    Button BackButtonInCharactersPanel;
    [SerializeField]
    Button BackButtonInCharacterPanel;
    [SerializeField]
    Button CharacterSelectionButton;
    [SerializeField]
    Button SelectButtonInCharacterPanel;
    [SerializeField]
    Button Player1ReadyButton;
    [SerializeField]
    Button Player2ReadyButton;

    [SerializeField]
    List<Button> CharacterButtons = new List<Button>();

    [SerializeField]
    Image CharacterImageInCharacterPanel;
    [SerializeField]
    Text CharacterDetais;

    [SerializeField]
    Image CharacterImageInMainMenu;

    [SerializeField] Toggle MultiplayerToggle;
    [SerializeField] GameObject BotToggles;

    [SerializeField]
    Text NoticeText;

    int tempIndex;
    bool isPlayer1Ready, isPlayer2Ready;
    

    void Start ()
    {
        BansheeCanvas.SetActive(false);
        CharacterPanel.SetActive(false);
        NoticeText.enabled = false;

        for (int i = 0; i < CharacterButtons.Count; i++)
        {
            int index = i;
            CharacterButtons[i].onClick.AddListener( delegate {OpenCharacterPanel(index);} );
        }

        BackButtonInCharacterPanel.onClick.AddListener(delegate { ClosePanel("CharacterPanel"); });
        BackButtonInCharactersPanel.onClick.AddListener(delegate { ClosePanel("CharactersPanel"); });
        CharacterSelectionButton.onClick.AddListener(OpenCharactersPanel);

        Player1ReadyButton.onClick.AddListener(SetPlayer1ReadyForPlay);
        Player2ReadyButton.onClick.AddListener(SetPlayer2ReadyForPlay);

        SelectButtonInCharacterPanel.onClick.AddListener(Select);
        //StartButton.onClick.AddListener(StartGame);

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

        CharacterImageInMainMenu.sprite = characters[PlayerPrefs.GetInt("selectedCharacter")].CharacterImage;
        BotToggles.transform.GetChild((int)GameControl.character).GetComponent<Toggle>().isOn = true;

        MultiplayerToggle.isOn = GameControl.multiplayer;
    }

    private void SetPlayer1ReadyForPlay()
    {
        isPlayer1Ready = true;

        if (isPlayer2Ready)
        {
            StartGame();
        }
        else
        {
            StartCoroutine(ShowMessage("Wait for Player2", 2));
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
            StartCoroutine(ShowMessage("Wait for Player1", 2));
            return;
        }
    }

   IEnumerator ShowMessage(string message, float second)
   {
        NoticeText.enabled = true;
        NoticeText.text = message;
        yield return new WaitForSeconds(second);
        NoticeText.enabled = false;
   }
   
    private void SetBot1(bool value)
    {
        if(value)
        {
            GameControl.character = Character.Character1;
        }
    }
    private void SetBot2(bool value)
    {
        if (value)
        {
            GameControl.character = Character.Character2;
        }
    }

    private void SetBot3(bool value)
    {
        if (value)
        {
            GameControl.character = Character.Character3;
        }
    }
    private void SetBot4(bool value)
    {
        if (value)
        {
            GameControl.character = Character.Character4;
        }
    }

    private void OpenCharactersPanel()
    {
        BansheeCanvas.SetActive(true);
    }

    private void SetMultiplayer(bool value)
    {
        GameControl.multiplayer = value;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void ClosePanel(string panelName)
    {
        if(panelName == "CharacterPanel")
        {
            CharacterPanel.SetActive(false);
        }
        if(panelName == "CharactersPanel")
        {
            BansheeCanvas.SetActive(false);
        }
    }

    private void OpenCharacterPanel(int characterIndex)
    {
        CharacterPanel.SetActive(true);

        CharacterImageInCharacterPanel.sprite = characters[characterIndex].CharacterImage;
        CharacterDetais.text = characters[characterIndex].ToString();

        tempIndex = characterIndex;
    }

    private void Select()
    {
        PlayerPrefs.SetInt("selectedCharacter", tempIndex);

        Debug.Log(PlayerPrefs.GetInt("selectedCharacter"));

        CharacterImageInMainMenu.sprite = characters[tempIndex].CharacterImage;
        ClosePanel("CharacterPanel");
        ClosePanel("CharactersPanel");

    }
}
