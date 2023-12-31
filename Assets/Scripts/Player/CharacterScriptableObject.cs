using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]

public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon {get => startingWeapon; private set => startingWeapon = value;}

    [SerializeField]
    float health;
    public float Health {get => health; private set => health = value;}

    [SerializeField]
    float recovery;
    public float Recovery {get => recovery; private set => recovery = value;}

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed {get => moveSpeed; private set => moveSpeed = value;}

    [SerializeField]
    float might;
    public float Might {get => might; private set => might = value;}

    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed {get => projectileSpeed; private set => projectileSpeed = value;}
    [SerializeField]
    float magnet;
    public float Magnet {get => magnet; private set => magnet = value;}
}
