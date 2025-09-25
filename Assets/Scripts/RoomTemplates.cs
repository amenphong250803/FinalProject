using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms = new List<GameObject>();

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    [Header("Room Spawn Limit")]
    public int minRooms = 30;
    public int maxRoomsLimit = 70;
    [HideInInspector] public int maxRooms;
    [HideInInspector] public int roomCount = 0;

    void Start()
    {
        maxRooms = Random.Range(minRooms, maxRoomsLimit + 1);
        Debug.Log("Dungeon target rooms: " + maxRooms);
    }

    void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {
            if (rooms.Count > 0)
            {
                // Spawn boss ở phòng cuối
                Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                spawnedBoss = true;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
