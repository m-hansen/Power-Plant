using UnityEngine;

// Suggestions for improvement:
// Zoom in/out toward the mouse position when scrolling
public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private float zoomSpeed = 1f;

    [SerializeField]
    private float minCameraSize = 8f;

    [SerializeField]
    private float maxCameraSize = 15f;

    private Camera myCamera;

    void Awake()
    {
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        float newSize = myCamera.orthographicSize + (-Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime);
        newSize = Mathf.Clamp(newSize, minCameraSize, maxCameraSize);
        myCamera.orthographicSize = newSize;
    }
}
