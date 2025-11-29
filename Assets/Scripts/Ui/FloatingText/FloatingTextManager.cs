using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance;

    public Canvas mainCanvas;
    public FloatingText floatingTextPrefab;

    private void Awake()
    {
        Instance = this;

        if (mainCanvas == null)
            mainCanvas = GetComponentInParent<Canvas>();
    }

    public void ShowText(string message, Vector3 worldPosition, Color color)
    {
        if (floatingTextPrefab == null || mainCanvas == null)
            return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        FloatingText ft = Instantiate(floatingTextPrefab, mainCanvas.transform);
        ft.transform.position = screenPos;
        ft.Setup(message, color);
    }
}
