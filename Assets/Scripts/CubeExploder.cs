using UnityEngine;

namespace cube_destruction_game
{
    public class CubeExploder : MonoBehaviour
    {
        [SerializeField] private float _explosionForce = 500.0f;
        [SerializeField] private float _upwardsModifier = 0.5f;

        public void ExplodeCube(Cube cube, Vector3 explosionCenter, float forceMultiplier = 1.0f)
        {
            if (cube == null) return;

            Vector3 direction = (cube.transform.position - explosionCenter);

            if (direction.magnitude < 0.1f)
            {
                direction = Random.onUnitSphere;
                direction.y = Mathf.Abs(direction.y);
            }
            else
            {
                direction = direction.normalized;
                direction.y += _upwardsModifier;
                direction = direction.normalized;
            }

            float force = _explosionForce * forceMultiplier;
            cube.ApplyForce(direction, force);
        }

        public void ExplodeCubes(Cube[] cubes, Vector3 explosionCenter, float forceMultiplier = 1.0f)
        {
            foreach (Cube cube in cubes)
            {
                ExplodeCube(cube, explosionCenter, forceMultiplier);
            }
        }
    }
}