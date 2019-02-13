using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField]
    Button GoToMenuSceneButton;
    [SerializeField]
    Button OpenSettingPanelButton;
    [SerializeField]
    Button OpenCreditPanelButton;
    [SerializeField]
    Toggle SoundToggle;
    
    [SerializeField]
    GameObject SettingPanel;
    [SerializeField]
    GameObject CreditPanel;

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
        CreditPanel.SetActive(false);
        SettingPanel.SetActive(false);

        GoToMenuSceneButton.onClick.AddListener(GoToMenuScene);
        OpenCreditPanelButton.onClick.AddListener(OpenCreditPanel);
        OpenSettingPanelButton.onClick.AddListener(OpenSettingPanel);

        SoundToggle.onValueChanged.AddListener((value) => { SetSoundOnOff(!value); });
	}

    private void SetSoundOnOff(bool value)
    {
        AudioListener.pause = value;
        //Debug.Log(AudioListener.pause);
    }

    public void ClosePopupPanels()
    {
        CreditPanel.SetActive(false);
        SettingPanel.SetActive(false);
    }
        
        
    private void OpenSettingPanel()
    {
        SettingPanel.SetActive(true);
        CreditPanel.SetActive(false);
    }

    private void OpenCreditPanel()
    {
        SettingPanel.SetActive(false);
        CreditPanel.SetActive(true);
    }

    private void GoToMenuScene()
    {
        SceneManager.LoadScene(1);
    }

}
