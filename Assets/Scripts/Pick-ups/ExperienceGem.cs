using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : Pickup, ICollectible
{
    public int experienceGranted;
    public void Start()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Collect(Vector2 vector2)
    {
        Perseguida(vector2, rb);
    }

    private void OnDestroy()
    {
        //Collect();
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        perseguir = false;
    }
}
