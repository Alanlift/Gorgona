using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{
        //List<Transform> enemyDistances;
    public GameObject instance;
    Transform newEnemySight;
    public float lowerDistance;
    float enemyDistance;
    GameObject passTarget;
    public bool estaActivoAtaque;
    
    protected override void Start()
    {
        estaActivoAtaque = false;
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(weaponData.Prefab);
        spawnedKnife.transform.position = transform.position; //Le asigno su posición a la del jugador
        //spawnedKnife.GetComponent<KnifeBehaviour>().changeTarget(passTarget); //Referenciar y establecer la dirección
        spawnedKnife.GetComponent<KnifeBehaviour>();
    }
    //Intento de ataque al objetivo
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Enemy") && col.gameObject != null && !estaActivoAtaque)
        {
            StartCoroutine(LanzarNuevoAtaque());
            //}
            
            /*
            base.Attack();
            Debug.Log("Xd");
            GameObject spawnedKnife = Instantiate(weaponData.Prefab);
            spawnedKnife.transform.position = transform.position; //Le asigno su posición a la del jugador
            spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(col.transform.position);

            //Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            //Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            //rb.AddForce(forceDirection * pullSpeed);
            */
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        /*if(col.CompareTag("Enemy") && enemyDistances.Contains(col.gameObject.transform))
        {
            enemyDistances.Remove(col.gameObject.transform);
        }
        */
    }

    IEnumerator LanzarNuevoAtaque()
    {
        //yield return new WaitForSeconds(currentCooldown);
        estaActivoAtaque = true;
            //Debug.Log(estaActivoAtaque); 
        //isWaveActive = true; // set waveStarted to true to prevent the coroutine from starting multiple times
        //Wave para "waveInterval" segundos antes de que inicie la siguiente wave
        yield return new WaitForSeconds(currentCooldown);
            if(estaActivoAtaque)
            {
                //Debug.Log(currentCooldown);   
                    //Attack();
                estaActivoAtaque = false;
                    //Debug.Log(estaActivoAtaque); 
            }
            
            //Si hay más waves que vayan a iniciar despues de la wave actual, nos movemos a la siguiente wave
            /*
            if(currentWaveCount < waves.Count -1)
            {
                currentWaveCount++;
                CalculateWaveQuota();
                isWaveActive = false; // reset isWaveActive to false so that the next wave can be started
            }
            */
    }

    public bool returnestaActivoAtaque()
    {
        return estaActivoAtaque;
    }
    
}
