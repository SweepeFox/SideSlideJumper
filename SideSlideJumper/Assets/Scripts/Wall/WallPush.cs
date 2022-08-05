using UnityEngine;

public class WallPush : MonoBehaviour
{
    [SerializeField] private Vector3 _pushForceDirection;
    [SerializeField] private float _pushForce;
    private Rigidbody _characterBody;
    private Animator _characterAnimator;

    private void Start()
    {
        var characterGO = GameObject.FindGameObjectWithTag("Character");
        if (characterGO == null)
        {
            throw new System.ArgumentException("Cannot find GameObject with tag Character");
        }
        _characterBody = characterGO.GetComponent<Rigidbody>();
        if (_characterBody == null)
        {
            throw new System.ArgumentException("Cannot get Rigidbody from Character");
        }
        _characterAnimator = characterGO.GetComponent<Animator>();
        if (_characterAnimator == null)
        {
            throw new System.ArgumentException("Cannot get Animator from Character");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            _characterBody.AddForce(_pushForceDirection * _pushForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            _characterAnimator.SetTrigger("HangWall");
        }
    }
}
