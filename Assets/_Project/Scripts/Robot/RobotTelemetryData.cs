using UnityEngine;

public class RobotTelemetryData : MonoBehaviour
{
    private Rigidbody rb;

    public float Speed { get; private set; }
    public Vector3 Position { get; private set; }
    public float RotationAngle { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        UpdateTelemetryData();
    }

    private void UpdateTelemetryData()
    {
        Speed = rb.linearVelocity.magnitude;

        Position = new Vector3(transform.position.x, 0, transform.position.z); // игнорируем Y, т.к. вам нужны X и Z

        RotationAngle = transform.eulerAngles.y;
    }
}
