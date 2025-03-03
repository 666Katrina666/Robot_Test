using TMPro;
using UnityEngine;

public class TelemetryUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text positionText;
    [SerializeField] TMP_Text rotationText;

    private RobotTelemetryData robotTelemetryData;

    private void Awake()
    {
        robotTelemetryData = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotTelemetryData>();
    }

    private void FixedUpdate()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        speedText.text = $"Скорость: {robotTelemetryData.Speed:F2} m/s";
        positionText.text = $"Позиция: X = {robotTelemetryData.Position.x:F2}, Z = {robotTelemetryData.Position.z:F2}";
        rotationText.text = $"Поворот: {robotTelemetryData.RotationAngle:F2}°";
    }
}