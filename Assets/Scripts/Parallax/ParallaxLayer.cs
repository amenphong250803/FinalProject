using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplierX = 0.5f;
    [SerializeField] private float parallaxMultiplierY = 0.2f;

    [Header("Loop settings")]
    [SerializeField] private bool loopX = true;
    [SerializeField] private bool loopY = false;

    [Header("Offset")]
    [SerializeField] private float imageWidthOffset = 10;
    [SerializeField] private float imageHeightOffset = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    private float imageFullHeight;
    private float imageHalfHeight;

    public void CalculateImageWidth()
    {
        var spriteRenderer = background.GetComponent<SpriteRenderer>();

        imageFullWidth = spriteRenderer.bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;

        imageFullHeight = spriteRenderer.bounds.size.y;
        imageHalfHeight = imageFullHeight / 2;
    }

    public void Move(float distanceToMoveX, float distanceToMoveY)
    {
        background.position += new Vector3(
            distanceToMoveX * parallaxMultiplierX,
            distanceToMoveY * parallaxMultiplierY
        );
    }

    public void LookBackground(float cameraLeftEdge, float cameraRightEdge,
                               float cameraBottomEdge, float cameraTopEdge)
    {
        if (loopX)
        {
            float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;
            float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;

            if (imageRightEdge < cameraLeftEdge)
                background.position += Vector3.right * imageFullWidth;
            else if (imageLeftEdge > cameraRightEdge)
                background.position -= Vector3.right * imageFullWidth;
        }

        if (loopY)
        {
            float imageTopEdge = (background.position.y + imageHalfHeight) - imageHeightOffset;
            float imageBottomEdge = (background.position.y - imageHalfHeight) + imageHeightOffset;

            if (imageTopEdge < cameraBottomEdge)
                background.position += Vector3.up * imageFullHeight;
            else if (imageBottomEdge > cameraTopEdge)
                background.position -= Vector3.up * imageFullHeight;
        }
    }
}
