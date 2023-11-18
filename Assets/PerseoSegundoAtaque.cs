using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerseoSegundoAtaque : MonoBehaviour
{
    public GameObject itemDrop;
    public GameObject Weapon;
    GameObject parentRef;
    Transform player;
    EnemyStats enemy;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(SpawnDrop());
        GameObject spawnedWeapon = Instantiate(Weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        parentRef =  transform.parent.gameObject;
        enemy = parentRef.GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
        enemy.BuffDamage(.5f);
    }

    // Update is called once per frame
    void Update()
    {
        parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime / 2 );
    }

    IEnumerator SpawnDrop()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(itemDrop, transform.position - new Vector3(3.5f, 3.5f,0), Quaternion.identity);
    }
}
