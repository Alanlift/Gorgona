using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;
    WeaponController invman;
    bool refObj = true;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //refObj = GetComponent<PlayerStats>().characterData.StartingWeapon.GetComponent<KnifeController>().estaActivoAtaque;
        if(GetComponent<InventoryManager>().weaponSlots[0] != null)
        {
            refObj = GetComponent<InventoryManager>().weaponSlots[0].GetComponent<KnifeController>().estaActivoAtaque;
        }
        //refObj = GetComponent<InventoryManager>().weaponSlots[0].GetComponent<KnifeController>().estaActivoAtaque;
            //Debug.Log(refObj);
        if(!refObj) //No deberia ser así, deberia ser true move y false Attack, pero bueno así queda mientras xd
        {
            am.SetBool("Move", false);
            am.SetBool("Attack", true);
            return;
        }
        else if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
        {
            am.SetBool("Move", true);
            am.SetBool("Attack", false);
            SpriteDirectionChecker();
        }
        /*
        else if(refObj && (pm.moveDir.x != 0 || pm.moveDir.y != 0))
        {
            Debug.Log(refObj);
            am.SetBool("Move", false);
            am.SetBool("Attack", false);
            return;
        }
        */
        else
        {
            am.SetBool("Move", false);
        }
    }

    void SpriteDirectionChecker()
    {
        if (pm.lastHorizontalVector < 0) 
        {
            sr.flipX = true;
        } else
        {
            sr.flipX = false;
        }
    }
}
