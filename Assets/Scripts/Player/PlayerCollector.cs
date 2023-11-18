using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector; //El imán
    public float pullSpeed;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.CurrentMagnet;
    }

    //Para chequear si el jugador chocó con un objeto que tenga la interfaz ICollectible(Osea, que se puede agarrar)
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //Obtenemos el componente rigidbody2d
            //Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            //Un vector que apunta desde el item al jugador
                //Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            //Y se llama a la función para que lo agarre
                //rb.AddForce(forceDirection * pullSpeed); //Nos traemos el objeto por el poder de la fuerza del Jedi
                //collectible.Collect(forceDirection);
                collectible.Collect(transform.position);
        }
    }

}
