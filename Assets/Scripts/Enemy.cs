using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        DamageDealer damageDealer = collisionObject.gameObject.GetComponent<DamageDealer>();

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        if (health <= 0)
            Destroy(gameObject);
    }
}
