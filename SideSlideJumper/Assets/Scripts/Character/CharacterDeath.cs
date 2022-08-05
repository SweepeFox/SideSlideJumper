using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterDeath : MonoBehaviour
{
    [SerializeField] private Vector3 _respawnPosition;
    private Rigidbody _characterBody;
    private Camera _mainCamera;

    private void Start()
    {
        _characterBody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        var y = _mainCamera.WorldToViewportPoint(transform.position).y;
        if (y < 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = _respawnPosition;
        _characterBody.velocity = Vector3.zero;
        _characterBody.angularVelocity = Vector3.zero;
    }
}
