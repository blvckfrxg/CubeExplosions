using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class Cube : MonoBehaviour
{
    private const float ScaleReductionFactor = 0.5f;
    private const float SplitChanceReductionFactor = 0.5f;
    private const float MinScale = 0.1f;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    private float _splitChance;
    private float _scale;
    private Color _originalColor;

    public float SplitChance => _splitChance;
    public float Scale => _scale;
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        ApplyRandomColor();
    }

    public void Initialize(float parentSplitChance, float parentScale)
    {
        _splitChance = parentSplitChance * SplitChanceReductionFactor;
        _scale = Mathf.Max(parentScale * ScaleReductionFactor, MinScale);
        transform.localScale = Vector3.one * _scale;
    }

    public void ApplyForce(Vector3 direction, float force)
    {
        if (_rigidbody != null)
        {
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    private void ApplyRandomColor()
    {
        if (_renderer != null)
        {
            _originalColor = Random.ColorHSV();
            _renderer.material.color = _originalColor;
        }
    }
}
