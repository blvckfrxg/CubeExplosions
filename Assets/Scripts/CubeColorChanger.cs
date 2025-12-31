using UnityEngine;

namespace cube_destruction_game
{
    public class CubeColorChanger : MonoBehaviour
    {
        public void ApplyRandomColor(Cube cube)
        {
            if (cube == null) return;

            Renderer renderer = cube.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
            }
        }
    }
}