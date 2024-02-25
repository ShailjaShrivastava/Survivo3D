using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float damage;
    public float areaOfEffect;

    public Transform target;
        void Start()
    {
        // Set velocity towards target
    }

    void Update()
    {
        if (target)
        {
            transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject); // Handle target loss
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.Damage(damage);
            enemy.CoinPrefab.SetActive(true);
            enemy.CoinPrefab.transform.SetParent(null);
             Destroy(gameObject); 
            // Apply area-of-effect damage if applicable
            // Destroy projectile (or handle persistence)
        }
    }


     //   public void OnCollisionEnter(Collision col)
  void OnCollisionEnter(Collision collision)
    {
              if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Damage(damage );

            // Apply area-of-effect damage if applicable
            // Destroy projectile (or handle persistence)
        }  
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetStats(float damage, float areaOfEffect)
    {
        this.damage = damage;
        this.areaOfEffect = areaOfEffect;
    }
}
