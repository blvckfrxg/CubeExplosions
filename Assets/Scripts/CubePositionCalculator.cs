using UnityEngine;

public class CubePositionCalculator : MonoBehaviour
{
    private const float DefaultMinOffset = -3f;
    private const float DefaultMaxOffset = 3f;
    private const float DefaultMinHeight = 1f;
    private const float DefaultMaxHeight = 3f;

    [SerializeField] private float _minOffset = DefaultMinOffset;
    [SerializeField] private float _maxOffset = DefaultMaxOffset;
    [SerializeField] private float _minHeight = DefaultMinHeight;
    [SerializeField] private float _maxHeight = DefaultMaxHeight;

    public Vector3[] CalculateSplitPositions(Vector3 parentPosition, int count)
    {
        Vector3[] positions = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = GetRandomOffset();
            positions[i] = parentPosition + offset;
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
