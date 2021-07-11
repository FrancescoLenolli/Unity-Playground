using UnityEngine;

public class State_Move : State
{
    public float randomDirectionTimer = 3.0f;

    private Vector3 moveDirection = Vector3.zero;
    private float currentTimer = 0.0f;

    public float RandomNumber { get => Random.Range(-1.0f, 1.0f); }

    public override void UpdateState()
    {
        Vector3 targetDirection = Owner.TargetPlayer ? IsTargetClose() ? Vector3.zero : GetTargetDirection() : GetRandomDirection();
        Move(targetDirection);
    }

    private void Move(Vector3 direction)
    {
        Vector3 velocity = Owner.movementValues.speed * Time.fixedDeltaTime * direction;
        Vector3 newPosition = transform.position + velocity;

        Owner.Rigidbody.MovePosition(newPosition);
        Owner.transform.LookAt(newPosition);
    }

    private Vector3 GetRandomDirection()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            currentTimer = randomDirectionTimer;
            moveDirection = new Vector3(RandomNumber, 0, RandomNumber);

            StateMachine.SwitchState(typeof(State_SearchForPlayer));
        }

        return moveDirection;
    }

    private Vector3 GetTargetDirection()
    {
        if (currentTimer < randomDirectionTimer)
            currentTimer = randomDirectionTimer;

        return (Owner.TargetPlayer.transform.position - Owner.transform.position).normalized;
    }

    private bool IsTargetClose()
    {
        float minimumDistance = 1.0f;

        return CharacterUtilities.SqrDistance(Owner.transform, Owner.TargetPlayer.transform) < minimumDistance * minimumDistance;
    }

}
