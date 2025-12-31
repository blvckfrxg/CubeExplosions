using UnityEngine;

namespace cube_destruction_game
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Renderer _renderer;

        private float _splitChance;
        private float _scale;
        private bool _isClicked;

        public float SplitChance => _splitChance;
        public float Scale => _scale;

        private void Awake()
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
            if (_renderer != null) _renderer.material.color = Random.ColorHSV();
        }

        public void Initialize(float parentSplitChance, float parentScale)
        {
            const float ScaleReductionFactor = 0.5f;
            const float SplitChanceReductionFactor = 0.5f;

            _splitChance = parentSplitChance * SplitChanceReductionFactor;
            _scale = parentScale * ScaleReductionFactor;
            transform.localScale = Vector3.one * _scale;
        }

        public void ApplyForce(Vector3 direction, float force)
        {
            if (_rigidbody != null)
            {
                _rigidbody.AddForce(direction * force, ForceMode.Impulse);
            }
        }

        private void OnMouseDown()
        {
            if (_isClicked) return;
            _isClicked = true;
        }
    }
}