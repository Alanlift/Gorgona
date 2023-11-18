using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorIZQ : MonoBehaviour
{
    Transform player;
    Animator am;
    SpriteRenderer sr;
    InventoryManager invman;
    bool refObj;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>().transform;
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {                                       //Va menor si el sprite del personaje mira hacia la izquierda
        transform.localScale = new Vector3((transform.position.x < player.transform.position.x) ? transform.localScale.y * -1 : transform.localScale.y, transform.localScale.y, transform.localScale.z);
    }
}
