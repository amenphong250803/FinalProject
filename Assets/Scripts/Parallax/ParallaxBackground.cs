using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float lastCameraPositionY;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;

        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        cameraHalfHeight = mainCamera.orthographicSize;

        lastCameraPositionX = mainCamera.transform.position.x;
        lastCameraPositionY = mainCamera.transform.position.y;

        InitializeLayers();
    }

    private void FixedUpdate()
    {
        float currentX = mainCamera.transform.position.x;
        float currentY = mainCamera.transform.position.y;

        float distanceX = currentX - lastCameraPositionX;
        float distanceY = currentY - lastCameraPositionY;

        lastCameraPositionX = currentX;
        lastCameraPositionY = currentY;

        float cameraLeftEdge = currentX - cameraHalfWidth;
        float cameraRightEdge = currentX + cameraHalfWidth;
        float cameraBottomEdge = currentY - cameraHalfHeight;
        float cameraTopEdge = currentY + cameraHalfHeight;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceX, distanceY);
            layer.LookBackground(cameraLeftEdge, cameraRightEdge, cameraBottomEdge, cameraTopEdge);
        }
    }

    private void InitializeLayers()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}
