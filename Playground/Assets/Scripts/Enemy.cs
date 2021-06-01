using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 10.0f;

    private float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void GetDamage(float damage)
    {
        health -= damage;

        Debug.Log(health);

        if(health <= 0.0f)
        {
            Debug.Log("Enemy dead");
            health = maxHealth;
        }
    }
}
