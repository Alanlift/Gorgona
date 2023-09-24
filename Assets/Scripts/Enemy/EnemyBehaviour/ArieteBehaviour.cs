using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArieteBehaviour : MonoBehaviour
{
    Transform player;
    EnemyStats enemy;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime); //Constantemente sigue al jugador
    }
}
