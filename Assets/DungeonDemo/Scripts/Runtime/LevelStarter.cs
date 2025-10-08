
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    void Start()
    {
        var templates = GameObject.FindObjectOfType<RoomTemplates>();
        if (templates == null)
        {
            Debug.LogError("RoomTemplates not found. Run Tools > Dungeon Demo > Create Demo first.");
            return;
        }

        // Instantiate Start Room at (0,0) grid
        if (templates.startRoom == null)
        {
            Debug.LogError("Start Room not assigned.");
            return;
        }

        var start = Instantiate(templates.startRoom, Vector3.zero, Quaternion.identity, templates.roomsParent);
        var room = start.GetComponent<Room>();
        if (room == null) room = start.AddComponent<Room>();
        room.gridPos = Vector2Int.zero;
        room.isStart = true;

        // Spawn player at child "PlayerSpawn" if available
        if (templates.playerPrefab != null)
        {
            var spawn = start.transform.Find("PlayerSpawn");
            Vector3 pos = start.transform.position;
            if (spawn != null) pos = spawn.position;
            Instantiate(templates.playerPrefab, pos, Quaternion.identity);
        }

        templates.RegisterRoom(room);
    }
}
