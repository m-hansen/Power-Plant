using UnityEngine;

//
// Improvement ideas:
//
// Since we control move speed and allow the feeling of a smooth moving camera, it makes sense to add the feel of "friction"
// instead of stopping movement the second the mouse is released.
// This is only noticable at slow to medium speeds where the user is rapidly trying to pan the camera across the map in
// multiple motions.
//

public class CameraDrag : MonoBehaviour
{
    [SerializeField]
    private Vector2 dragSpeed;

    private Vector3 clickPos;
    private bool isMouseDown = false;

    void Update()
    {
        HandleInput();

        if (isMouseDown)
        {
            Vector3 mousePos = Util.GetMousePositionInWorldSpace();

            // intentionally not normalizing so the user can control the relative speed
            // note the camera will feel "lerp-y" if we use a slow drag speed. At higher speeds it feels better.
            Vector3 direction = mousePos - clickPos; 
            direction.z = 0;

            float dt = Time.deltaTime;
            // Note: We invert movement because we want the feeling of dragging the map below
            transform.position -= new Vector3(direction.x * dragSpeed.x * dt, direction.y * dragSpeed.y * dt, 0f);
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isMouseDown = true;
            clickPos = Util.GetMousePositionInWorldSpace();
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMouseDown = false;
        }
    }
}
