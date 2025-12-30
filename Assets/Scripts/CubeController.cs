using UnityEngine;

namespace cube_destruction_game
{
    public class CubeController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Renderer _renderer;
        private bool _isClicked;
        private float _splitChance;

        private const float ScaleReductionFactor = 0.5f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            if (_rigidbody != null)
            {
                _rigidbody.useGravity = true;
            }
        }

        public void Initialize(float parentSplitChance, Vector3 scale, Color color)
        {
            _splitChance = parentSplitChance * ScaleReductionFactor;
            Debug.Log($"Cube initialized with splitChance: {_splitChance}, scale: {scale}");
            transform.localScale = scale;

            if (_renderer != null)
            {
                _renderer.material.color = color;
            }
        }

        private void OnMouseDown()
        {
            if (_isClicked)
            {
                return;
            }

            _isClicked = true;
            Debug.Log($"Cube clicked! Split chance: {_splitChance}");

            CubeManager cubeManager = FindFirstObjectByType<CubeManager>();

            if (cubeManager != null)
            {
                cubeManager.HandleCubeClick(this);
            }
        }

        public float GetSplitChance()
        {
            return _splitChance;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void ApplyExplosionForce(Vector3 explosionCenter, float force)
        {
            if (_rigidbody != null)
            {
                Vector3 direction = (transform.position - explosionCenter).normalized;
                _rigidbody.AddForce(direction * force, ForceMode.Impulse);
                Debug.Log($"Applied explosion force: {force} in direction: {direction}");
            }
        }
    }
}