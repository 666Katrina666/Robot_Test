using UnityEngine;

public class RobotInputHandler : MonoBehaviour
{
    private InputActions inputActions;
    private MovementController movementController;
    private RotationController rotationController;

    private void Awake()
    {
        inputActions = new InputActions();
        movementController = GetComponent<MovementController>();
        rotationController = GetComponent<RotationController>();
    }

    private void OnEnable()
    {
        inputActions.Movement.Enable();
    }

    private void OnDisable()
    {
        inputActions?.Movement.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 movement = inputActions.Movement.Move.ReadValue<Vector2>();
        movementController.SetMovementInput(movement);

        float rotation = 0f;

        if (inputActions.Movement.TurnLeft.IsPressed() && inputActions.Movement.TurnRight.IsPressed())
        {
            rotation = 0f;
        }
        else if (inputActions.Movement.TurnLeft.IsPressed())
        {
            rotation = -1f;
        }
        else if (inputActions.Movement.TurnRight.IsPressed())
        {
            rotation = 1f;
        }

        rotationController.SetRotationInput(rotation);
    }
}