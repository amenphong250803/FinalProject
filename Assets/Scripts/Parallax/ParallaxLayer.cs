using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplierX;
    [SerializeField] private float parallaxMultiplierY;
    [SerializeField] private float imageWidthOffset = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMoveX, float distanceToMoveY)
    {
        background.position = background.position + new Vector3(distanceToMoveX * parallaxMultiplierX, distanceToMoveY * parallaxMultiplierY );
    }

    public void LookBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;

        if(imageRightEdge < cameraLeftEdge)
        {
            background.position = background.position + (Vector3.right * imageFullWidth);
        } 
        else if(imageLeftEdge > cameraRightEdge)
        {
            background.position = background.position + (Vector3.right * -imageFullWidth);
        }
    }
}
