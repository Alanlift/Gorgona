using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerseoTercerAtaque : MonoBehaviour
{
    public GameObject itemDrop;
    GameObject parentRef;
    Transform player;
    EnemyStats enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDrop());
        parentRef =  transform.parent.gameObject;
        enemy = parentRef.GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
        enemy.BuffAguante = true;
    }

    // Update is called once per frame
    void Update()
    {
        parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime * 1.1f );
    }

    IEnumerator SpawnDrop()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(itemDrop, transform.position + new Vector3(2.5f,-3.5f,0), Quaternion.identity);
    }
}
