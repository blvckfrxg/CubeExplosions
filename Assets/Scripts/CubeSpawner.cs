using UnityEngine;

namespace cube_destruction_game
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private float _spawnHeight = 5f;

        private CubeDestructionCoordinator _coordinator;

        private const float InitialSplitChance = 2.0f;
        private const float MinOffset = -3.0f;
        private const float MaxOffset = 3.0f;
        private const float ScaleReductionFactor = 0.5f;

        private void Awake()
        {
            _coordinator = FindAnyObjectByType<CubeDestructionCoordinator>();

            if (_coordinator == null)
            {
                Debug.LogError("CubeDestructionCoordinator not found!");
            }
        }

        public Cube SpawnInitial()
        {
            Vector3 spawnPosition = new Vector3(0, _spawnHeight, 0);
            Cube cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
            cube.Initialize(InitialSplitChance, Vector3.one * 1.0f, Random.ColorHSV(), _coordinator);

            Debug.Log($"Spawned initial cube at {spawnPosition}");
            return cube;
        }

        public Cube[] SpawnSplit(Vector3 parentPosition, Vector3 parentScale, float parentSplitChance, int count)
        {
            Cube[] newCubes = new Cube[count];

            Debug.Log($"Spawning {count} cubes at {parentPosition}");

            for (int i = 0; i < count; i++)
            {
                Vector3 offset = new Vector3(
                    Random.Range(MinOffset, MaxOffset),
                    Random.Range(1.0f, 3.0f),
                    Random.Range(MinOffset, MaxOffset)
                );

                Vector3 spawnPosition = parentPosition + offset;
                Cube cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

                Vector3 newScale = parentScale * ScaleReductionFactor;
                Color randomColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
                cube.Initialize(parentSplitChance, newScale, randomColor, _coordinator);

                newCubes[i] = cube;
                Debug.Log($"Created cube {i} at {spawnPosition}, scale: {newScale}");
            }

            return newCubes;
        }
    }
}