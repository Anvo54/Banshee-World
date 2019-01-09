using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour {

    [SerializeField]
    Button GoToMenuSceneButton;
    [SerializeField]
    Button OpenSettingPanelButton;
    [SerializeField]
    Button OpenCreditPanelButton;
    
    [SerializeField]
    GameObject SettingPanel;
    [SerializeField]
    GameObject CreditPanel;

    // Use this for initialization
    void Start ()
    {
        CreditPanel.SetActive(false);
        SettingPanel.SetActive(false);

        GoToMenuSceneButton.onClick.AddListener(GoToMenuScene);
        OpenCreditPanelButton.onClick.AddListener(OpenCreditPanel);
        OpenSettingPanelButton.onClick.AddListener(OpenSettingPanel);
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
        SceneManager.LoadScene("Menu");
    }

}
