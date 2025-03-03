using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 25f;
    [SerializeField] float speedIncreaseRate = 5f;
    [SerializeField] float speedDecreaseRate = 5f;
    [SerializeField] MovementStrategy movementStrategy;

    private Rigidbody rb;
    private Vector3 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetMovementInput(Vector2 input)
    {
        moveInput = new Vector3(0f, 0f, input.y).normalized * maxSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = transform.rotation * moveInput;

        movementStrategy.Move(rb, direction, speedIncreaseRate, speedDecreaseRate);
    }
}