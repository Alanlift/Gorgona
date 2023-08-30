using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProjectedWeaponBehavior : MonoBehaviour //Comportamiento de todas las armas de proyectiles
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;
    
    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir) //Para cambiar las rotaciones y las escalas(que se flipee)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if(dirx < 0 && diry == 0) //left
        {
            scale.x *= -1;
            scale.y *= -1;
        } else if(dirx == 0 && diry > 0) //up
        {
            scale.x *= -1;
        } else if(dirx == 0 && diry < 0) //down
        {
            scale.y *= -1;
        } else if(dirx > 0 && diry > 0) //rightup
        {
            rotation.z = 0f;
        } else if(dirx > 0 && diry < 0) //rightdown
        {
            rotation.z = -90f;
        }else if(dirx < 0 && diry > 0) //leftup
        {
            rotation.z = 90f;
        } else if(dirx < 0 && diry < 0) //leftdown
        {
            rotation.z = 180f;
        }
        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation); //
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        //Se referencia al tag "Enemy"(enemigo) para luego obtener sus datos y que tome daño
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage()); //Se usa current damage y no weaponData.damage por si luego se incluyen modificadores
            ReducePierce();//Se rompe según su penetración
        }
        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
