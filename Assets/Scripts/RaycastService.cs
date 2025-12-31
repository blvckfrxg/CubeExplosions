using UnityEngine;

public class RaycastService : MonoBehaviour
{
    private const string MainCameraTag = "MainCamera";

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _interactionLayer;

    public event System.Action<RaycastHit> OnHitDetected;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            FindMainCamera();
        }
    }

    private void FindMainCamera()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            GameObject cameraObject = GameObject.FindWithTag(MainCameraTag);
            if (cameraObject != null)
                _mainCamera = cameraObject.GetComponent<Camera>();
        }
    }

    public void PerformRaycast(Vector2 screenPosition)
    {
        if (_mainCamera == null) return;

        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _interactionLayer))
        {
            OnHitDetected?.Invoke(hit);
        }
    }
}
