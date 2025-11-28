using UnityEngine;
using System.Collections;

public class PlayerDeathHandler : MonoBehaviour
{
    public Entity_Health health;
    public GameOverUI gameOverUI;

    private bool handled = false;

    private void Start()
    {
        if (health == null)
            health = GetComponent<Entity_Health>();

        if (gameOverUI == null)
            Debug.LogError("PlayerDeathHandler: CHƯA gán GameOverUI trong Inspector!");
    }

    private void Update()
    {
        if (handled || health == null || gameOverUI == null)
            return;

        if (health.IsDead)
        {
            handled = true;
            StartCoroutine(DelayShowGameOver());  
        }
    }

    private IEnumerator DelayShowGameOver()
    {
        yield return new WaitForSeconds(1.5f);

        gameOverUI.ShowGameOver();
    }
}
