using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoplitaBehaviour : MonoBehaviour
{
    public GameObject Weapon;
    Transform player;
    EnemyStats enemy;
    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(Weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime / 2 );
    }
}
