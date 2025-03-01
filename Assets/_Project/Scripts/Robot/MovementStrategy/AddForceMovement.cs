using UnityEngine;

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