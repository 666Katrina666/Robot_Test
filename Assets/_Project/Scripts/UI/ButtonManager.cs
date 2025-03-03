using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject instructionPanel;
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new();
        inputActions.Game.Restart.performed += _ => ResetLevel();
    }

    private void OnEnable()
    {
        inputActions.Game.Enable();
    }

    private void OnDisable()
    {
        inputActions?.Game.Disable();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowOrHideInstructions()
    {
        instructionPanel.SetActive(!instructionPanel.activeSelf);
    }
}
