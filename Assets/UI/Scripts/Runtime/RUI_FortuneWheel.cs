using UnityEngine;
using UnityEngine.UIElements;

public class RUI_FortuneWheel : MonoBehaviour
{
    [Header("Input Events")]
    public RSE_ShutFortuneWheelPopUp shutFortuneWheelPopUp;

    [Header("Output Events")]
    public RSE_CallFortuneWheelPopUp callFortuneWheelPopUp;

    private UIDocument uiDocument;

    private Button buttonQuit;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();

        buttonQuit = uiDocument.rootVisualElement.Q("Button_Quit") as Button;
        buttonQuit.RegisterCallback<ClickEvent>(ShutPopUp);

        callFortuneWheelPopUp.Fire += OpenPopUp;
        shutFortuneWheelPopUp.Fire += ShutPopUp;

        ShutPopUp();
    }

    private void OnDisable()
    {
        buttonQuit.UnregisterCallback<ClickEvent>(ShutPopUp);

        callFortuneWheelPopUp.Fire -= OpenPopUp;
        shutFortuneWheelPopUp.Fire -= ShutPopUp;
    }

    void ShutPopUp(ClickEvent clickEvent)
    {
        uiDocument.rootVisualElement.visible = false;
    }

    void ShutPopUp()
    {
        uiDocument.rootVisualElement.visible = false;
    }

    void OpenPopUp()
    {
        uiDocument.rootVisualElement.visible = true;
    }
}
