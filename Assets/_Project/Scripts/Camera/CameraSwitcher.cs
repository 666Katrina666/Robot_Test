using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] CinemachineCamera thirdPersonCam;
    [SerializeField] CinemachineCamera topDownCam;

    private InputActions inputActions;
    private bool isTopDown = false;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Camera.ChangeCamera.performed += _ => SwitchCamera();
    }

    private void OnEnable()
    {
        inputActions.Camera.Enable();
    }

    private void OnDisable()
    {
        inputActions?.Camera.Disable();
    }

    private void SwitchCamera()
    {
        isTopDown = !isTopDown;
        thirdPersonCam.Priority = isTopDown ? 0 : 10;
        topDownCam.Priority = isTopDown ? 10 : 0;
    }
}
