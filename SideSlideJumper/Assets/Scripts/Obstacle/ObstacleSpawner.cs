using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] _obstaclePrefabs;
    [SerializeField] private Vector3 spawnObstaclesPosition;
    //todo: better get this from config
    [SerializeField] private float _spawnTime = 2.0f;
    [SerializeField] private float _spawnStartTimeout = 5.0f;
    private float _timeSinceLastSpawn = 0.0f;

    private void Start()
    {
        if (_obstaclePrefabs == null || _obstaclePrefabs.Length == 0)
        {
            throw new System.Exception("You must provide at least one obstacle prefab.");
        }
        _timeSinceLastSpawn = _spawnStartTimeout;
    }

    private void Update()
    {
        _timeSinceLastSpawn -= Time.deltaTime;
        if (_timeSinceLastSpawn < 0)
        {
            SpawnObstacle();
            _timeSinceLastSpawn = _spawnTime;
        }
    }

    private void SpawnObstacle()
    {
        int indexOfPrefab = Random.Range(0, _obstaclePrefabs.Length);
        Instantiate(_obstaclePrefabs[indexOfPrefab], spawnObstaclesPosition, Quaternion.identity, transform);
    }

    public Obstacle[] GetAllObstacles()
    {
        return _obstaclePrefabs;
    }
}
