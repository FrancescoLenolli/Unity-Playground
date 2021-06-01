using System;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    public Collider myCollider = null;

    private Action<Enemy> onEnemyCollision;

    public Action<Enemy> OnEnemyCollision { get => onEnemyCollision; set => onEnemyCollision = value; }
    public Collider Collider { get => myCollider; set => myCollider = value; }

    private void Awake()
    {
        myCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy)
            onEnemyCollision?.Invoke(enemy);
    }
}
