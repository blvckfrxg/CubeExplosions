using UnityEngine;

namespace cube_destruction_game
{
    public class CubeFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _cubePrefab;

        private const float MinimumOffset = -0.5f;
        private const float MaximumOffset = 0.5f;
        private const float ScaleReductionFactor = 0.5f;

        public GameObject[] CreateCubes(Vector3 parentPosition, Vector3 parentScale, float parentSplitChance, int cubeCount)
        {
            GameObject[] newCubes = new GameObject[cubeCount];
            Debug.Log($"Creating {cubeCount} cubes at position {parentPosition} with scale {parentScale}");

            for (int cubeIndex = 0; cubeIndex < cubeCount; cubeIndex++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(MinimumOffset, MaximumOffset),
                    Random.Range(0.1f, 0.3f),
                    Random.Range(MinimumOffset, MaximumOffset)
                );

                Vector3 spawnPosition = parentPosition + randomOffset;
                Debug.Log($"Creating cube {cubeIndex} at {spawnPosition}");

                GameObject newCube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

                CubeController cubeController = newCube.GetComponent<CubeController>();

                if (cubeController != null)
                {
                    Vector3 newScale = parentScale * ScaleReductionFactor;
                    Color randomColor = Random.ColorHSV();
                    cubeController.Initialize(parentSplitChance, newScale, randomColor);
                }
                else
                {
                    Debug.LogError("CubeController not found on new cube!");
                }

                newCubes[cubeIndex] = newCube;
            }

            return newCubes;
        }
    }
}