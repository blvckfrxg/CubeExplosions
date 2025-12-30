using UnityEngine;

namespace cube_destruction_game
{
    public class CubeRaycaster : MonoBehaviour
    {
        public event System.Action<Cube> OnCubeHit;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Cube cube = hit.collider.GetComponent<Cube>();
                    if (cube != null)
                    {
                        OnCubeHit?.Invoke(cube);
                    }
                }
            }
        }
    }
}