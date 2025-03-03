using UnityEngine;

public abstract class MovementStrategy : ScriptableObject
{
    public abstract void Move(Rigidbody rb, Vector3 direction, float acceleration, float deceleration);
}