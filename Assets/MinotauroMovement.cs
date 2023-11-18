using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotauroMovement : MonoBehaviour
{
    public AudioSource soundEffect;
    GameObject parentRef;
    Transform player;
    EnemyStats enemy;

    void Start()
    {
        parentRef =  transform.parent.gameObject;
        enemy = parentRef.GetComponent<EnemyStats>();
        enemy.BuffDamage(1.20f);
        enemy.BuffMoveSpeed(1.1f);
        player = FindObjectOfType<PlayerMovement>().transform;
        soundEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime); //Constantemente sigue al jugador
    }
}
