using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float DEFAULT_SPAWN_HEIGHT = 5f;

    [SerializeField] private Cube _cubePrefab;

    public Cube Spawn(Vector3 position)
    {
        return Spawn(position, Quaternion.identity);
    }

    public Cube Spawn(Vector3 position, Quaternion rotation)
    {
        if (_cubePrefab == null)
        {
            return null;
        }

        return Instantiate(_cubePrefab, position, rotation);
    }

    public Cube SpawnInitial()
    {
        Vector3 spawnPosition = new Vector3(0f, DEFAULT_SPAWN_HEIGHT, 0f);
        return Spawn(spawnPosition);
    }
}
