using UnityEngine;
using UnityEngine.UIElements;

public class RUI_Game : MonoBehaviour
{
    [Header("Output Events")]
    public RSE_CallGiftPopUp callGiftsPopUp;
    public RSE_CallQuestsPopUp callQuestsPopUp;
    public RSE_CallFortuneWheelPopUp callFortuneWheelPopUp;

    public RSE_ShutGiftPopUp shutGiftsPopUp;
    public RSE_ShutQuestsPopUp shutQuestsPopUp;
    public RSE_ShutFortuneWheelPopUp shutFortuneWheelPopUp;

    private Button buttonGiftsPopUp;
    private Button buttonFortuneWheelPopUp;
    private Button buttonQuestsPopUp;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        buttonGiftsPopUp = uiDocument.rootVisualElement.Q("Button_Gifts") as Button;
        buttonFortuneWheelPopUp = uiDocument.rootVisualElement.Q("Button_FortuneWheel") as Button;
        buttonQuestsPopUp = uiDocument.rootVisualElement.Q("Button_Quests") as Button;

        buttonGiftsPopUp.RegisterCallback<ClickEvent>(CallGiftsPopUp);
        buttonFortuneWheelPopUp.RegisterCallback<ClickEvent>(CallFortuneWheelPopUp);
        buttonQuestsPopUp.RegisterCallback<ClickEvent>(CallQuestsPopUp);
    }

    private void OnDisable()
    {
        buttonGiftsPopUp.UnregisterCallback<ClickEvent>(CallGiftsPopUp);
        buttonFortuneWheelPopUp.UnregisterCallback<ClickEvent>(CallFortuneWheelPopUp);
        buttonQuestsPopUp.UnregisterCallback<ClickEvent>(CallQuestsPopUp);
    }

    void CallGiftsPopUp(ClickEvent clickEvent)
    {
        callGiftsPopUp.Fire?.Invoke();
    }

    void CallFortuneWheelPopUp(ClickEvent clickEvent)
    {
        callFortuneWheelPopUp.Fire?.Invoke();
    }

    void CallQuestsPopUp(ClickEvent clickEvent)
    {
        callQuestsPopUp.Fire?.Invoke();
    }

    void ShutAllPopUps(ClickEvent clickEvent)
    {
        shutGiftsPopUp.Fire?.Invoke();
        shutQuestsPopUp.Fire?.Invoke();
        shutFortuneWheelPopUp.Fire?.Invoke();
    }
}
