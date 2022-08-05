using System.Collections;
using UnityEngine;

public class SideTouchEventHandler : MonoBehaviour, ISideTouchedHandler
{
    [SerializeField] private DefaultPlatformForceProvider _platformPrefab;
    [SerializeField] private Material _leftWallMaterial;
    [SerializeField] private Material _rightWallMaterial;
    [SerializeField] private float _blinkSpeed = 1.0f;
    private SideTouchPanelSide _lastTouchedSide;

    private void Start()
    {
        _lastTouchedSide = SideTouchPanelSide.None;
    }

    private IEnumerator AnimateWall(Material material)
    {
        float x = 0.0f;
        while (true)
        {
            material.color = new Color(Mathf.Abs(Mathf.Sin(x)), 1.0f, 1.0f);
            x += Time.deltaTime * _blinkSpeed;
            yield return new WaitForEndOfFrame();
        }
    }

    public void OnSideTouched(Vector3 worldTouchPosition, Vector2 screenTouchPosition, SideTouchPanelSide side)
    {
        if (_lastTouchedSide == side)
        {
            return;
        }
        if (_lastTouchedSide == SideTouchPanelSide.None)
        {
            _lastTouchedSide = side;
        }
        else
        {
            _lastTouchedSide = _lastTouchedSide == SideTouchPanelSide.Left ? SideTouchPanelSide.Right : SideTouchPanelSide.Left;
        }
        switch (_lastTouchedSide)
        {
            case SideTouchPanelSide.Left:
                StopAllCoroutines();
                _leftWallMaterial.color = Color.white;
                StartCoroutine(AnimateWall(_rightWallMaterial));
                break;
            case SideTouchPanelSide.Right:
                StopAllCoroutines();
                _rightWallMaterial.color = Color.white;
                StartCoroutine(AnimateWall(_leftWallMaterial));
                break;
        }
        Instantiate(_platformPrefab, worldTouchPosition, Quaternion.identity, transform);
    }
}
