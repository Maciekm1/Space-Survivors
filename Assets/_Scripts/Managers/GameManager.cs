using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Singleton
    public static GameManager Instance;
    //References
    private UIManager uiManager;
    private TutorialManager tutorialManager;
    private EnemySpawner spawner;
    [SerializeField] PlayerController playerController;
    //Events
    public Action OnGameEnd;

    private float gameTime;
    public bool GameOn { get; private set; }

    private void Awake()
    {
        Instance = this;
        uiManager = GetComponent<UIManager>();
        tutorialManager = GetComponent<TutorialManager>();
        spawner = GetComponent<EnemySpawner>();
        GameOn = false;
    }

    private void OnEnable()
    {
        tutorialManager.OnTutorialFinish += StartGame;
        playerController.PlayerHealth.OnLoseAllHealth += EndGame;
    }

    private void OnDisable()
    {
        tutorialManager.OnTutorialFinish -= StartGame;
        playerController.PlayerHealth.OnLoseAllHealth -= EndGame;
    }

    public void StartGame()
    {
        gameTime = 0;
        uiManager.HideEndGameScreen();

        GameOn = true;
        spawner.StartSpawning();
        playerController.ResetPlayer();
    }

    public void EndGame()
    {
        uiManager.ShowEndGameScreen();

        GameOn = false;
        spawner.StopSpawning();
        OnGameEnd?.Invoke();
    }

    public void ResumeGame()
    {
        GameOn = true;
        spawner.StartSpawning();
    }

    public void PauseGame()
    {
        GameOn = false;
        spawner.StopSpawning();
    }

    private void Update()
    {
        if(GameOn)
        {
            gameTime += Time.deltaTime;
            uiManager.UpdateGameTime(gameTime);
        }
    }
}
