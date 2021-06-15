using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MovementValues", fileName = "NewValues")]
public class MovementValues : ScriptableObject
{
    public float speed = 1.0f;
    public float runSpeedMultiplier = 2.0f;
    public float attackCooldown = 0.5f;
    public float jumpForce = 1.0f;
}
