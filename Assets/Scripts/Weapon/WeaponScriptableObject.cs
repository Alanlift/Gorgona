using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab{get => prefab; private set => prefab = value;}
    //Base stats for weapons
    [SerializeField]
    float damage;
    public float Damage{get => damage; private set => damage = value;}

    [SerializeField]
    float speed;
    public float Speed{get => speed; private set => speed = value;}

    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration{get => cooldownDuration; private set => cooldownDuration = value;}
    
    [SerializeField]
    int pierce;
    public int Pierce{get => pierce; private set => pierce = value;}

    [SerializeField]
    int level; //Para modificar en el editor
    public int Level{get => level; private set => level = value;}

    [SerializeField]
    GameObject nextLevelPrefab; //Lo que se convierte el objeto cuando levelea
                        //No tiene que ver con el prefab spawneado al incio del nivel
    public GameObject NextLevelPrefab{get => nextLevelPrefab; private set => nextLevelPrefab = value;}

    [SerializeField]
    new string name;
    public string Name {get => name; set => name = value;}
    [SerializeField]
    string description;
    public string Description {get => description; set => description = value;}

    [SerializeField]
    Sprite icon;    //Se modifica en el editor
    public Sprite Icon {get => icon; set => icon = value;}
}
