using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool perseguir = false;
    public Rigidbody2D rb;
    //Vector2 forceDirection;
    Vector2 player;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            perseguir = false;
            Destroy(gameObject); 
        }
    }
    public void Perseguida(Vector2 vector2)
    {
        //forceDirection = vector2;
        player = vector2;
        perseguir = true;
    }
        private void Update()
    {
        if (perseguir)
        {
            transform.position = Vector2.MoveTowards(transform.position, player, .1f);
            //rb.AddForce(forceDirection * 10f);
        }
    }
}
