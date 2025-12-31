using UnityEngine;

public class CubeDestructionCoordinator : MonoBehaviour
{
    private const int MinNewCubes = 2;
    private const int MaxNewCubes = 6;

    private const float InitialForceMultiplier = 0.5f;
    private const float ClickForceMultiplier = 1f;
    private const float NewCubesForceMultiplier = 1.5f;

    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeExploder _exploder;
    [SerializeField] private CubeScaleCalculator _scaleCalculator;
    [SerializeField] private CubePositionCalculator _positionCalculator;
    [SerializeField] private RaycastService _raycastService;

    private void Awake()
    {
        _raycastService.OnHitDetected += HandleRaycastHit;
    }

    private void OnDestroy()
    {
        _raycastService.OnHitDetected -= HandleRaycastHit;
    }

    private void Start()
    {
        Cube initialCube = _spawner.SpawnInitial();
        _exploder.ExplodeCube(initialCube, initialCube.Position + Vector3.up * 2f, InitialForceMultiplier);
    }

    private void HandleRaycastHit(RaycastHit hit)
    {
        if (!hit.collider.TryGetComponent(out Cube cube))
            return;

        HandleCubeClick(cube);
    }

    private void HandleCubeClick(Cube cube)
    {
        _exploder.ExplodeCube(cube, cube.Position, ClickForceMultiplier);

        if (Random.value > cube.SplitChance)
        {
            Destroy(cube.gameObject, 1f);
            return;
        }

        SplitCube(cube);
        Destroy(cube.gameObject, 0.1f);
    }

    private void SplitCube(Cube parent)
    {
        int count = Random.Range(MinNewCubes, MaxNewCubes + 1);
        Vector3[] positions = _positionCalculator.CalculateSplitPositions(parent.Position, count);

        Cube[] cubes = new Cube[count];

        for (int i = 0; i < count; i++)
        {
            cubes[i] = _spawner.Spawn(positions[i]);
            cubes[i].Initialize(parent.SplitChance, _scaleCalculator.GetNextScale(parent.Scale));
        }

        _exploder.ExplodeCubes(cubes, parent.Position, NewCubesForceMultiplier);
    }
}
