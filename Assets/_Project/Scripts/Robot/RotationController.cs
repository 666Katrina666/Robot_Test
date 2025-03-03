using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotationController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private float currentRotation;

    public void SetRotationInput(float rotation)
    {
        currentRotation = rotation;
    }

    private void FixedUpdate()
    {
        if (currentRotation != 0f) Rotate();
    }

    private void Rotate()
    {
        float rotationAmount = currentRotation * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        transform.rotation *= deltaRotation;
    }
}
