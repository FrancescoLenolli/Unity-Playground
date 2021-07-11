using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 10.0f;
    public float detectionDistance = 5.0f;
    public MovementValues movementValues;

    private float health;
    private PlayerControl targetPlayer;
    private new Rigidbody rigidbody;

    public PlayerControl TargetPlayer { get => targetPlayer; set => targetPlayer = value; }
    public Rigidbody Rigidbody { get => rigidbody; }

    private void Start()
    {
        health = maxHealth;
        rigidbody = GetComponent<Rigidbody>();
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
