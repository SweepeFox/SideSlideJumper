using UnityEngine;
using UnityEngine.EventSystems;

public class SideTouchPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private SideTouchPanelSide _side;
    [SerializeField] private GameObject[] _sideTouchListeners;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_sideTouchListeners.Length == 0)
        {
            return;
        }
        Vector3 screenTouchPosition = new Vector3(eventData.position.x, eventData.position.y, _mainCamera.transform.position.z);
        Vector3 worldTouchPosition = -_mainCamera.ScreenToWorldPoint(screenTouchPosition);
        worldTouchPosition.z = 0.0f;
        foreach (GameObject target in _sideTouchListeners)
        {
            ExecuteEvents.Execute<ISideTouchedHandler>(target, null, (x, y) => x.OnSideTouched(worldTouchPosition, screenTouchPosition, _side));
        }
    }
}

public interface ISideTouchedHandler : IEventSystemHandler
{
    void OnSideTouched(Vector3 worldTouchPosition, Vector2 screenTouchPosition, SideTouchPanelSide side);
}

public enum SideTouchPanelSide
{
    None,
    Left,
    Right
}