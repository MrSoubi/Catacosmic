using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

public class RUI_Quests : MonoBehaviour
{
    [Title("Output Events")]
    public RSE_ShutQuestsPopUp shutQuestsPopUp;

    private Button buttonBack;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        buttonBack = uiDocument.rootVisualElement.Q("Button_Back") as Button;

        buttonBack.RegisterCallback<ClickEvent>(CallButtonShut);
    }

    private void OnDisable()
    {
        buttonBack.UnregisterCallback<ClickEvent>(CallButtonShut);
    }

    private void CallButtonShut(ClickEvent clickEvent)
    {
        shutQuestsPopUp.Fire?.Invoke();
    }
}
