using UnityEngine;
using System.Collections;

public class BossDeathHandler : MonoBehaviour
{
    [Header("Refs")]
    public Entity_Health health;
    public VictoryUI victoryUI;
    public Animator anim;
    public string deathStateName = "Dead";

    private bool handled = false;

    private void Start()
    {
        if (health == null)
            health = GetComponent<Entity_Health>();

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (handled || health == null || victoryUI == null)
            return;

        if (health.IsDead)
        {
            handled = true;
            StartCoroutine(WaitForDeathAnimAndWin());
        }
    }

    private IEnumerator WaitForDeathAnimAndWin()
    {
        if (anim != null && !string.IsNullOrEmpty(deathStateName))
        {
            while (!anim.GetCurrentAnimatorStateInfo(0).IsName(deathStateName))
                yield return null;

            float len = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(len);
        }
        victoryUI.ShowVictory();
    }
}
