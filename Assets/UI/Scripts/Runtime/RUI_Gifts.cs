using UnityEngine;
using UnityEngine.UIElements;

public class RUI_Gifts : MonoBehaviour
{
    [Header("Input Events")]
    public RSE_ShutGiftPopUp shutGiftPopUp;

    [Header("Output Events")]
    public RSE_CallGiftPopUp callGiftPopUp;

    private UIDocument uiDocument;

    private Button buttonQuit;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();

        buttonQuit = uiDocument.rootVisualElement.Q("Button_Quit") as Button;
        buttonQuit.RegisterCallback<ClickEvent>(ShutPopUp);

        callGiftPopUp.Fire += OpenPopUp;
        shutGiftPopUp.Fire += ShutPopUp;

        ShutPopUp();
    }

    private void OnDisable()
    {
        buttonQuit.UnregisterCallback<ClickEvent>(ShutPopUp);

        callGiftPopUp.Fire -= OpenPopUp;
        shutGiftPopUp.Fire -= ShutPopUp;
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
