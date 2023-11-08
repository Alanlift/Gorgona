using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArieteBehaviour : MonoBehaviour
{
    public AudioSource soundEffect;
    Transform player;
    EnemyStats enemy;
    Transform target = null;
    bool estaActivoAtaqueFase1;
    bool estaActivoAtaqueFase2;
    public float tiempoDeAtaque;
    GameObject parentRef;
    public Vector2 direction;


    void Start()
    {
        parentRef =  transform.parent.gameObject;
        estaActivoAtaqueFase1 = false;
        estaActivoAtaqueFase2 = false;
        enemy = parentRef.GetComponent<EnemyStats>();
        //enemyRB = parentRef.GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!estaActivoAtaqueFase1)
        {
            parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime); //Constantemente sigue al jugador
        }
        else
        {
            StartCoroutine(LanzarNuevoAtaque());
        }
    }
    void changeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void EncontrarJugadorCercano()
    {
        float lowerEnemyDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        GameObject[] enemyDistances = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject currentEnemy in enemyDistances)
        {
            float distanceToEnemy = (currentEnemy.transform.position - parentRef.transform.position).sqrMagnitude;
            if(distanceToEnemy < lowerEnemyDistance)
            {
                lowerEnemyDistance = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
            //Debug.DrawLine(transform.position, closestEnemy.transform.position); (Ver recorrido)
        //changeTarget(closestEnemy);      
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && col.gameObject != null && !estaActivoAtaqueFase1)
        {
            soundEffect.Play();
            estaActivoAtaqueFase1 = true;
            changeTarget(col.gameObject.transform);
        }
        else
        {

        }
    }

    IEnumerator LanzarNuevoAtaque()
    {
        //changeTarget(player);   
            //Debug.Log(estaActivoAtaque); 
        //isWaveActive = true; // set waveStarted to true to prevent the coroutine from starting multiple times
        //Wave para "waveInterval" segundos antes de que inicie la siguiente wave     
            if(estaActivoAtaqueFase1 && !estaActivoAtaqueFase2)
            {
                direction = new Vector2(target.position.x, target.position.y);
                estaActivoAtaqueFase2 = true;
                    //parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, direction, enemy.currentMoveSpeed * 10 * Time.deltaTime);
                transform.up = direction;
                //Vector2 direction = new Vector2(target.transform.position.x - parentRef.transform.position.x, target.transform.position.y - parentRef.transform.position.y); //Es para dirección
                //parentRef.transform.up = direction;
                //parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, target.position, enemy.currentMoveSpeed * 10 * Time.deltaTime);
                
            }
            else if(estaActivoAtaqueFase2)
            {
                yield return new WaitForSeconds(tiempoDeAtaque);
                parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, direction, enemy.currentMoveSpeed * 10 * Time.deltaTime);
                if(parentRef.transform.position.x - direction.x <= 1f && parentRef.transform.position.y - direction.y <= 1f)
                {
                    estaActivoAtaqueFase1 = false;
                    estaActivoAtaqueFase2 = false;
                }
            }
    }
}
