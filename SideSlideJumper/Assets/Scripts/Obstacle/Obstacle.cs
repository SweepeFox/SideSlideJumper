using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public event System.Action<ObstacleType, float> OnCharacterInteracted;
    public ObstacleType _currentType;
    public float _effectDuration = 1;

    protected void CharacterInteracted()
    {
        OnCharacterInteracted?.Invoke(_currentType, _effectDuration);
    }
}

public enum ObstacleType
{
    Stopper
}