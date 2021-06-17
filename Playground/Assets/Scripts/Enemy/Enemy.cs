using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 10.0f;
    public float detectionDistance = 5.0f;

    private float health;
    private PlayerControl targetPlayer;

    public PlayerControl TargetPlayer { get => targetPlayer; set => targetPlayer = value; }

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
