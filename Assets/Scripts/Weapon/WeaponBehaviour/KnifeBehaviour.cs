using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectedWeaponBehaviour
{
    public Sprite[] spritesRayo;
    public AudioSource soundEffect;
    SpriteRenderer spriteRenderer;
    GameObject target = null;
    bool estaActivoAtaque;
    float angle;
    Vector2 direction;
    
    protected override void Start()
    {
        base.Start();
        soundEffect.Play();
        int spritesRan = Random.Range(0, spritesRayo.Length);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spritesRayo[spritesRan];
        estaActivoAtaque =  true;
    }


    void Update()
    {
        if( target == null)
        {
            EncontrarEnemigoCercano();
            return;
        }
            //Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            //transform.up = direction;
        //Analizar luego su función
        if(estaActivoAtaque)
        {
            StartCoroutine(LanzarAtaque());
            EncontrarEnemigoCercano();
        }
        //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, currentSpeed * Time.deltaTime);
        transform.position += new Vector3(direction.x,direction.y,0) * currentSpeed * Time.deltaTime;
        //gameObject.GetComponent<Rigidbody2D>().AddForce(direction * currentSpeed * Time.deltaTime * 100f);
        //Pasamos el ataque a la CoRutina
        
        //transform.position += direction * currentSpeed * Time.deltaTime; //Movimiento del cuchillo
    }

    IEnumerator LanzarAtaque()
    {
        if(estaActivoAtaque)
        {
            estaActivoAtaque = false;
            direction = target.transform.position - transform.position;
            direction.Normalize();
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -45f;
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle));
            yield return new WaitForSeconds(currentCooldownDuration);
            estaActivoAtaque =  true;
        }
    }

    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    void EncontrarEnemigoCercano()
    {
        float lowerEnemyDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        GameObject[] enemyDistances = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject currentEnemy in enemyDistances)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToEnemy < lowerEnemyDistance)
            {
                lowerEnemyDistance = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
            //Debug.DrawLine(transform.position, closestEnemy.transform.position); (Ver recorrido)
        ChangeTarget(closestEnemy);      
    }
}
