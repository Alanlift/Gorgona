using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : WeaponController //Es melee
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedMelee = Instantiate(weaponData.Prefab);
        spawnedMelee.transform.position = transform.position; //Para que su posición sea la del jugador
        spawnedMelee.transform.parent = transform; //Para que aparezca con su "padre"
    }
    
}
