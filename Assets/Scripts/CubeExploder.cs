using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    private const float DEFAULT_UPWARDS_MODIFIER = 0.5f;
    private const float MIN_DISTANCE_THRESHOLD_SQR = 0.01f;

    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _upwardsModifier = DEFAULT_UPWARDS_MODIFIER;

    public void ExplodeCube(Cube cube, Vector3 explosionCenter, float forceMultiplier = 1f)
    {
        if (cube == null)
        {
            return;
        }

        Vector3 direction = CalculateExplosionDirection(cube.Position, explosionCenter);
        float force = _explosionForce * forceMultiplier;

        cube.ApplyForce(direction, force);
    }

    public void ExplodeCubes(Cube[] cubes, Vector3 explosionCenter, float forceMultiplier = 1f)
    {
        if (cubes == null)
        {
            return;
        }

        foreach (Cube cube in cubes)
        {
            if (cube != null)
            {
                ExplodeCube(cube, explosionCenter, forceMultiplier);
            }
        }
    }

    private Vector3 CalculateExplosionDirection(Vector3 cubePosition, Vector3 explosionCenter)
    {
        Vector3 direction = cubePosition - explosionCenter;
        float distanceSqr = direction.sqrMagnitude;

        if (distanceSqr < MIN_DISTANCE_THRESHOLD_SQR)
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

        return direction;
    }
}
