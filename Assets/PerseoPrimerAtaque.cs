using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerseoPrimerAtaque : MonoBehaviour
{
    Transform player;
    Vector2 refVolver;
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
        enemy.BuffDamage(2f);
    }


    // Update is called once per frame
    void Update()
    {
        if(!estaActivoAtaqueFase1 && !estaActivoAtaqueFase2)
        {
            parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime); //Constantemente sigue al jugador
        }
        else if(estaActivoAtaqueFase1 && !enemy.hizoDaño)
        {
            parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, player.transform.position, enemy.currentMoveSpeed * 2 * Time.deltaTime);
        }
        else if(enemy.hizoDaño && estaActivoAtaqueFase1)
        {
            estaActivoAtaqueFase2 = true;
            estaActivoAtaqueFase1 = false;
        }
        else if(estaActivoAtaqueFase2 && Vector2.Distance(parentRef.transform.position, refVolver) >1f)
        {
            parentRef.transform.localScale = new Vector3((parentRef.transform.position.x > player.transform.position.x) ? parentRef.transform.localScale.y * -1 : parentRef.transform.localScale.y, parentRef.transform.localScale.y, parentRef.transform.localScale.z);
            parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, refVolver, enemy.currentMoveSpeed * Time.deltaTime);
        }
        else
        {
            enemy.hizoDaño = false;
            estaActivoAtaqueFase1 = false;
            estaActivoAtaqueFase2 = false;
        }
        
    }

    void changeTarget(Transform newTarget)
    {
        target = newTarget;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && col.gameObject != null && !estaActivoAtaqueFase1)
        {
            refVolver = parentRef.transform.position;
            estaActivoAtaqueFase1 = true;
            changeTarget(col.gameObject.transform);
        }
        else
        {

        }
    }
}
