using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_MoveAround : State
{
    public MovementValues movementValues;
    public float randomDirectionTimer = 3.0f;

    private new Rigidbody rigidbody;
    private Vector3 moveDirection = Vector3.zero;
    private float currentTimer = 0.0f;

    public float RandomNumber { get => Random.Range(-1.0f, 1.0f); }

    public override void UpdateState()
    {
        Move(GetDirection());
    }

    private void Move(Vector3 direction)
    {
        Vector3 velocity = movementValues.speed * Time.fixedDeltaTime * direction;
        Vector3 newPosition = transform.position + velocity;

        if (!rigidbody)
            rigidbody = Owner.GetComponent<Rigidbody>();

        rigidbody.MovePosition(newPosition);
    }

    private Vector3 GetDirection()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            currentTimer = randomDirectionTimer;
            moveDirection = new Vector3(RandomNumber, 0, RandomNumber);
        }

        return moveDirection;
    }
}
