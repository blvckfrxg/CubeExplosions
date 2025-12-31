using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class Cube : MonoBehaviour
{
    private const float SCALE_REDUCTION_FACTOR = 0.5f;
    private const float SPLIT_CHANCE_REDUCTION_FACTOR = 0.5f;
    private const float MIN_SCALE = 0.1f;

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
        _splitChance = parentSplitChance * SPLIT_CHANCE_REDUCTION_FACTOR;
        _scale = parentScale * SCALE_REDUCTION_FACTOR;

        if (_scale < MIN_SCALE)
        {
            _scale = MIN_SCALE;
        }

        transform.localScale = Vector3.one * _scale;
    }

    public void ApplyForce(Vector3 direction, float force)
    {
        if (_rigidbody == null)
        {
            return;
        }

        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }

    private void ApplyRandomColor()
    {
        if (_renderer == null)
        {
            return;
        }

        _originalColor = Random.ColorHSV();
        _renderer.material.color = _originalColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
