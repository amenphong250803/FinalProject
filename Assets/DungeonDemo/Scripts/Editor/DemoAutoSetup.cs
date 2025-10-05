
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.IO;

public class DungeonDemoCreator
{
    const string Root = "Assets/DungeonDemo";
    const string PrefabFolder = Root + "/Prefabs";
    const string SceneFolder  = Root + "/Scenes";

    [MenuItem("Tools/Dungeon Demo/Create Demo")]
    public static void CreateDemo()
    {
        Directory.CreateDirectory(PrefabFolder);
        Directory.CreateDirectory(SceneFolder);

        // Create basic player prefab
        var playerGO = new GameObject("Player");
        playerGO.AddComponent<PlayerController2D>();
        var playerPrefab = SaveAsPrefab(playerGO, PrefabFolder + "/Player.prefab");

        // Create room prefabs
        GameObject start = CreateRoomPrefab("StartRoom", includeAllDoors:true);
        var startPrefab = SaveAsPrefab(start, PrefabFolder + "/StartRoom.prefab");

        GameObject bottom = CreateRoomPrefab("Room_Bottom", includeAllDoors:true);
        var bottomPrefab = SaveAsPrefab(bottom, PrefabFolder + "/Room_Bottom.prefab");

        GameObject top = CreateRoomPrefab("Room_Top", includeAllDoors:true);
        var topPrefab = SaveAsPrefab(top, PrefabFolder + "/Room_Top.prefab");

        GameObject left = CreateRoomPrefab("Room_Left", includeAllDoors:true);
        var leftPrefab = SaveAsPrefab(left, PrefabFolder + "/Room_Left.prefab");

        GameObject right = CreateRoomPrefab("Room_Right", includeAllDoors:true);
        var rightPrefab = SaveAsPrefab(right, PrefabFolder + "/Room_Right.prefab");

        GameObject closed = CreateRoomPrefab("ClosedRoom", includeAllDoors:false);
        var closedPrefab = SaveAsPrefab(closed, PrefabFolder + "/ClosedRoom.prefab");

        GameObject boss = CreateRoomPrefab("BossRoom", includeAllDoors:false, isBoss:true);
        var bossPrefab = SaveAsPrefab(boss, PrefabFolder + "/BossRoom.prefab");

        // Scene & templates
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        var templatesGO = new GameObject("RoomTemplates");
        var templates = templatesGO.AddComponent<RoomTemplates>();
        templates.topRooms = new GameObject[] { topPrefab };
        templates.bottomRooms = new GameObject[] { bottomPrefab };
        templates.leftRooms = new GameObject[] { leftPrefab };
        templates.rightRooms = new GameObject[] { rightPrefab };
        templates.closedRoom = closedPrefab;
        templates.bossRoom = bossPrefab;
        templates.startRoom = startPrefab;
        templates.playerPrefab = playerPrefab;
        templates.maxRooms = 30;
        templates.roomSize = 16f;
        templates.useSeed = true;
        templates.seed = 12345;

        var starter = new GameObject("LevelStarter");
        starter.AddComponent<LevelStarter>();

        // Save scene
        string scenePath = SceneFolder + "/DemoScene.unity";
        EditorSceneManager.SaveScene(scene, scenePath);
        EditorSceneManager.OpenScene(scenePath);
        EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<Object>(scenePath));

        EditorUtility.DisplayDialog("Dungeon Demo", "Demo created! Opened DemoScene. Press Play to generate a dungeon.", "OK");
    }

    static GameObject CreateRoomPrefab(string name, bool includeAllDoors, bool isBoss=false)
    {
        var go = new GameObject(name);
        var room = go.AddComponent<Room>();
        room.isBoss = isBoss;
        room.isStart = name.Contains("Start");

        float size = 16f;

        // Optional gizmo-only frame object (empty)
        // Door spawn points
        if (includeAllDoors)
        {
            // Top -> next needs Bottom door (1)
            CreateDoor(go.transform, "Door_Top", new Vector3(0f, size/2f, 0f), 1);
            // Bottom -> next needs Top door (2)
            CreateDoor(go.transform, "Door_Bottom", new Vector3(0f, -size/2f, 0f), 2);
            // Left -> next needs Right door (4)
            CreateDoor(go.transform, "Door_Left", new Vector3(-size/2f, 0f, 0f), 4);
            // Right -> next needs Left door (3)
            CreateDoor(go.transform, "Door_Right", new Vector3(size/2f, 0f, 0f), 3);
        }

        // Player spawn only for StartRoom
        if (room.isStart)
        {
            var sp = new GameObject("PlayerSpawn");
            sp.transform.SetParent(go.transform);
            sp.transform.localPosition = Vector3.zero;
        }

        return go;
    }

    static void CreateDoor(Transform parent, string name, Vector3 localPos, int openingDir)
    {
        var d = new GameObject(name);
        d.transform.SetParent(parent);
        d.transform.localPosition = localPos;
        var sp = d.AddComponent<RoomSpawner>();
        sp.openingDirection = openingDir;
    }

    static GameObject SaveAsPrefab(GameObject go, string path)
    {
        var prefab = PrefabUtility.SaveAsPrefabAsset(go, path);
        GameObject.DestroyImmediate(go);
        return prefab;
    }
}
#endif
