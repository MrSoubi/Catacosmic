using Sirenix.OdinInspector;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Title("ScriptableObjects")]
    public RSO_PointerPositionOnScreen pointerPositionOnScreen;
    public RSO_PointerWorldPosition pointerWorldPosition;

    private void OnEnable()
    {
        pointerPositionOnScreen.onValueChanged += GetLocalPositionFromScreenPosition;
    }

    private void OnDisable()
    {
        pointerPositionOnScreen.onValueChanged -= GetLocalPositionFromScreenPosition;
    }

    void GetLocalPositionFromScreenPosition(Vector2 screenPosition)
    {
        Vector3 viewportPosition = new Vector3(screenPosition.x / Camera.main.pixelWidth, (Camera.main.pixelHeight - screenPosition.y) / Camera.main.pixelHeight, 1);

        Vector2 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);

        pointerWorldPosition.Value = worldPosition;
    }
}
