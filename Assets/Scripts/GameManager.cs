using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startScene;
    [SerializeField] private GameObject victoryScene;
    [SerializeField] private GameObject loseScene;
    [SerializeField] private GameObject joystickPanel;
    [SerializeField] private PlayerController playerController;

    private TextMeshProUGUI tmpComponent;

    public event System.Action OnMainMenu;
    public event System.Action OnPlayTurn;

    public static GameManager instance;

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
        OnMainMenu?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    private void OnEnable()
    {
        playerController.OnVictory += VictoryHandler;
        playerController.OnLose += LoseHandler;
        OnMainMenu += MainMenuHandler;
        OnPlayTurn += PlayTurnHandler;
    }

    private void OnDisable()
    {
        playerController.OnVictory -= VictoryHandler;
        playerController.OnLose += LoseHandler;
        OnMainMenu -= MainMenuHandler;
        OnPlayTurn -= PlayTurnHandler;
    }

    private void PlayTurnHandler()
    {
        Debug.Log("playturn");
        joystickPanel.SetActive(true);
        startScene.SetActive(false);
    }

    private void MainMenuHandler()
    {
        Debug.Log("mainmenu");
        startScene.SetActive(true);
    }

    private void VictoryHandler()
    {
        Debug.Log("victory");
        joystickPanel.SetActive(false);
        victoryScene.SetActive(true);

        tmpComponent = victoryScene.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void LoseHandler()
    {
        Debug.Log("lose");
        joystickPanel.SetActive(false);
        loseScene.SetActive(true);
    }

    private void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StartBtn()
    {
        OnPlayTurn?.Invoke();   // Transition to PlayTurn state
    }

    private void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Game Over! Win!");
            tmpComponent.text = "Congratulations! You have completed all the levels.";
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
