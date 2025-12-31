using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float DefaultSpawnHeight = 5f;
    private const float InitialSplitChance = 2f;
    private const float InitialScale = 2f;

    [SerializeField] private Cube _cubePrefab;

    public Cube Spawn(Vector3 position)
    {
        return Spawn(position, Quaternion.identity);
    }

    public Cube Spawn(Vector3 position, Quaternion rotation)
    {
        if (_cubePrefab == null)
            throw new System.InvalidOperationException("CubeSpawner: Cube prefab is not assigned!");

        Cube cube = Instantiate(_cubePrefab, position, rotation);
        cube.Initialize(InitialSplitChance, InitialScale);
        return cube;
    }

    public Cube SpawnInitial()
    {
        Vector3 spawnPosition = new Vector3(0f, DefaultSpawnHeight, 0f);
        return Spawn(spawnPosition);
    }
}
