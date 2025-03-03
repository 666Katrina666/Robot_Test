using UnityEngine;

public class RoadSegmentInfo : BaseMinimapObject
{
    [SerializeField] RoadType roadType;
    [SerializeField] float rotationAngle; // угол (0, 90, 180, 270)

    public override Transform WorldTransform => transform;
    public override GameObject MinimapIcon { get; set; }

    public RoadType GetRoadType() => roadType;
    public float GetRotationAngle() => rotationAngle;
}

public enum RoadType { Straight, Turn, Intersection }