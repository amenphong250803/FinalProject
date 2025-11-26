using UnityEngine;

public class BossTargetDetection : MonoBehaviour
{
    public Transform player;
    public float distanceToPlayer;

    public bool HasTarget => player != null;

    void Update()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (HasTarget)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
        }
    }
}
