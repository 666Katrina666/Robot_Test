using UnityEngine;

[CreateAssetMenu(fileName = "LerpMovement", menuName = "MovementStrategy/Lerp")]
public class LerpMovement : MovementStrategy
{
    public override void Move(Rigidbody rb, Vector3 direction, float acceleration, float deceleration)
    {
        rb.linearVelocity = direction.sqrMagnitude > 0f
            ? Vector3.Lerp(rb.linearVelocity, direction, acceleration * Time.fixedDeltaTime)
            : Vector3.Lerp(rb.linearVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
    }
}
