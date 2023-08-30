using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;

    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {
        //Para aplicar el modificador al stat apropiado
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }
}
