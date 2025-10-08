
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [Header("Directional room banks (each should contain rooms that HAVE the corresponding door)")]
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    [Header("Special rooms")]
    public GameObject startRoom;
    public GameObject closedRoom;
    public GameObject bossRoom;
    public GameObject playerPrefab;

    [Header("Generation settings")]
    public int maxRooms = 30;
    public float roomSize = 16f;
    public bool useSeed = true;
    public int seed = 12345;

    [Header("Runtime (readonly)")]
    public int roomCount = 0;
    public List<Vector2Int> occupied = new List<Vector2Int>();
    public Transform roomsParent;

    System.Random rng;

    void Awake()
    {
        rng = useSeed ? new System.Random(seed) : new System.Random();
        if (roomsParent == null)
        {
            var go = new GameObject("Rooms");
            roomsParent = go.transform;
        }
    }

    public System.Random RNG => rng;

    public bool IsOccupied(Vector2Int gp) => occupied.Contains(gp);

    public void RegisterRoom(Room room)
    {
        if (!occupied.Contains(room.gridPos))
        {
            occupied.Add(room.gridPos);
        }
        roomCount++;
    }
}
