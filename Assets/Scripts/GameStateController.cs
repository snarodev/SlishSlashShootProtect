using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public GameState currentGameState = GameState.PreGame;

    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject dialogMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }
    public void GameOver()
    {
        currentGameState = GameState.GameOver;
        gameOverMenu.SetActive(true);
    }

    public void StartDialog()
    {
        mainMenu.SetActive(false);
        dialogMenu.SetActive(true);
        currentGameState = GameState.Dialog;
    }

    public void StartGame()
    {
        currentGameState = GameState.InGame;
        mainMenu.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public enum GameState
    {
        PreGame,
        Dialog,
        InGame,
        GameOver
    }
}
