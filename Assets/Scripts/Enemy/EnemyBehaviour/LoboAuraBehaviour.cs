using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoboAuraBehaviour : MonoBehaviour
{
    public int buff;
    SpriteRenderer spriteRenderer;
    public Sprite[] spritesLobo;
    EnemyStats enemy;
    Transform player;
    GameObject parentRef;
    bool noHayLoboCerca;
    List<GameObject> buffedLobos;
    EnemySpawner enemySpawner;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        noHayLoboCerca = true;
        parentRef =  transform.parent.gameObject;
        enemy = parentRef.GetComponent<EnemyStats>();
        spriteRenderer = parentRef.gameObject.GetComponent<SpriteRenderer>();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    void Update()
    {
        buffedLobos = new List<GameObject>(enemySpawner.buffedLobos);
        //referenceObject = GameObject.Find("LoboAtaque");
        if(noHayLoboCerca || buffedLobos.Count == 0)
        {
            spriteRenderer.sprite = spritesLobo[0];
            parentRef.transform.position = Vector2.MoveTowards(parentRef.transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            foreach(GameObject lobo in buffedLobos)
            {
                lobo.GetComponent<SpriteRenderer>().sprite = spritesLobo[1];
                lobo.GetComponent<EnemyStats>().BuffDamage(buff);
                lobo.GetComponent<EnemyStats>().BuffMoveSpeed(buff);
            }
            spriteRenderer.sprite = spritesLobo[2];
            noHayLoboCerca = false;
            /*
            referenceObject.gameObject.GetComponent<EnemyStats>().BuffDamage(buff);
            referenceObject.gameObject.GetComponent<EnemyStats>().BuffMoveSpeed(buff);
            */
            //noHayLoboCerca = false;
        }
    }

}
