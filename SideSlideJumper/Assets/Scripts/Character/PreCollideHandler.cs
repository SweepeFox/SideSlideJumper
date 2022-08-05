using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PreCollideHandler : MonoBehaviour
{
    private Animator characterAnimator;

    private void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            characterAnimator.SetTrigger("Land");
        }
    }
}
