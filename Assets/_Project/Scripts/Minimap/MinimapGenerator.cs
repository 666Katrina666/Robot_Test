using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapGenerator : MonoBehaviour
{
    [Header("Миникарта")]
    [SerializeField] RectTransform minimapParent;
    [SerializeField] RawImage backgroundImage;
    [SerializeField] Vector2 mapScale = new Vector2(100f, 100f);

    [Header("Префабы объектов")]
    [SerializeField] GameObject buildingIconPrefab;
    [SerializeField] GameObject playerIconPrefab;

    [Header("Префабы дорог")]
    [SerializeField] GameObject straightRoadPrefab;
    [SerializeField] GameObject turnRoadPrefab;
    [SerializeField] GameObject intersectionPrefab;

    private Dictionary<RoadType, GameObject> roadPrefabs;
    private readonly Dictionary<Transform, GameObject> minimapObjects = new();
    private Transform player;

    private void Awake()
    {
        InitializeRoadPrefabsDictionary();
    }

    private void Start()
    {
        InitializeBuildings();
        InitializeRoads();
        InitializePlayer();
    }

    private void FixedUpdate()
    {
        UpdateMinimapPositions();
    }

    private void InitializeRoadPrefabsDictionary()
    {
        roadPrefabs ??= new Dictionary<RoadType, GameObject>();

        if (straightRoadPrefab != null && !roadPrefabs.ContainsKey(RoadType.Straight))
            roadPrefabs[RoadType.Straight] = straightRoadPrefab;

        if (turnRoadPrefab != null && !roadPrefabs.ContainsKey(RoadType.Turn))
            roadPrefabs[RoadType.Turn] = turnRoadPrefab;

        if (intersectionPrefab != null && !roadPrefabs.ContainsKey(RoadType.Intersection))
            roadPrefabs[RoadType.Intersection] = intersectionPrefab;
    }

    private void InitializePlayer()
    {
        var playerObject = FindFirstObjectByType<MovementController>();
        if (playerObject == null)
        {
            Debug.LogError("Не найден PlayerController!");
            return;
        }

        player = playerObject.transform;
        var playerIcon = Instantiate(playerIconPrefab, minimapParent);
        minimapObjects[player] = playerIcon;
    }

    private void InitializeBuildings()
    {
        var buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (var building in buildings)
        {
            if (building.TryGetComponent<BuildingMinimapObject>(out var buildingComponent))
            {
                var icon = Instantiate(buildingIconPrefab, minimapParent);
                var rectTransform = icon.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = WorldToMinimapPosition(building.transform.position);
                buildingComponent.MinimapIcon = icon;
                minimapObjects[buildingComponent.WorldTransform] = icon;
            }
        }
    }

    private void InitializeRoads()
    {
        var roads = GameObject.FindGameObjectsWithTag("Road");
        foreach (var roadObject in roads)
        {
            var roadComponent = roadObject.GetComponent<RoadSegmentInfo>();
            if (roadComponent != null)
            {
                var roadIcon = CreateRoadIcon(roadComponent);
                roadComponent.MinimapIcon = roadIcon;
                minimapObjects[roadComponent.WorldTransform] = roadIcon;
            }
        }
    }

    private GameObject CreateRoadIcon(RoadSegmentInfo road)
    {
        var prefab = roadPrefabs.GetValueOrDefault(road.GetRoadType());
        if (prefab == null)
        {
            Debug.LogWarning($"Не найден префаб для типа дороги: {road.GetRoadType()}");
            return null;
        }

        var icon = Instantiate(prefab, minimapParent);
        var rectTransform = icon.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = WorldToMinimapPosition(road.transform.position);
        rectTransform.rotation = Quaternion.Euler(0, 0, -road.GetRotationAngle());

        return icon;
    }

    private void UpdateMinimapPositions()
    {
        if (player == null || minimapParent == null) return;

        Vector2 centerPoint = new(
            minimapParent.rect.width / 2f,
            minimapParent.rect.height / 2f
        );

        foreach (var pair in minimapObjects)
        {
            var worldPos = pair.Key.position;
            var icon = pair.Value;
            var rectTransform = icon.GetComponent<RectTransform>();

            Vector3 relativePos = worldPos - player.position;

            Vector2 minimapPos = WorldToMinimapPosition(relativePos);

            rectTransform.anchoredPosition = minimapPos;

            if (pair.Key == player)
            {
                icon.transform.rotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);
            }
        }
    }

    private Vector2 WorldToMinimapPosition(Vector3 worldPosition)
    {
        return new Vector2(
        (worldPosition.x / mapScale.x) * minimapParent.rect.width,
        (worldPosition.z / mapScale.y) * minimapParent.rect.height
        );
    }
}
