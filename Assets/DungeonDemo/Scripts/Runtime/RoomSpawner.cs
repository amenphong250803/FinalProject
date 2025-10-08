
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // openingDirection indicates what door the NEXT room must have:
    // 1 -> NEXT needs Bottom door
    // 2 -> NEXT needs Top door
    // 3 -> NEXT needs Left door
    // 4 -> NEXT needs Right door
    public int openingDirection;

    RoomTemplates templates;
    Room parentRoom;
    bool spawned = false;

    void Awake()
    {
        templates = GameObject.FindObjectOfType<RoomTemplates>();
        parentRoom = GetComponentInParent<Room>();
    }

    void Start()
    {
        Invoke(nameof(Spawn), Random.Range(0.05f, 0.15f));
    }

    void Spawn()
    {
        if (spawned || templates == null || parentRoom == null) return;

        if (templates.roomCount >= templates.maxRooms)
        {
            // Stop spawning after cap; optionally place closed cap
            spawned = true;
            return;
        }

        Vector2Int delta = Vector2Int.zero;
        switch (openingDirection)
        {
            case 1: delta = Vector2Int.down; break;
            case 2: delta = Vector2Int.up; break;
            case 3: delta = Vector2Int.left; break;
            case 4: delta = Vector2Int.right; break;
        }

        Vector2Int targetGrid = parentRoom.gridPos + delta;
        if (templates.IsOccupied(targetGrid))
        {
            spawned = true;
            return;
        }

        GameObject prefabToSpawn = null;

        bool spawnBoss = (templates.roomCount >= templates.maxRooms - 1) && templates.bossRoom != null;
        if (spawnBoss)
        {
            prefabToSpawn = templates.bossRoom;
        }
        else
        {
            switch (openingDirection)
            {
                case 1:
                    if (templates.bottomRooms != null && templates.bottomRooms.Length > 0)
                        prefabToSpawn = templates.bottomRooms[templates.RNG.Next(0, templates.bottomRooms.Length)];
                    break;
                case 2:
                    if (templates.topRooms != null && templates.topRooms.Length > 0)
                        prefabToSpawn = templates.topRooms[templates.RNG.Next(0, templates.topRooms.Length)];
                    break;
                case 3:
                    if (templates.leftRooms != null && templates.leftRooms.Length > 0)
                        prefabToSpawn = templates.leftRooms[templates.RNG.Next(0, templates.leftRooms.Length)];
                    break;
                case 4:
                    if (templates.rightRooms != null && templates.rightRooms.Length > 0)
                        prefabToSpawn = templates.rightRooms[templates.RNG.Next(0, templates.rightRooms.Length)];
                    break;
            }
        }

        if (prefabToSpawn == null)
        {
            spawned = true;
            return;
        }

        float size = templates.roomSize;
        Vector3 worldPos = new Vector3(targetGrid.x * size, targetGrid.y * size, 0f);
        var roomGO = GameObject.Instantiate(prefabToSpawn, worldPos, Quaternion.identity, templates.roomsParent);
        var room = roomGO.GetComponent<Room>();
        if (room == null) room = roomGO.AddComponent<Room>();
        room.gridPos = targetGrid;
        room.isBoss = spawnBoss;

        templates.RegisterRoom(room);
        spawned = true;
    }
}
