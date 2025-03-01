using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private enum CameraMode
    {
        TopDown,
        ThirdPerson
    }

    [Header("Camera Settings")]
    [SerializeField] CameraMode currentMode = CameraMode.ThirdPerson;
    [SerializeField] float cameraSwitchSpeed = 15f;
    [SerializeField] Transform target;

    [Header("Top Down Settings")]
    [SerializeField] Vector3 topDownOffset = new(0, 15, 0);
    [SerializeField] float topDownRotation = 90f;

    [Header("Third Person Settings")]
    [SerializeField] Vector3 thirdPersonOffset = new(0, 3, -6);
    [SerializeField] Vector3 thirdPersonLockOffset = new(0, 0.5f, 2);

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Camera.ChangeCamera.performed += _ => ChangeCameraTarget();
    }

    private void Start()
    {
        UpdateCameraTarget();
    }

    private void OnEnable()
    {
        inputActions.Camera.Enable();
    }

    private void OnDisable()
    {
        inputActions?.Camera.Disable();
    }

    private void FixedUpdate()
    {
        UpdateCameraTarget();
        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, targetPosition, cameraSwitchSpeed * Time.fixedDeltaTime), Quaternion.Lerp(transform.rotation, targetRotation, cameraSwitchSpeed * Time.fixedDeltaTime));
    }

    private void ChangeCameraTarget()
    {
        currentMode = currentMode == CameraMode.ThirdPerson ? CameraMode.TopDown : CameraMode.ThirdPerson;
    }

    private void UpdateCameraTarget()
    {
        switch (currentMode)
        {
            case CameraMode.TopDown:
                targetPosition = target.position + topDownOffset;
                targetRotation = Quaternion.Euler(topDownRotation, 0, 0);
                break;

            case CameraMode.ThirdPerson:
                targetPosition = target.position + target.TransformDirection(thirdPersonOffset);
                targetRotation = Quaternion.LookRotation(target.position + thirdPersonLockOffset - targetPosition, Vector3.up);
                break;
        }
    }
}
