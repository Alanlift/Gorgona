using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //Una lista de los grupos de enemigos que spawnean en esta wave(ola)
        public int waveQuota; //La cantidad de enemigos spawnean en la wave
        public float spawnInterval; //El intervalo al que se spawnean enemigos(tiempo)
        public int spawnCount; //El numero de enemigos que hayan spawneado en la wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; //Cantidad de enemigos que spawnean
        public int spawnCount; //El numero de enemigos que hayan spawneado en la wave
        public GameObject enemyPrefab;
    }
    public List<Wave> waves;    //Una lista con todas las waves en el juego
    public int currentWaveCount; //El index de la wave (Las listas arrancan de 0)
    int currentWaveQuota = 0;

    [Header("Spawner Attributes")]
    float spawnTimer; //Timer usado para determinar cuando spawnear el siguiente enemigo
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval; //El intervalo entre cada wave

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; //Lista de puntos de spawns de los enemigos
    Transform player;
    private bool isWaveActive = false;
    [HideInInspector]
    public List<GameObject> buffedLobos;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
        buffedLobos = new List<GameObject>();
    }


    void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive)
        {
        StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        if(spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    void CalculateWaveQuota()
    {
        currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        //Debug.LogWarning("Cantidad de enemigos " + currentWaveQuota);
    }

    IEnumerator BeginNextWave()
    {
        isWaveActive = true; // set waveStarted to true to prevent the coroutine from starting multiple times
        //Wave para "waveInterval" segundos antes de que inicie la siguiente wave
        yield return new WaitForSeconds(waveInterval);
        {
            //Si hay más waves que vayan a iniciar despues de la wave actual, nos movemos a la siguiente wave
            if(currentWaveCount < waves.Count -1)
            {
                maxEnemiesAllowed *= 2;
                currentWaveCount++;
                CalculateWaveQuota();
                isWaveActive = false; // reset isWaveActive to false so that the next wave can be started
            }
        }
    }
    void SpawnEnemies()
    {
        //Chequea si el minimo numero de enemigos en la wave fueron spawneados
        if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //Spawnea cada tipo de enemigo hasta que el cupo esté lleno
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //Chequea si el número minimo de enemigos de este tipo ya fue spawneado
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    //Numero limite de enemigos que pueden spawnear al mismo tiempo
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    //Spawnea el enemigo en una posición random fuera de la cámara del jugador
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                    if(enemyGroup.enemyPrefab.name == "ManadaLobo" || enemyGroup.enemyPrefab.name == "ManadaPerro")
                    {
                        buffedLobos.Add(enemyGroup.enemyPrefab.gameObject.transform.GetChild(0).gameObject);
                        buffedLobos.Add(enemyGroup.enemyPrefab.gameObject.transform.GetChild(1).gameObject);
                    }
                }
            }
        }
        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    //Llamamos esta función cuando se muere un enemigo
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }

}
