using UnityEngine;

public class CubeScaleCalculator : MonoBehaviour
{
    private const float MIN_SCALE = 0.1f;

    public float GetNextScale(float currentScale)
    {
        return Mathf.Max(currentScale * 0.5f, MIN_SCALE);
    }
}
