using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotauroBehaviour : MonoBehaviour
{
    EnemyStats enemy;
    public GameObject Ataque1;
    public GameObject Ataque2;
    GameObject parentRef;
    float maxHealth;
    float currentHealth;
    void Start()
    {
        parentRef =  transform.parent.gameObject;
        enemy = parentRef.GetComponent<EnemyStats>();
        maxHealth = parentRef.GetComponent<EnemyStats>().currentHealth;
        //enemyRB = parentRef.GetComponent<Rigidbody2D>();
        //player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = parentRef.GetComponent<EnemyStats>().currentHealth;
        if(currentHealth>=(maxHealth*0.30))
        {
            Ataque1.SetActive(true);
            Ataque2.SetActive(false);
        }
        else if(currentHealth<(maxHealth*0.30))
        {
            Ataque2.SetActive(true);
            Ataque1.SetActive(false);
        }
        else
        {
        }
    }

    private void OnDestroy() {
        Debug.Log("Muertito");
        GameManager.instance.GameWin();
    }
}
