using UnityEngine;
using System.Collections;

namespace cube_destruction_game
{
    public class CubeDestructionCoordinator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private CubeSpawner _spawner;
        [SerializeField] private CubeExploder _exploder;
        [SerializeField] private CubeColorChanger _colorChanger;
        [SerializeField] private CubeScaleCalculator _scaleCalculator;

        [Header("Game Settings")]
        [SerializeField] private float _initialSpawnHeight = 5f;
        [SerializeField] private int _minNewCubes = 2;
        [SerializeField] private int _maxNewCubes = 6;
        [SerializeField] private float _initialExplosionDelay = 0.5f;

        [Header("Force Settings")]
        [SerializeField] private float _initialCubeForceMultiplier = 0.5f;
        [SerializeField] private float _clickedCubeForceMultiplier = 1.0f;
        [SerializeField] private float _newCubesForceMultiplier = 1.5f;
        [SerializeField] private float _splitDelay = 0.1f;
        [SerializeField] private float _noSplitDelay = 1.0f;

        private const float InitialSplitChance = 2.0f;
        private const float InitialScale = 2.0f;

        private void Start()
        {
            SpawnInitialCube();
        }

        private void SpawnInitialCube()
        {
            Vector3 spawnPosition = new Vector3(0, _initialSpawnHeight, 0);
            Cube cube = _spawner.Spawn(spawnPosition, Quaternion.identity);

            cube.Initialize(InitialSplitChance, InitialScale);
            _colorChanger.ApplyRandomColor(cube);

            StartCoroutine(ExplodeInitialCube(cube));
        }

        private IEnumerator ExplodeInitialCube(Cube cube)
        {
            yield return new WaitForSeconds(_initialExplosionDelay);
            _exploder.ExplodeCube(cube, cube.transform.position + Vector3.up * 2f, _initialCubeForceMultiplier);
        }

        public void HandleCubeClick(Cube clickedCube)
        {
            if (clickedCube == null) return;

            Vector3 explosionCenter = clickedCube.transform.position;
            float splitChance = clickedCube.SplitChance;

            _exploder.ExplodeCube(clickedCube, explosionCenter, _clickedCubeForceMultiplier);

            if (Random.value <= splitChance)
            {
                SplitCube(clickedCube, explosionCenter);
                StartCoroutine(DestroyCubeDelayed(clickedCube, _splitDelay));
            }
            else
            {
                StartCoroutine(DestroyCubeDelayed(clickedCube, _noSplitDelay));
            }
        }

        private void SplitCube(Cube parentCube, Vector3 explosionCenter)
        {
            int newCubeCount = Random.Range(_minNewCubes, _maxNewCubes + 1);

            Vector3[] positions = _scaleCalculator.CalculateSplitPositions(explosionCenter, newCubeCount);
            Quaternion[] rotations = _scaleCalculator.GetRandomRotations(newCubeCount);

            Cube[] newCubes = _spawner.SpawnMultiple(positions, rotations);

            foreach (Cube cube in newCubes)
            {
                cube.Initialize(parentCube.SplitChance, parentCube.Scale);
                _colorChanger.ApplyRandomColor(cube);
            }

            _exploder.ExplodeCubes(newCubes, explosionCenter, _newCubesForceMultiplier);
        }

        private IEnumerator DestroyCubeDelayed(Cube cube, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (cube != null && cube.gameObject != null)
            {
                Destroy(cube.gameObject);
            }
        }
    }
}