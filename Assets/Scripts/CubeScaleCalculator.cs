using UnityEngine;

public class CubeScaleCalculator : MonoBehaviour
{
    private const float MinScale = 0.1f;

    public float GetNextScale(float currentScale)
    {
        return Mathf.Max(currentScale * 0.5f, MinScale);
    }
}
