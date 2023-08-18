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

    private float gameTime;
    private bool gameStarted = false;

    private void Awake()
    {
        Instance = this;
        uiManager = GetComponent<UIManager>();
        tutorialManager = GetComponent<TutorialManager>();
        spawner = GetComponent<EnemySpawner>();
    }

    private void OnEnable()
    {
        tutorialManager.OnTutorialFinish += StartGame;
    }

    private void OnDisable()
    {
        tutorialManager.OnTutorialFinish -= StartGame;
    }

    public void StartGame()
    {
        gameStarted = true;
        spawner.StartSpawning();
    }

    private void Update()
    {
        if(gameStarted)
        {
            gameTime += Time.deltaTime;
            uiManager.UpdateGameTime(gameTime);
        }
    }
}
