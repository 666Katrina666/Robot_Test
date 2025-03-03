using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public interface IMinimapObject
{
    Transform WorldTransform { get; }
    GameObject MinimapIcon { get; set; }
}

public abstract class BaseMinimapObject : MonoBehaviour, IMinimapObject
{
    public abstract Transform WorldTransform { get; }
    public abstract GameObject MinimapIcon { get; set; }
}