using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //Current stats
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentDamage;
    
    public float despawnDistance = 40f;
    Transform player;

    SpriteRenderer spriteRenderer;
    Color originalColor;

    public ParticleSystem deathEffect;
    public bool muerteAnimada;
    public float segMuerte;

    public AudioSource deathSoundEffect;
    [HideInInspector]
    public bool hizoDaño;
    [HideInInspector]
    public bool BuffAguante;
    void Awake()
    {
        //Sprite y Color
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        currentHealth = enemyData.Health;
        currentMoveSpeed = enemyData.MoveSpeed;
        currentDamage = enemyData.Damage;
        hizoDaño = false;
        BuffAguante = false;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(DamageFlash());
        if(BuffAguante)
        {
            currentHealth -= damage/2;
        }
        else
        {
            currentHealth -= damage;
        }
        if(currentHealth<=0)
        {
            if(muerteAnimada)
            {
                StartCoroutine(AnimMuerte());
            }
            else
            {
            Kill();
            }
        }
    }
    IEnumerator DamageFlash()
    {
        spriteRenderer.color = new Color(0, 170, 228, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    public void BuffDamage(float damage)
    {
        currentDamage *= damage;
    }
    public void BuffMoveSpeed(float speed)
    {
        currentMoveSpeed *= speed;
    }

    IEnumerator AnimMuerte()
    {
        yield return new WaitForSeconds(segMuerte);
        Kill();
    }
    IEnumerator SonidoMuerte()
    {
        yield return new WaitForSeconds(.20f);
        Destroy(gameObject);
    }
    public void Kill()
    {
        deathSoundEffect.Play();
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        es.OnEnemyKilled();
        StartCoroutine(SonidoMuerte());
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
            hizoDaño = true;
            currentMoveSpeed = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            currentMoveSpeed = enemyData.MoveSpeed;
        }
    }

    private void OnDestroy()
    {
    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
