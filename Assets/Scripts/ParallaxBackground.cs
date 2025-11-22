using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHalfWidth;


    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
    }

    private void Update()
    {
        float CurrentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMove = CurrentCameraPositionX - lastCameraPositionX;
        lastCameraPositionX = CurrentCameraPositionX;

        float cameraLeftEdge = CurrentCameraPositionX - cameraHalfWidth;
        float cameraRightEgde = CurrentCameraPositionX + cameraHalfWidth;
        
        foreach(ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
        }
    }
}
