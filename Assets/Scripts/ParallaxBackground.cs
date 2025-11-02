using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private Camera mainCamera;
    private float lastCameraX;

    private float cameraHalfWidth;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        CalculateImageLengths();
    }

    private void FixedUpdate()
    {
        float currentCameraX = mainCamera.transform.position.x;
        float deltaX = currentCameraX - lastCameraX;
        lastCameraX = currentCameraX;

        float cameraLeftEdge = currentCameraX - cameraHalfWidth;
        float cameraRightEdge = currentCameraX + cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(deltaX);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    private void CalculateImageLengths()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}
