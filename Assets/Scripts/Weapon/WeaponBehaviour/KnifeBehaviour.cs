using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectedWeaponBehaviour
{
    public Sprite[] spritesRayo;
    SpriteRenderer spriteRenderer;
    GameObject target = null;
    
    protected override void Start()
    {
        base.Start();
        int spritesRan = Random.Range(0, spritesRayo.Length);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spritesRayo[spritesRan];
    }


    void Update()
    {
        EncontrarEnemigoCercano();
        if( target == null)
        {
            return;
        }
            //Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            //transform.up = direction;
        //Analizar luego su función
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - target.transform.position), 200 * Time.deltaTime);
        //DirectionChecker(target);
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, currentSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle));
        
        //transform.position += direction * currentSpeed * Time.deltaTime; //Movimiento del cuchillo
    }

    public void changeTarget(GameObject newTarget)
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
        changeTarget(closestEnemy);      
    }
}
