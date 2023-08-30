using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    //[SerializeField]
    //GameObject prefab;
    //public GameObject Prefab{get => prefab; private set => prefab = value;}

    //Base stats for enemies
    [SerializeField]
    float health;
    public float Health {get => health; private set => health = value;}
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed{get => moveSpeed; private set => moveSpeed = value;}
    [SerializeField]
    float damage;
    public float Damage{get => damage; private set => damage = value;}
}
