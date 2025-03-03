using UnityEngine;

public class BuildingMinimapObject : BaseMinimapObject
{
    public override Transform WorldTransform => transform;
    public override GameObject MinimapIcon { get; set; }
}
