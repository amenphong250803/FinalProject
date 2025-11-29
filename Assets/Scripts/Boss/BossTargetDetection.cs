using UnityEngine;

public class BossTargetDetection : MonoBehaviour
{
    public Transform player;
    public float distanceToPlayer;

    public AudioClip bossBGM;
    public AudioClip normalBGM;
    public float triggerDistance = 12f;

    private bool bossMusicPlaying = false;
    public bool bossIsDead = false;

    public bool HasTarget => player != null;

    void Update()
    {
        if (bossIsDead) return;

        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (!HasTarget) return;

        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= triggerDistance && !bossMusicPlaying)
        {
            bossMusicPlaying = true;
            BGMManager.Instance.FadeTo(bossBGM, 1f);
        }
        else if (distanceToPlayer > triggerDistance && bossMusicPlaying)
        {
            bossMusicPlaying = false;
            BGMManager.Instance.FadeTo(normalBGM, 1f);
        }
    }

    public void OnBossDead()
    {
        bossIsDead = true;
        bossMusicPlaying = false;
        BGMManager.Instance.FadeTo(normalBGM, 1f);
    }
}
