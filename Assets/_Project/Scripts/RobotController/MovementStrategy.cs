using UnityEngine;

public abstract class MovementStrategy : ScriptableObject
{
    public abstract void Move(Rigidbody rb, Vector3 direction, float acceleration, float deceleration);
}

[CreateAssetMenu(fileName = "SmoothDampMovement", menuName = "MovementStrategy/SmoothDamp")]
public class SmoothDampMovement : MovementStrategy
{
    private Vector3 currentVelocity = Vector3.zero;

    public override void Move(Rigidbody rb, Vector3 direction, float acceleration, float deceleration)
    {
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, direction, ref currentVelocity, direction.sqrMagnitude > 0 ? 1f / acceleration : 1f / deceleration);
    }
}

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

[CreateAssetMenu(fileName = "AddForceMovement", menuName = "MovementStrategy/AddForce")]
public class AddForceMovement : MovementStrategy
{
    public override void Move(Rigidbody rb, Vector3 direction, float acceleration, float deceleration)
    {
        if (direction.sqrMagnitude > 0f)
            rb.AddForce((direction - rb.linearVelocity) * acceleration, ForceMode.Acceleration);
        else
            rb.AddForce(-rb.linearVelocity * acceleration, ForceMode.Acceleration);
    }
}