using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionarioBehaviour : MonoBehaviour
{
    GameObject parentRef;
    Transform player;
    EnemyStats enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
        enemy.BuffAguante = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime * 1.1f );
    }

}
