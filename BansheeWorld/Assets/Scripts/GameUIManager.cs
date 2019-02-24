using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;

    [SerializeField] Button pauseButton;
    [SerializeField] Button playAgainButton;
    [SerializeField] Button cancelButton;

    [SerializeField] Button continueButton;
    [SerializeField] Button goToMenuButton;
    [SerializeField] Button quitButton;

    GameSceneManager gameSceneManager;

    void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);

        gameSceneManager = GetComponent<GameSceneManager>();

        playAgainButton.onClick.AddListener(gameSceneManager.PlayAgain);
        cancelButton.onClick.AddListener(GoToMenuScene);

        pauseButton.onClick.AddListener(Pause);
        continueButton.onClick.AddListener(Unpause);
        goToMenuButton.onClick.AddListener(GoToMenuScene);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void GoToMenuScene()
    {
        if(gameOverPanel.activeSelf)
            gameOverPanel.SetActive(false);
        if(pausePanel.activeSelf)
            pausePanel.SetActive(false);

        SceneManager.LoadScene("Menu2");  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameSceneManager.isGameOver)
        {
            gameOverPanel.SetActive(true);

            gameSceneManager.isGameOver = false;
        }
    }
}
