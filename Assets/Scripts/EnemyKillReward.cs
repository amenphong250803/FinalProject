using UnityEngine;

public class EnemyKillReward : MonoBehaviour
{
    public int killValue = 1;  
    private bool rewarded = false;

    public void RewardKill()
    {
        if (rewarded) return;
        rewarded = true;

        PlayerProgression playerProg = FindObjectOfType<PlayerProgression>();
        if (playerProg != null)
        {
            playerProg.AddKill(killValue);
        }
    }
}
