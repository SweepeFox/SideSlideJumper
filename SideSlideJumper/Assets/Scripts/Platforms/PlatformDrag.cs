using UnityEngine;

public class PlatformDrag : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _draggable;

    private void Start()
    {
        _draggable = true;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _draggable = false;
        }
        if (!_draggable)
        {
            return;
        }
        var worldMousePosition = -_mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.transform.position.z));
        worldMousePosition.y = transform.position.y;
        worldMousePosition.z = 0;
        transform.position = worldMousePosition;
    }
}
