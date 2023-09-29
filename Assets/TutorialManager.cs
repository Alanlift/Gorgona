using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    //Los distintos estados del juego
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        Victory,
        LevelUp,
        BossFight
    }

    public GameState currentState;
    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    public GameObject victoryScreen;
    public GameObject levelUpScreen;
    public GameObject experienceBar;

    //Stats que se mostraran
    public Text currentHealthDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;

    public GameObject spawnAldeano;

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
            case GameState.BossFight:
                CheckForPauseAndResume();
                //UpdateStopWatch();
                break;
            case GameState.Victory:
                if(!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    Debug.Log("Se ganó");
                    DisplayVictory();
                }
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
            experienceBar.SetActive(false);
            pauseScreen.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            experienceBar.SetActive(true);
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
        experienceBar.SetActive(true);
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void GameWin()
    {
        ChangeState(GameState.Victory);
    }

    void DisplayResults()
    {
        experienceBar.SetActive(false);
        resultsScreen.SetActive(true);
    }

    void DisplayVictory()
    {
        experienceBar.SetActive(false);
        victoryScreen.SetActive(true);
    }

    void UpdateStopWatch()
    {
        stopwatchTime += Time.deltaTime;

        UpdateStopWatchDisplay();

        if (stopwatchTime >= timeLimit)
        {
            GameObject[] enemyDistances = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject currentEnemy in enemyDistances)
            {
                currentEnemy.GetComponent<DropRateManager>().drops.Clear();
                Destroy(currentEnemy);
            }
            if(enemyDistances.Length < 1)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                //player.transform.position = new Vector2(12,-2.5f);
                player.transform.position = new Vector2(-12,-2.5f);
                Instantiate(spawnAldeano, new Vector2(12,-2.5f), Quaternion.identity);
                ChangeState(GameState.BossFight);
            }
            
            //GameOver();
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

