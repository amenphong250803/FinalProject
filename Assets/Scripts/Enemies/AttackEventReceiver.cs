using UnityEngine;

public class AttackEventReceiver : MonoBehaviour
{
    private EnemyCombat combat;

    private void Awake()
    {
        combat = GetComponentInParent<EnemyCombat>();
    }

    public void PerformAttack()
    {
        combat?.PerformAttack();
    }
}
