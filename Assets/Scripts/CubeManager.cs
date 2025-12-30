using UnityEngine;
using System.Collections.Generic;

namespace cube_destruction_game
{
    public class CubeManager : MonoBehaviour
    {
        [SerializeField] private CubeFactory _cubeFactory;
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private float _explosionForce = 5.0f;

        private List<GameObject> _allCubes = new List<GameObject>();

        private const int MinimumNewCubes = 2;
        private const int MaximumNewCubes = 6;
        private const float InitialSplitChance = 1.0f;

        public void Start()
        {
            CreateInitialCube();
        }

        private void CreateInitialCube()
        {
            if (_cubePrefab != null)
            {
                Vector3 spawnPosition = new Vector3(0, 2, 0);
                GameObject initialCube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
                initialCube.transform.localScale = Vector3.one;

                CubeController controller = initialCube.GetComponent<CubeController>();
                if (controller != null)
                {
                    controller.Initialize(1.0f, Vector3.one, Random.ColorHSV());
                }

                _allCubes.Add(initialCube);
                Debug.Log($"Initial cube created with scale: {Vector3.one}");
            }
            else
            {
                Debug.LogError("Cube Prefab is not assigned in CubeManager!");
            }
        }

        public void HandleCubeClick(CubeController clickedCube)
        {
            Vector3 explosionCenter = clickedCube.GetPosition();
            float splitChance = clickedCube.GetSplitChance();
            Debug.Log($"HandleCubeClick called! splitChance: {splitChance}");

            bool shouldSplit = true;

            if (shouldSplit && _cubeFactory != null)
            {
                int newCubeCount = Random.Range(MinimumNewCubes, MaximumNewCubes + 1);
                Debug.Log($"Creating {newCubeCount} new cubes");

                GameObject[] newCubes = _cubeFactory.CreateCubes(
                    explosionCenter,
                    clickedCube.transform.localScale,
                    splitChance,
                    newCubeCount
                );

                Debug.Log($"Created {newCubes.Length} new cubes");

                foreach (GameObject cube in newCubes)
                {
                    CubeController controller = cube.GetComponent<CubeController>();

                    if (controller != null)
                    {
                        controller.ApplyExplosionForce(explosionCenter, _explosionForce);
                    }

                    _allCubes.Add(cube);
                }
            }
            else
            {
                Debug.Log($"Not splitting. shouldSplit: {shouldSplit}, _cubeFactory: {_cubeFactory != null}");
            }

            _allCubes.Remove(clickedCube.gameObject);
            Destroy(clickedCube.gameObject);
            Debug.Log($"Clicked cube destroyed");
        }
    }
}