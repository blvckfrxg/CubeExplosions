using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ClickEvent : UnityEvent<Vector2> { }

public class InputReader : MonoBehaviour
{
    private const KeyCode DEFAULT_INTERACTION_KEY = KeyCode.Mouse0;

    [SerializeField] private KeyCode _interactionKey = DEFAULT_INTERACTION_KEY;
    [SerializeField] private ClickEvent _onClickPerformed;

    private void Update()
    {
        if (Input.GetKeyDown(_interactionKey))
        {
            _onClickPerformed?.Invoke(Input.mousePosition);
        }
    }
}
