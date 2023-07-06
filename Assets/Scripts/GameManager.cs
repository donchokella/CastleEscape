using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject startScene;   // Reference to the start scene GameObject
    public GameObject victoryScene; // Reference to the victory scene GameObject
    public GameObject loseScene;    // Reference to the lose scene GameObject
    public GameObject joystickPanel;    // Reference to the joystick panel GameObject

    private TextMeshProUGUI tmpComponent;

    public PlayerController playerController;
    public event System.Action OnMainMenu;
    public event System.Action OnPlayTurn;

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

    void PlayTurnHandler()
    {
        Debug.Log("playturn");
        joystickPanel.SetActive(true);  // Activate the joystick panel
        startScene.SetActive(false);    // Deactivate the start scene
    }

    void MainMenuHandler()
    {
        Debug.Log("mainmenu");
        startScene.SetActive(true);     // Activate the start scene
    }

    void VictoryHandler()
    {
        Debug.Log("victory");
        joystickPanel.SetActive(false); // Deactivate the joystick panel
        victoryScene.SetActive(true);   // Activate the victory scene

        tmpComponent = victoryScene.GetComponentInChildren<TextMeshProUGUI>();
    }
    void LoseHandler()
    {
        Debug.Log("lose");
        joystickPanel.SetActive(false); // Deactivate the joystick panel
        loseScene.SetActive(true);      // Activate the lose scene
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // Reload the current scene
    }

    public void StartBtn()
    {
        OnPlayTurn?.Invoke();   // Transition to PlayTurn state
    }

    public void NextScene()
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
