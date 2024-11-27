using UnityEngine;
using UnityEngine.UIElements;

public class RUI_Quests : MonoBehaviour
{
    [Header("Input Events")]
    public RSE_ShutQuestsPopUp shutQuestsPopUp;

    [Header("Input Events")]
    public RSE_CallQuestsPopUp callQuestsPopUp;

    private UIDocument uiDocument;

    private Button buttonQuit;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();

        buttonQuit = uiDocument.rootVisualElement.Q("Button_Quit") as Button;
        buttonQuit.RegisterCallback<ClickEvent>(ShutPopUp);

        callQuestsPopUp.Fire += OpenPopUp;
        shutQuestsPopUp.Fire += ShutPopUp;

        ShutPopUp();
    }

    private void OnDisable()
    {
        buttonQuit.UnregisterCallback<ClickEvent>(ShutPopUp);

        callQuestsPopUp.Fire -= OpenPopUp;
        shutQuestsPopUp.Fire -= ShutPopUp;
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
