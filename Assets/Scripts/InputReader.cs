using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ClickEvent : UnityEvent<Vector2> { }

public class InputReader : MonoBehaviour
{
    private const KeyCode DefaultInteractionKey = KeyCode.Mouse0;

    [SerializeField] private KeyCode _interactionKey = DefaultInteractionKey;
    [SerializeField] private ClickEvent _onClickPerformed;

    private void Update()
    {
        if (Input.GetKeyDown(_interactionKey))
        {
            Vector2 screenPosition = Input.mousePosition;
            _onClickPerformed?.Invoke(screenPosition);
        }
    }
}
