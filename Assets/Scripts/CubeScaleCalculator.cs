using UnityEngine;

namespace cube_destruction_game
{
    public class CubeScaleCalculator : MonoBehaviour
    {
        [SerializeField] private float _minOffset = -3.0f;
        [SerializeField] private float _maxOffset = 3.0f;
        [SerializeField] private float _minHeight = 1.0f;
        [SerializeField] private float _maxHeight = 3.0f;

        public Vector3[] CalculateSplitPositions(Vector3 parentPosition, int count)
        {
            Vector3[] positions = new Vector3[count];
            Quaternion[] rotations = new Quaternion[count];

            for (int i = 0; i < count; i++)
            {
                Vector3 offset = new Vector3(
                    Random.Range(_minOffset, _maxOffset),
                    Random.Range(_minHeight, _maxHeight),
                    Random.Range(_minOffset, _maxOffset)
                );

                positions[i] = parentPosition + offset;
            }

            return positions;
        }

        public Quaternion[] GetRandomRotations(int count)
        {
            Quaternion[] rotations = new Quaternion[count];

            for (int i = 0; i < count; i++)
            {
                rotations[i] = Random.rotation;
            }

            return rotations;
        }
    }
}