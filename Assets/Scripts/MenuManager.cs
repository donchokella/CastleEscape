using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startScene;
    public GameObject joystickPanel;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_On_OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_On_OnGameStateChanged;
    }

    void GameManager_On_OnGameStateChanged(GameManager.GameState state)
    {
        startScene.SetActive(state == GameManager.GameState.MainMenu);
    }
    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartBtn()
    {
        joystickPanel.SetActive(true);
        startScene.SetActive(false);
        gameManager.UpdateGameStates(GameManager.GameState.PlayTurn);
    }
}
