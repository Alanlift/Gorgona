using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickup, ICollectible
{
    public int healthToRestore;

    public void Start()
    {
        //Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Collect(Vector2 vector2)
    {
        Perseguida(vector2);
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
    }

}
