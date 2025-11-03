using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float imageWidthOffset = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        SpriteRenderer spriteRenderer = background.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            imageFullWidth = spriteRenderer.bounds.size.x;
            imageHalfWidth = imageFullWidth / 2f;
        }
    }

    public void Move(float deltaX)
    {
        background.position += Vector3.right * (deltaX * parallaxMultiplier);
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;
        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;
        
        if (imageRightEdge < cameraLeftEdge)
        {
            background.position += Vector3.right * imageFullWidth;
        }
        else if (imageLeftEdge > cameraRightEdge)
        {
            background.position += Vector3.left * imageFullWidth;
        }
    }
}
