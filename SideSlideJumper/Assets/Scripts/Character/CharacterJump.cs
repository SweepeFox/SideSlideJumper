using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterJump : MonoBehaviour
{
    private Rigidbody _characterBody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _slowJumpForce;
    private ObstacleSpawner _obstacleSpawner;

    private void Start()
    {
        _characterBody = GetComponent<Rigidbody>();
        _obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        if (_obstacleSpawner == null)
        {
            throw new System.Exception("Cannot find Obstacle spawner.");
        }
        foreach(Obstacle obstacle in _obstacleSpawner.GetAllObstacles())
        {
            obstacle.OnCharacterInteracted += OnObstacleInteracted;
        }
    }

    private void OnCollisionEnter(Collision collided)
    {
        if (collided.gameObject.CompareTag("Platform"))
        {
            var forceProvider = collided.gameObject.GetComponent<DefaultPlatformForceProvider>();
            if (forceProvider == null)
            {
                throw new System.Exception("Can't find DefaultPlatformForceProvider in the Platform");
            }
            var forceVector = forceProvider.GetForceVector(collided.GetContact(0).point);
            _characterBody.velocity = Vector3.zero;
            _characterBody.angularVelocity = Vector3.zero;
            _characterBody.AddForce(forceVector * _jumpForce, ForceMode.Impulse);
        }
    }

    private void OnObstacleInteracted(ObstacleType obstacleType, float obstacleEffectDuration)
    {
        switch (obstacleType)
        {
            case ObstacleType.Stopper:
                StartCoroutine(OnStopperInteracted(obstacleEffectDuration));
                break;
        }
    }

    private IEnumerator OnStopperInteracted(float obstacleEffectDuration)
    {
        var normalJumpForce = _jumpForce;
        _jumpForce = _slowJumpForce;
        yield return new WaitForSeconds(obstacleEffectDuration);
        _jumpForce = normalJumpForce;
    }

    private void OnDestroy()
    {
        foreach (Obstacle obstacle in _obstacleSpawner.GetAllObstacles())
        {
            obstacle.OnCharacterInteracted -= OnObstacleInteracted;
        }
        StopAllCoroutines();
    }
}
