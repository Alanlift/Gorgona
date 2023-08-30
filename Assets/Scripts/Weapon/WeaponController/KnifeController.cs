using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(weaponData.Prefab);
        spawnedKnife.transform.position = transform.position; //Le asigno su posición a la del jugador
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lastMovedVector); //Referenciar y establecer la dirección
    }
    /* Intento de ataque al objetivo
    void OnTriggerStay2D(Collider2D col) {
        if(col.CompareTag("Enemy"))
        {
            base.Attack();
            Debug.Log("Xd");
            GameObject spawnedKnife = Instantiate(weaponData.Prefab);
            spawnedKnife.transform.position = transform.position; //Le asigno su posición a la del jugador
            spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(col.transform.position);

            //Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            //Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            //rb.AddForce(forceDirection * pullSpeed);
        }
    }
    */
}
