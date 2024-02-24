using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public float damage;
    public bool isDead = false; // Flag for tracking death

    private Transform target; // Reference to the target (Tower)
    private Rigidbody rb; // Reference to Rigidbody component for movement

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    public Vector3 GetPosition()
    {

      return  this.transform.position;

    }
   public void Update()
    {
        MoveTowardsTarget();

        if (health <= 0)
        {
            Die();
        }
    }

    void MoveTowardsTarget()
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction = target.position - transform.position;
        rb.MovePosition(transform.position + direction.normalized * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Tower"))
        {
            Tower tower = collision.collider.GetComponent<Tower>();
            tower.TakeDamage(damage);
            // Optional: Apply knockback or stun to tower
        }
    }

    void Die()
    {
        isDead = true;
        // Visualize death (e.g., play animation, change material)
        // Add score or reward logic
        // Destroy or disable the enemy object
    }


    public void Damage(float damageAmount)
    {
        health -= damageAmount ;
       
    }




}
