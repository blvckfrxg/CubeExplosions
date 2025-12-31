using UnityEngine;

public class CubeDestructionCoordinator : MonoBehaviour
{
    private const int MIN_NEW_CUBES = 2;
    private const int MAX_NEW_CUBES = 6;

    private const float INITIAL_SPLIT_CHANCE = 2f;
    private const float INITIAL_FORCE_MULTIPLIER = 0.5f;
    private const float CLICK_FORCE_MULTIPLIER = 1f;
    private const float NEW_CUBES_FORCE_MULTIPLIER = 1.5f;

    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeExploder _exploder;
    [SerializeField] private CubeScaleCalculator _scaleCalculator;
    [SerializeField] private CubePositionCalculator _positionCalculator;
    [SerializeField] private RaycastService _raycastService;

    [SerializeField] private float _initialScale = 2f;
    [SerializeField] private float _initialExplosionDelay = 0.5f;

    private Cube _initialCube;

    private void Awake()
    {
        if (_raycastService == null)
        {
            return;
        }

        _raycastService.OnHitDetected += HandleRaycastHit;
    }

    private void OnDestroy()
    {
        if (_raycastService == null)
        {
            return;
        }

        _raycastService.OnHitDetected -= HandleRaycastHit;
    }

    private void Start()
    {
        SpawnInitialCube();
    }

    private void SpawnInitialCube()
    {
        _initialCube = _spawner.SpawnInitial();

        if (_initialCube == null)
        {
            return;
        }

        _initialCube.Initialize(INITIAL_SPLIT_CHANCE, _initialScale);
        Invoke(nameof(ExplodeInitialCube), _initialExplosionDelay);
    }

    private void ExplodeInitialCube()
    {
        if (_initialCube == null)
        {
            return;
        }

        _exploder.ExplodeCube(
            _initialCube,
            _initialCube.Position + Vector3.up * 2f,
            INITIAL_FORCE_MULTIPLIER
        );
    }

    private void HandleRaycastHit(RaycastHit hit)
    {
        if (!hit.collider.TryGetComponent(out Cube cube))
        {
            return;
        }

        HandleCubeClick(cube);
    }

    private void HandleCubeClick(Cube cube)
    {
        _exploder.ExplodeCube(cube, cube.Position, CLICK_FORCE_MULTIPLIER);

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
        int count = Random.Range(MIN_NEW_CUBES, MAX_NEW_CUBES + 1);
        Vector3[] positions = _positionCalculator.CalculateSplitPositions(parent.Position, count);

        Cube[] cubes = new Cube[count];

        for (int i = 0; i < count; i++)
        {
            cubes[i] = _spawner.Spawn(positions[i]);
            cubes[i].Initialize(
                parent.SplitChance,
                _scaleCalculator.GetNextScale(parent.Scale)
            );
        }

        _exploder.ExplodeCubes(cubes, parent.Position, NEW_CUBES_FORCE_MULTIPLIER);
    }
}
