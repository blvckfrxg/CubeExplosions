using UnityEngine;
using System.Collections;

namespace cube_destruction_game
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Renderer _renderer;

        private float _splitChance;
        private bool _isClicked;
        private CubeDestructionCoordinator _coordinator;
        private Color _originalColor;

        private const float ScaleReductionFactor = 0.5f;

        public float SplitChance => _splitChance;

        private void Awake()
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
            if (_renderer == null) _renderer = GetComponent<Renderer>();
        }

        public void Initialize(float parentSplitChance, Vector3 scale, Color color, CubeDestructionCoordinator coordinator)
        {
            _splitChance = parentSplitChance * ScaleReductionFactor;
            transform.localScale = scale;
            _coordinator = coordinator;

            if (_renderer != null)
            {
                _renderer.material.color = color;
                _originalColor = color;
            }

            Debug.Log($"Cube initialized: splitChance={_splitChance}, scale={scale}");
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

            Debug.Log($"Mouse down on cube at {transform.position}, scale: {transform.localScale}");

            if (_renderer != null)
            {
                StartCoroutine(FlashEffect());
            }

            if (_coordinator != null)
            {
                _coordinator.HandleCubeClick(this);
            }
            else
            {
                Debug.LogError("CubeDestructionCoordinator not found!");
            }
        }

        private IEnumerator FlashEffect()
        {
            if (_renderer != null)
            {
                _renderer.material.color = Color.white;
                yield return new WaitForSeconds(0.1f);
                _renderer.material.color = _originalColor;
            }
        }

        private void OnDestroy()
        {
            Debug.Log($"Cube destroyed at position: {transform.position}, scale: {transform.localScale}");
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Cube at {transform.position} collided with {collision.gameObject.name}");
        }
    }
}