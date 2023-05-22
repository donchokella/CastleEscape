using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

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
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
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
    }

    void MainMenuHandler()
    {
        Debug.Log("mainmenu");
    }
}
