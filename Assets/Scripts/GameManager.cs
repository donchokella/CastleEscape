using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState State; // Current game state

    public GameObject startScene;   // Reference to the start scene GameObject
    public GameObject victoryScene; // Reference to the victory scene GameObject
    public GameObject loseScene;    // Reference to the lose scene GameObject
    public GameObject joystickPanel;    // Reference to the joystick panel GameObject

    // Singleton design pattern
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
        UpdateGameStates(GameState.MainMenu);   // Set the initial game state to MainMenu
    }

    public void UpdateGameStates(GameState newState)
    {
        State = newState;   // Update the current game state
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
        joystickPanel.SetActive(true);  // Activate the joystick panel
        startScene.SetActive(false);    // Deactivate the start scene
    }

    void MainMenuHandler()
    {
        Debug.Log("mainmenu");
        joystickPanel.SetActive(false); // Deactivate the joystick panel
        startScene.SetActive(true);     // Activate the start scene
        victoryScene.SetActive(false);  // Deactivate the victory scene
    }

    void VictoryHandler()
    {
        joystickPanel.SetActive(false); // Deactivate the joystick panel
        startScene.SetActive(false);    // Deactivate the start scene
        victoryScene.SetActive(true);   // Activate the victory scene
    }
    void LoseHandler()
    {
        Debug.Log("lose");
        joystickPanel.SetActive(false); // Deactivate the joystick panel
        startScene.SetActive(false);    // Deactivate the start scene
        victoryScene.SetActive(false);  // Deactivate the victory scene
        loseScene.SetActive(true);      // Activate the lose scene
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // Reload the current scene
    }

    public void StartBtn()
    {
        UpdateGameStates(GameManager.GameState.PlayTurn);   // Transition to PlayTurn state
    }
}
