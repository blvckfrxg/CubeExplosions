using UnityEngine;

public class CubePositionCalculator : MonoBehaviour
{
    private const float DEFAULT_MIN_OFFSET = -3f;
    private const float DEFAULT_MAX_OFFSET = 3f;
    private const float DEFAULT_MIN_HEIGHT = 1f;
    private const float DEFAULT_MAX_HEIGHT = 3f;

    [SerializeField] private float _minOffset = DEFAULT_MIN_OFFSET;
    [SerializeField] private float _maxOffset = DEFAULT_MAX_OFFSET;
    [SerializeField] private float _minHeight = DEFAULT_MIN_HEIGHT;
    [SerializeField] private float _maxHeight = DEFAULT_MAX_HEIGHT;

    public Vector3[] CalculateSplitPositions(Vector3 parentPosition, int count)
    {
        Vector3[] positions = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            positions[i] = parentPosition + GetRandomOffset();
        }

        return positions;
    }

    private Vector3 GetRandomOffset()
    {
        return new Vector3(
            Random.Range(_minOffset, _maxOffset),
            Random.Range(_minHeight, _maxHeight),
            Random.Range(_minOffset, _maxOffset)
        );
    }
}
