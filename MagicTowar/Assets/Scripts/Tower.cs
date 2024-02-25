using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tower : MonoBehaviour
{
    public float health;
    public float mana;
    public float maxHealth;
    public float maxMana;
    public float healthBarDisplayOffset; // Adjust UI positioning
  
  
   public List<Spell> spells ;
    public float baseAttackRate; // Time between spell casts
    public float currentAttackRate; // Adjusted attack rate with bonuses
    public Transform projectileSpawnPoint;

    private float nextAttackTime; // Timer for next spell cast
    private HealthBar healthBar; // Reference to UI health bar component
    private     Enemy closestEnemy ;
    private GameManager gm;

    public float moveSpeed;
    public JoyStick jS;
    private Vector3 moveVector;
    public float RadiusofAttack = Mathf.Infinity;
    void Start()
    {
        health = maxHealth;
        mana = maxMana;
        currentAttackRate = baseAttackRate;
        healthBar = GetComponentInChildren<HealthBar>();
        gm =  FindObjectOfType<GameManager>();
       RadiusofAttack = Mathf.Infinity;
      
    }



        private void move()
    {
        transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
    }

    void Update()
    {

        moveVector.x = jS.Horizontal();
        moveVector.z = jS.Vertical();
        move();


        if (Time.time >= nextAttackTime && mana > 0)
        {
            closestEnemy = GetClosestEnemy();
            if(closestEnemy != null)
            CastSpell(ChooseRandomSpell()); // Cast a random spell if mana allows
            
            nextAttackTime = Time.time + currentAttackRate;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health / maxHealth); // Update health bar UI
        // Optional: Visualize damage taken (e.g., change material, play sound)
        if (health <= 0)
        {
           Destroy(this.gameObject);
            gm.GameOver();
        }
    }

    public void CastSpell(Spell spell)
    {


        if (mana >= spell.manaCost)
        {
            mana -= spell.manaCost;
            // Instantiate projectile prefab based on spell type
            Projectile projectile = Instantiate(spell.projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();;
           projectile.SetTarget(GetClosestEnemy().transform); // Optionally, target specific enemies
           projectile.SetStats(spell.damage, spell.areaOfEffect);
    
            }
    }

    Spell ChooseRandomSpell()
    {
        // Choose a random spell from the available ones
        return spells[Random.Range(0, spells.Count)];
    }

    Enemy GetClosestEnemy()
    {
        // Find the closest enemy within range
    
        float minDistance = RadiusofAttack;
        foreach (Enemy enemy in gm.enemies)
    //  for(int i = 0 ; i < GameManager.instance.enemies.Count ; i++ )
        {
            float distance = Vector3.Distance(this.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                
                minDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    public void IncreaseAttackRate(float bonus)
    {
        // Apply temporary or permanent attack rate bonuses
        currentAttackRate -= bonus;
    }

    public void GainMana(float amount)
    {
        // Replenish mana
        mana = Mathf.Min(mana + amount, maxMana);
    }





    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
         Destroy(collision.gameObject);
        }
    }



}
