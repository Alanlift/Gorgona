using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoboAtaqueBehaviour : MonoBehaviour
{
    EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnDestroy() {
        enemySpawner.buffedLobos.RemoveAt(0);
        /*enemySpawner.buffedLobos.Remove(gameObject); :Placeholder)
        Debug.Log("Muertito");
        if(enemySpawner.buffedLobos.Contains(gameObject))
        {
            Debug.Log("Si está :v");
            enemySpawner.buffedLobos.RemoveAt(0);
        }
        */
    }
}
