using UnityEngine;

namespace cube_destruction_game
{
    public class CubeRaycaster : MonoBehaviour
    {
        private CubeDestructionCoordinator _coordinator;
        private const float SearchDelay = 0.1f;

        private void Start()
        {
            StartCoroutine(FindCoordinator());
        }

        private System.Collections.IEnumerator FindCoordinator()
        {
            yield return new WaitForSeconds(SearchDelay);
            _coordinator = GetComponent<CubeDestructionCoordinator>();

            if (_coordinator == null)
            {
                _coordinator = GetComponentInParent<CubeDestructionCoordinator>();
            }
        }

        private void Update()
        {
            if (_coordinator == null || !Input.GetMouseButtonDown(0)) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Cube cube = hit.collider.GetComponent<Cube>();
                if (cube != null)
                {
                    _coordinator.HandleCubeClick(cube);
                }
            }
        }
    }
}