using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraBehaviour : MeleeWeaponBehaviour
{
    public AudioSource soundEffect;
    List<GameObject> markedEnemies;
    protected override void Start()
    {
        
        base.Start();
        soundEffect.Play();
        markedEnemies = new List<GameObject>();
    }


    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject)){
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
            markedEnemies.Add(col.gameObject);//Marcamos al enemigo para que no se haga el daño otra vez
        }
        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable) && !markedEnemies.Contains(col.gameObject))
            {
                breakable.TakeDamage(GetCurrentDamage());

                markedEnemies.Add(col.gameObject); //Marcamos al objeto para que no se haga el daño otra vez
            }
        }
    }
}
