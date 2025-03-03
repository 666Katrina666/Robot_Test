using UnityEngine;

[CreateAssetMenu(fileName = "SmoothDampMovement", menuName = "MovementStrategy/SmoothDamp")]
public class SmoothDampMovement : MovementStrategy
{
    private Vector3 currentVelocity = Vector3.zero;

    public override void Move(Rigidbody rb, Vector3 direction, float acceleration, float deceleration)
    {
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, direction, ref currentVelocity, direction.sqrMagnitude > 0 ? 1f / acceleration : 1f / deceleration);
    }
}