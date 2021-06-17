using UnityEngine;

public static class CharacterUtilities
{
    /// <summary>
    /// Returns TRUE if the given Transform is touching the ground.
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static bool IsGrounded(Transform transform)
    {
        float offset = 0.1f;
        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        Physics.Raycast(startPoint, Vector3.down, out RaycastHit hitInfo, 0.1f + offset);

        if (hitInfo.transform)
        {
            return hitInfo.transform != transform;
        }

        return false;
    }

    /// <summary>
    /// Returns TRUE if the given rigidbody is falling down.
    /// </summary>
    /// <param name="rigidbody"></param>
    /// <returns></returns>
    public static bool IsFalling(Rigidbody rigidbody)
    {
        if (!rigidbody)
        {
            Debug.Log($"{rigidbody.name} missing Rigidbody component");
            return false;
        }

        return rigidbody.velocity.y < 0.0f;
    }

    public static float SqrDistance(Transform character, Transform target)
    {
        Vector3 offset = target.position - character.position;

        return offset.sqrMagnitude;
    }
}
