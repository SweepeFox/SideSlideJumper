using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlatformLife : MonoBehaviour
{
    [SerializeField] private float _fallSpeed = 3.0f;
    private Camera mainCamera;
    private Collider _collider;

    private void Start()
    {
        mainCamera = Camera.main;
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            StartCoroutine(StartDeathCycle());
        }
    }

    private IEnumerator StartDeathCycle()
    {
        Destroy(_collider);
        var yPosition = mainCamera.WorldToViewportPoint(transform.position).y;
        var newPosition = transform.position;
        while (yPosition > 0)
        {
            newPosition.y -= Time.deltaTime * _fallSpeed;
            _fallSpeed *= 1.09f;
            transform.position = newPosition;
            yPosition = mainCamera.WorldToViewportPoint(transform.position).y;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
