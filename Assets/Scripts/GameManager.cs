using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState State;


    public GameObject startScene;
    public GameObject victoryScene;
    public GameObject loseScene;
    public GameObject joystickPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateGameStates(GameState.MainMenu);
    }

    public void UpdateGameStates(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                MainMenuHandler();
                break;
            case GameState.PlayTurn:
                PlayTurnHandler();
                break;
            case GameState.Victory:
                VictoryHandler();
                break;
            case GameState.Lose:
                LoseHandler();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
    public enum GameState
    {
        MainMenu,
        PlayTurn,
        Victory,
        Lose
    }

    void PlayTurnHandler()
    {
        Debug.Log("playturn");
        joystickPanel.SetActive(true);
        startScene.SetActive(false);
    }

    void MainMenuHandler()
    {
        Debug.Log("mainmenu");
        joystickPanel.SetActive(false);
        startScene.SetActive(true);
        victoryScene.SetActive(false);
    }

    void VictoryHandler()
    {
        joystickPanel.SetActive(false);
        startScene.SetActive(false);
        victoryScene.SetActive(true);
    }
    void LoseHandler()
    {
        Debug.Log("lose");
        joystickPanel.SetActive(false);
        startScene.SetActive(false);
        victoryScene.SetActive(false);
        loseScene.SetActive(true);
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartBtn()
    {
        UpdateGameStates(GameManager.GameState.PlayTurn);
    }
}
