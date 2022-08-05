using UnityEngine;

public class ThreePartsPlatformForceProvider : DefaultPlatformForceProvider
{
    private void Start()
    {
        if (_forceVectors.Length < 3)
        {
            throw new System.ArgumentException("You need to provide at least three force vectors to work with ThreePartsPlatformForceProvider.");
        }
    }

    public override Vector3 GetForceVector(Vector3 collisionPoint)
    {
        var contactX = transform.worldToLocalMatrix.MultiplyPoint(collisionPoint).x;
        if (contactX > 0.3f)
        {
            return _forceVectors[2];
        }
        if (contactX < -0.3f)
        {
            return _forceVectors[1];
        }
        return _forceVectors[0];
    }
}
