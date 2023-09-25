using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampeonBehaviour : MonoBehaviour
{
    Transform player;
    EnemyStats enemy;
    Transform target = null;
    float gradoRotacion;
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
            gradoRotacion++;
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
            estaActivoAtaqueFase1 = true;
        }
    
    }
    

    IEnumerator LanzarNuevoAtaque()
    {
            if(estaActivoAtaqueFase1 && !estaActivoAtaqueFase2)
            {
                gradoRotacion++;
                parentRef.transform.rotation = Quaternion.Euler(Vector3.forward * gradoRotacion);
                parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
                transform.up = direction;
                //Vector2 direction = new Vector2(target.transform.position.x - parentRef.transform.position.x, target.transform.position.y - parentRef.transform.position.y); //Es para dirección
                //parentRef.transform.up = direction;
                //parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, target.position, enemy.currentMoveSpeed * 10 * Time.deltaTime);
                yield return new WaitForSeconds(tiempoDeAtaque);

                estaActivoAtaqueFase2 = true;
            }
            else if(estaActivoAtaqueFase2)
            {
                gradoRotacion = 1;
                parentRef.transform.rotation = Quaternion.Euler(Vector3.forward * gradoRotacion);
                yield return new WaitForSeconds(tiempoDeAtaque + 1f);
                estaActivoAtaqueFase1 = false;
                estaActivoAtaqueFase2 = false;
            }
    }
}
