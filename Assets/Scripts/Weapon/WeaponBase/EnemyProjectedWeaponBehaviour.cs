using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyProjectedWeaponBehaviour : MonoBehaviour //Comportamiento de todas las armas de proyectiles
{
    public WeaponScriptableObject weaponData;

    //protected GameObject direction;
    public float destroyAfterSeconds;
    public bool isDestroyed;

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
        isDestroyed = false;
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(GameObject dir) //Para cambiar las rotaciones y las escalas(que se flipee)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - dir.transform.position), 200 * Time.deltaTime);

        /*float dirx = direction.x;
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
        */
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        //Se referencia al tag "Enemy"(enemigo) para luego obtener sus datos y que tome daño
        if(col.CompareTag("Player"))
        {
            PlayerStats player = col.GetComponent<PlayerStats>();
            player.TakeDamage(GetCurrentDamage()); //Se usa current damage y no weaponData.damage por si luego se incluyen modificadores
            ReducePierce();//Se rompe según su penetración
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

    void OnDestroy()
    {
        isDestroyed = true;
    }
}
