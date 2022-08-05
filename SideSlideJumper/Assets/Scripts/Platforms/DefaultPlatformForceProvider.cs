using UnityEngine;

public class DefaultPlatformForceProvider : MonoBehaviour
{
    protected Vector3[] _forceVectors = new Vector3[] { Vector3.up };

    public virtual Vector3 GetForceVector(Vector3 collisionPoint)
    {
        return _forceVectors[0];
    }
}
