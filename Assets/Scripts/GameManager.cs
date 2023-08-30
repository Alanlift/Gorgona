using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //Los distintos estados del juego
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp
    }

    public GameState currentState;
    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    public GameObject levelUpScreen;

    //Stats que se mostraran
    public Text currentHealthDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;

    [Header("Stopwatch")]
    public float timeLimit; //Tiempo limite
    float stopwatchTime; //Tiempo
    public Text stopwatchDisplay;

    //Variable para determinar si la partida termina o no
    public bool isGameOver = false;
    //Variable para determinar si se sube de nivel o no
    public bool choosingUpgrade;

    //Referencia al game object del jugador
    public GameObject playerObject;

    void Awake()
    {   //Chequeo de prevención por si hay otra instancia unica en el juego
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA"+ this +" DELETED");
            Destroy(gameObject);
        }
        DisableScreens();
    }
    void Update()
    {
        //Define el comportamiento de cada estado
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                UpdateStopWatch();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if(!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    Debug.Log("Se perdió");
                    DisplayResults();
                }
                break;
            case GameState.LevelUp:
                if(!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f;
                    Debug.Log("Mejoras");
                    levelUpScreen.SetActive(true);
                }
                CheckForPauseAndResume();
                break;
            default:
                Debug.LogWarning("Este estado no existe");
                break;
        }
    }

    //Cambiamos el estado definido
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if(currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; //Para el juego
            pauseScreen.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    //Definimos la función para pausar o resumir la partida

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    void UpdateStopWatch()
    {
        stopwatchTime += Time.deltaTime;

        UpdateStopWatchDisplay();

        if (stopwatchTime >= timeLimit)
        {
            GameOver();
            //SpawnBoss();
        }
    }

    void UpdateStopWatchDisplay()
    {
        //Calculamos la cantidad de minutos y segundos que pasaron
        int minutes = Mathf.FloorToInt(stopwatchTime/60);
        int seconds = Mathf.FloorToInt(stopwatchTime%60); //Calcula lo que sobra del minuto
        //Updateamos el texto del cronómetro para que se muestre
        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        choosingUpgrade = false;
        Time.timeScale = 1;//Resumimos la partida
        levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
}
