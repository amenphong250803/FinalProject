using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;

    private float imageFullWidth;
    private float imageHalfWidth;


    public void Move(float distanceToMove)
    {
        background.position = background.position + new Vector3(distanceToMove * parallaxMultiplier, 0);
    }

    public void LookBackground(float cameraLeftEdge, float cameraRightEdge)
    {

    }
}
