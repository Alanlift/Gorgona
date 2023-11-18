using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PerseoBehaviour : MonoBehaviour
{
    Animator bossAM;
    EnemyStats enemy;
    public GameObject Ataque1;
    public GameObject Ataque2;
    public GameObject Ataque3;
    GameObject parentRef;
    float maxHealth;
    float currentHealth;
    Image healthBar;
    GameManager gameManager;
    void Start()
    {
        parentRef =  transform.parent.gameObject;
        enemy = parentRef.GetComponent<EnemyStats>();
        maxHealth = parentRef.GetComponent<EnemyStats>().currentHealth;
        currentHealth = parentRef.GetComponent<EnemyStats>().currentHealth;
        gameManager = FindObjectOfType<GameManager>();
        bossAM = parentRef.GetComponent<Animator>();
        
        //enemyRB = parentRef.GetComponent<Rigidbody2D>();
        //player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = parentRef.GetComponent<EnemyStats>().currentHealth;
        gameManager.currentHealthBoss = currentHealth;
        //UpdateHealthBar();
        if(currentHealth>(enemy.enemyData.Health*0.75))
        {
            bossAM.SetBool("Normal", true);
            Ataque1.SetActive(true);
            Ataque2.SetActive(false);
            Ataque3.SetActive(false);
        }
        else if(currentHealth<=(enemy.enemyData.Health*0.75) && currentHealth>(enemy.enemyData.Health*0.50))
        {
            bossAM.SetBool("Disparo", true);
            bossAM.SetBool("Normal", false);
            Ataque1.SetActive(false);
            Ataque2.SetActive(true);
            Ataque3.SetActive(false);
        }
        else if(currentHealth<=(enemy.enemyData.Health*0.25) && currentHealth>(enemy.enemyData.Health*0))
        {
            bossAM.SetBool("Escudo", true);
            bossAM.SetBool("Disparo", false);
            Ataque1.SetActive(false);
            Ataque2.SetActive(false);
            Ataque3.SetActive(true);
        }
        else if(currentHealth<=(enemy.enemyData.Health*0))
        {
            enemy.BuffDamage(0);
            bossAM.SetBool("Escudo", false);
            bossAM.SetBool("Death", true);
            Ataque3.SetActive(false);
        }
        else
        {
            Ataque2.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        Ataque3.SetActive(false);
        bossAM.SetBool("Escudo", false);
        bossAM.SetBool("Death", true);
        GameManager.instance.GameWin();
    }
}
