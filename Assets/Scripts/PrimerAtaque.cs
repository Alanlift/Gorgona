using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimerAtaque : MonoBehaviour
{
    public GameObject ArmaTutorial;
    public GameObject spawnerComienza;
    InventoryManager inventory;
    public int weaponIndex;
    bool GoodbyeObject = false;
    public Text refOraculo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GoodbyeObject)
        {
            refOraculo.gameObject.GetComponent<OraculoHabla>().tieneArma = true;
            spawnerComienza.SetActive(true);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if(player.CompareTag("Player") && !GoodbyeObject)
        {
            GoodbyeObject = true;
            player.gameObject.GetComponent<PlayerStats>().SpawnWeapon(ArmaTutorial);
            /*
            GameObject spawnedWeapon = Instantiate(ArmaTutorial, player.gameObject.transform.position, Quaternion.identity);
            spawnedWeapon.transform.SetParent(transform);
            inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());
            */
        }
        
    }
}
