using UnityEngine;
using System.Collections;

namespace cube_destruction_game
{
    public class CubeDestructionCoordinator : MonoBehaviour
    {
        [SerializeField] private CubeSpawner _spawner;
        [SerializeField] private CubeExploder _exploder;

        private const int MinNewCubes = 2;
        private const int MaxNewCubes = 6;
        private const float InitialExplosionDelay = 0.5f;

        private void Start()
        {
            Debug.Log("Game started!");

            Cube initialCube = _spawner.SpawnInitial();

            StartCoroutine(ExplodeInitialCube(initialCube));
        }

        private IEnumerator ExplodeInitialCube(Cube cube)
        {
            yield return new WaitForSeconds(InitialExplosionDelay);
            Debug.Log($"Exploding initial cube at {cube.transform.position}");
            _exploder.ExplodeCube(cube, cube.transform.position + Vector3.up * 2f, 0.5f);
        }

        public void HandleCubeClick(Cube clickedCube)
        {
            if (clickedCube == null) return;

            Debug.Log($"Cube clicked at position: {clickedCube.transform.position}! Split chance: {clickedCube.SplitChance}, Random.value: {Random.value}");

            Vector3 explosionCenter = clickedCube.transform.position;
            float splitChance = clickedCube.SplitChance;

            _exploder.ExplodeCube(clickedCube, explosionCenter, 1.0f);

            if (Random.value <= splitChance)
            {
                Debug.Log($"Cube WILL split! splitChance={splitChance} >= Random.value={Random.value}");

                int newCubeCount = Random.Range(MinNewCubes, MaxNewCubes + 1);
                Debug.Log($"Creating {newCubeCount} new cubes");

                Cube[] newCubes = _spawner.SpawnSplit(
                    explosionCenter,
                    clickedCube.transform.localScale,
                    splitChance,
                    newCubeCount
                );

                _exploder.ExplodeCubes(newCubes, explosionCenter, 1.5f);

                StartCoroutine(DestroyCubeDelayed(clickedCube, 0.1f));
            }
            else
            {
                Debug.Log($"Cube will NOT split! splitChance={splitChance} < Random.value={Random.value}");
                StartCoroutine(DestroyCubeDelayed(clickedCube, 1.0f));
            }
        }

        private IEnumerator DestroyCubeDelayed(Cube cube, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (cube != null && cube.gameObject != null)
            {
                Debug.Log($"Destroying cube after {delay} seconds");
                Destroy(cube.gameObject);
            }
        }
    }
}