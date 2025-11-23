using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float lastCameraPositionY;
    private float cameraHalfWidth;


    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        lastCameraPositionX = mainCamera.transform.position.x;
        lastCameraPositionY = mainCamera.transform.position.y;
        CalculateImageLength();
    }

    private void FixedUpdate()
    {
        float CurrentCameraPositionX = mainCamera.transform.position.x;
        float CurrentCameraPositionY = mainCamera.transform.position.y;

        float distanceToMoveX = CurrentCameraPositionX - lastCameraPositionX;
        float distanceToMoveY = CurrentCameraPositionY - lastCameraPositionY;

        lastCameraPositionX = CurrentCameraPositionX;
        lastCameraPositionY = CurrentCameraPositionY;

        float cameraLeftEdge = CurrentCameraPositionX - cameraHalfWidth;
        float cameraRightEgde = CurrentCameraPositionX + cameraHalfWidth;
        
        foreach(ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMoveX, distanceToMoveY);
            layer.LookBackground(cameraLeftEdge, cameraRightEgde);
        }
    }

    private void CalculateImageLength()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}
