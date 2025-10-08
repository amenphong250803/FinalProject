
using UnityEngine;

[ExecuteAlways]
public class Room : MonoBehaviour
{
    public Vector2Int gridPos;
    public bool isBoss;
    public bool isStart;
    public float sizeOverride = -1f; // if < 0, use templates.roomSize

    private void OnDrawGizmos()
    {
        float s = sizeOverride > 0 ? sizeOverride : FindSize();
        Vector3 center = transform.position;
        Gizmos.color = isBoss ? Color.red : (isStart ? Color.green : Color.cyan);
        Gizmos.DrawWireCube(center, new Vector3(s, s, 0.1f));

        // Draw door points
        Gizmos.color = Color.yellow;
        foreach (Transform t in transform)
        {
            var sp = t.GetComponent<RoomSpawner>();
            if (sp != null)
            {
                Gizmos.DrawSphere(t.position, 0.25f);
            }
        }
    }

    float FindSize()
    {
        var templates = GameObject.FindObjectOfType<RoomTemplates>();
        return templates != null ? templates.roomSize : 16f;
    }
}
