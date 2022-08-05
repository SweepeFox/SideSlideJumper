using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StopperMovement : Obstacle
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _xCoord;
    [SerializeField] private float _minYCoord;
    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        transform.position += new Vector3(_xCoord, 0, 0);
    }

    private void Update()
    {
        transform.position = transform.position + Vector3.down * _movementSpeed * Time.deltaTime;
        if (transform.position.y < _minYCoord)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            CharacterInteracted();
            _collider.enabled = false;
            _movementSpeed *= 2;
        }
    }
}
