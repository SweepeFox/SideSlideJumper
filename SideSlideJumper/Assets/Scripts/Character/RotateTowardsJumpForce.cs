using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotateTowardsJumpForce : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private Rigidbody _characterBody;
    private LastCollisionContactType _lastCollisionType = LastCollisionContactType.None;

    private void Start()
    {
        _characterBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        switch (_lastCollisionType)
        {
            case LastCollisionContactType.Platform:
                var xVelocity = _characterBody.velocity.x;
                if (xVelocity < 0)
                {
                    _characterBody.rotation = Quaternion.Lerp(_characterBody.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * _rotationSpeed);
                } 
                else if (xVelocity > 0)
                {
                    _characterBody.rotation = Quaternion.Lerp(_characterBody.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * _rotationSpeed);
                }
                break;
            case LastCollisionContactType.Wall:
                _characterBody.rotation = Quaternion.Lerp(_characterBody.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * _rotationSpeed);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _lastCollisionType = LastCollisionContactType.Platform;
            return;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            _lastCollisionType = LastCollisionContactType.Wall;
            return;
        }
    }
}

public enum LastCollisionContactType
{
    None,
    Platform,
    Wall
}
