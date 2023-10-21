using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    //Current stats
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    //public float currentDamage;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;
    #region Propiedades de las estadisticas "Current"
    public float CurrentHealth
    {
        get{ return currentHealth;}
        set
        {
            if (currentHealth != value) //Miramos si cambia el valor
            {
                currentHealth = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Vida: " + currentHealth;
                }
                //Actualizamos a tiempo real el valor de la estadistica
            }
        }
    }
    public float CurrentRecovery
    {
        get{ return currentRecovery;}
        set
        {
            if (currentRecovery != value) //Miramos si cambia el valor
            {
                currentRecovery = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recuperación: " + currentRecovery;
                }
                //Actualizamos a tiempo real el valor de la estadistica
            }
        }
    }
    public float CurrentMoveSpeed
    {
        get{ return currentMoveSpeed;}
        set
        {
            if (currentMoveSpeed != value) //Miramos si cambia el valor
            {
                currentMoveSpeed = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Velocidad: " + currentMoveSpeed;
                }
                //Actualizamos a tiempo real el valor de la estadistica
            }
        }
    }
    public float CurrentMight
    {
        get{ return currentMight;}
        set
        {
            if (currentMight != value) //Miramos si cambia el valor
            {
                currentMight = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Poder: " + currentMight;
                }
                //Actualizamos a tiempo real el valor de la estadistica
            }
        }
    }
    public float CurrentProjectileSpeed
    {
        get{ return currentProjectileSpeed;}
        set
        {
            if (currentProjectileSpeed != value) //Miramos si cambia el valor
            {
                currentProjectileSpeed = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Velocidad Proyectil: " + currentProjectileSpeed;
                }
                //Actualizamos a tiempo real el valor de la estadistica
            }
        }
    }
    public float CurrentMagnet
    {
        get{ return currentMagnet;}
        set
        {
            if (currentMagnet != value) //Miramos si cambia el valor
            {
                currentMagnet = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Imán: " + currentMagnet;
                }
                //Actualizamos a tiempo real el valor de la estadistica
            }
        }
    }
    #endregion

    //Experiencia y nivel del jugador
    [Header("Experiencia/Nivel")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public Text levelText;

    public GameObject firstPassiveItemTest, secondPassiveItemTest;

    public GameObject secondWeaponTest;

    void Awake()
    {
        inventory = GetComponent<InventoryManager>();
        //Asignamos las variables
        CurrentHealth = characterData.Health;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentRecovery = characterData.Recovery;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        //Spawneamos el arma o item pasivo
        if(characterData.StartingWeapon != null)
        {
            SpawnWeapon(characterData.StartingWeapon);
        }
        
        //SpawnWeapon(secondWeaponTest);
        //SpawnPassiveItem(firstPassiveItemTest);
        //SpawnPassiveItem(secondPassiveItemTest);
    }

    void Start()
    {
        GameManager.instance.currentHealthDisplay.text = "Vida: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recuperación: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Velocidad: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Poder: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Velocidad Proyectil: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Imán: " + currentMagnet;
        

        UpdateHealthBar();
        UpdateExpBar();
    }

    private void Update()
    {
        //Se hace para que el jugador solo reciba una instancia de daño a la vez
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }//Si el timer de invencibilidad llega a 0 la invencibilidad es falsa
        else if(isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
        UpdateExpBar();
    }

    void LevelUpChecker()
    {
        if(experience >= experienceCap)//Sube de nivel si supera el cap
        {
            //Subida de nivel del jugador y se resetea el contador de experiencia
            level++;
            experience -= experienceCap;
            experienceCap += experienceCapIncrease;
            GameManager.instance.StartLevelUp();
            UpdateLevelText();
        }
    }

    void UpdateExpBar()
    {
        //Actualizamos la barra de exp
        expBar.fillAmount = (float)experience / experienceCap;
    }

    void UpdateLevelText()
    {
        //Actualizamos el nivel en el texto
        levelText.text = "LV " + level.ToString();
    }
    public void TakeDamage(float damage)
    {
        //Si el jugador no es invencible pasa el daño
        if(!isInvincible){
            CurrentHealth -= damage;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if(CurrentHealth<=0)
            {
                Kill();
            }

            UpdateHealthBar();
        }     
    }

    void UpdateHealthBar()
    {
        //Update the health bar
        healthBar.fillAmount = currentHealth /characterData.Health;
    }

    public void Kill()
    {   //Lo hacemos acá por si luego agregamos un revivir
        if(!GameManager.instance.isGameOver)
        {
            GameManager.instance.GameOver();
        }
        //Destroy(gameObject);
    }

    public void RestoreHealth(float amount)
    {
        //Cura si tiene menos vida
        if(CurrentHealth < characterData.Health)
        {
            CurrentHealth += amount;
            //Si se pasa vuelve a su vida original
            if(CurrentHealth > characterData.Health)
            {
                CurrentHealth = characterData.Health;
            }
        }
        
    }

    void Recover()
    {
        if(CurrentHealth < characterData.Health)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            if(CurrentHealth > characterData.Health)
            {
                CurrentHealth = characterData.Health;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if(weaponIndex >= inventory.weaponSlots.Count -1)
        {
            Debug.LogError("El slot del inventario está lleno");
            return;
        }

        //Spawneamos el arma inicial
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>()); //Añadimos el arma al inventory slot

        weaponIndex++; //Se hace despues porque el array aranca de 0
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if(passiveItemIndex >= inventory.passiveItemSlots.Count -1)
        {
            Debug.LogError("El slot del inventario está lleno");
            return;
        }

        //Spawneamos el arma inicial
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>()); //Añadimos el arma al inventory slot

        passiveItemIndex++; //Se hace despues porque el array aranca de 0
    }
}
