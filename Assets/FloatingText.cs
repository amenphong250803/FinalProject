using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float moveSpeed = 50f;
    public float lifetime = 1f;

    private float timer;

    private void Awake()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();
    }

    public void Setup(string message, Color color)
    {
        text.text = message;
        text.color = color;
        timer = lifetime;
    }

    private void Update()
    {

        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
