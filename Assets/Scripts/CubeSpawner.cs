using UnityEngine;

namespace cube_destruction_game
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private Cube _cubePrefab;

        public Cube Spawn(Vector3 position, Quaternion rotation)
        {
            return Instantiate(_cubePrefab, position, rotation);
        }

        public Cube[] SpawnMultiple(Vector3[] positions, Quaternion[] rotations)
        {
            if (positions.Length != rotations.Length)
            {
                Debug.LogError("Positions and rotations arrays must have the same length!");
                return new Cube[0];
            }

            Cube[] cubes = new Cube[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                cubes[i] = Spawn(positions[i], rotations[i]);
            }

            return cubes;
        }
    }
}