using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

public class RUI_Gifts : MonoBehaviour
{
    [Title("Output Events")]
    public RSE_ShutGiftPopUp shutGiftsPopUp;

    private Button buttonDayGift1;
    private Button buttonDayGift2;
    private Button buttonDayGift3;
    private Button buttonDayGift4;
    private Button buttonDayGift5;
    private Button buttonDayGift6;
    private Button buttonDayGift7;

    private Button buttonConsecutifGift1;
    private Button buttonConsecutifGift2;
    private Button buttonConsecutifGift3;
    private Button buttonConsecutifGift4;
    private Button buttonConsecutifGift5;
    private Button buttonConsecutifGift6;
    private Button buttonConsecutifGift7;

    private Button buttonBack;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        buttonDayGift1 = uiDocument.rootVisualElement.Q("Button_DayGift1") as Button;
        buttonDayGift2 = uiDocument.rootVisualElement.Q("Button_DayGift2") as Button;
        buttonDayGift3 = uiDocument.rootVisualElement.Q("Button_DayGift3") as Button;
        buttonDayGift4 = uiDocument.rootVisualElement.Q("Button_DayGift4") as Button;
        buttonDayGift5 = uiDocument.rootVisualElement.Q("Button_DayGift5") as Button;
        buttonDayGift6 = uiDocument.rootVisualElement.Q("Button_DayGift6") as Button;
        buttonDayGift7 = uiDocument.rootVisualElement.Q("Button_DayGift7") as Button;

        buttonDayGift1.RegisterCallback<ClickEvent>(CallDayGift1);
        buttonDayGift2.RegisterCallback<ClickEvent>(CallDayGift2);
        buttonDayGift3.RegisterCallback<ClickEvent>(CallDayGift3);
        buttonDayGift4.RegisterCallback<ClickEvent>(CallDayGift4);
        buttonDayGift5.RegisterCallback<ClickEvent>(CallDayGift5);
        buttonDayGift6.RegisterCallback<ClickEvent>(CallDayGift6);
        buttonDayGift7.RegisterCallback<ClickEvent>(CallDayGift7);

        buttonConsecutifGift1 = uiDocument.rootVisualElement.Q("Button_ConsecutifGift1") as Button;
        buttonConsecutifGift2 = uiDocument.rootVisualElement.Q("Button_ConsecutifGift2") as Button;
        buttonConsecutifGift3 = uiDocument.rootVisualElement.Q("Button_ConsecutifGift3") as Button;
        buttonConsecutifGift4 = uiDocument.rootVisualElement.Q("Button_ConsecutifGift4") as Button;
        buttonConsecutifGift5 = uiDocument.rootVisualElement.Q("Button_ConsecutifGift5") as Button;
        buttonConsecutifGift6 = uiDocument.rootVisualElement.Q("Button_ConsecutifGift6") as Button;
        buttonConsecutifGift7 = uiDocument.rootVisualElement.Q("Button_ConsecutifGift7") as Button;

        buttonConsecutifGift1.RegisterCallback<ClickEvent>(CallConsecutifGift1);
        buttonConsecutifGift2.RegisterCallback<ClickEvent>(CallConsecutifGift2);
        buttonConsecutifGift3.RegisterCallback<ClickEvent>(CallConsecutifGift3);
        buttonConsecutifGift4.RegisterCallback<ClickEvent>(CallConsecutifGift4);
        buttonConsecutifGift5.RegisterCallback<ClickEvent>(CallConsecutifGift5);
        buttonConsecutifGift6.RegisterCallback<ClickEvent>(CallConsecutifGift6);
        buttonConsecutifGift7.RegisterCallback<ClickEvent>(CallConsecutifGift7);

        buttonBack = uiDocument.rootVisualElement.Q("Button_Back") as Button;

        buttonBack.RegisterCallback<ClickEvent>(CallButtonShut);
    }

    private void OnDisable()
    {
        buttonDayGift1.UnregisterCallback<ClickEvent>(CallDayGift1);
        buttonDayGift2.UnregisterCallback<ClickEvent>(CallDayGift2);
        buttonDayGift3.UnregisterCallback<ClickEvent>(CallDayGift3);
        buttonDayGift4.UnregisterCallback<ClickEvent>(CallDayGift4);
        buttonDayGift5.UnregisterCallback<ClickEvent>(CallDayGift5);
        buttonDayGift6.UnregisterCallback<ClickEvent>(CallDayGift6);
        buttonDayGift7.UnregisterCallback<ClickEvent>(CallDayGift7);

        buttonConsecutifGift1.UnregisterCallback<ClickEvent>(CallConsecutifGift1);
        buttonConsecutifGift2.UnregisterCallback<ClickEvent>(CallConsecutifGift2);
        buttonConsecutifGift3.UnregisterCallback<ClickEvent>(CallConsecutifGift3);
        buttonConsecutifGift4.UnregisterCallback<ClickEvent>(CallConsecutifGift4);
        buttonConsecutifGift5.UnregisterCallback<ClickEvent>(CallConsecutifGift5);
        buttonConsecutifGift6.UnregisterCallback<ClickEvent>(CallConsecutifGift6);
        buttonConsecutifGift7.UnregisterCallback<ClickEvent>(CallConsecutifGift7);

        buttonBack.UnregisterCallback<ClickEvent>(CallButtonShut);
    }

    private void CallDayGift1(ClickEvent clickEvent)
    {
        Debug.Log("Day 1");
    }

    private void CallDayGift2(ClickEvent clickEvent)
    {
        Debug.Log("Day 2");
    }

    private void CallDayGift3(ClickEvent clickEvent)
    {
        Debug.Log("Day 3");
    }

    private void CallDayGift4(ClickEvent clickEvent)
    {
        Debug.Log("Day 4");
    }

    private void CallDayGift5(ClickEvent clickEvent)
    {
        Debug.Log("Day 5");
    }

    private void CallDayGift6(ClickEvent clickEvent)
    {
        Debug.Log("Day 6");
    }

    private void CallDayGift7(ClickEvent clickEvent)
    {
        Debug.Log("Day 7");
    }

    private void CallConsecutifGift1(ClickEvent clickEvent)
    {
        Debug.Log("Day 1");
    }

    private void CallConsecutifGift2(ClickEvent clickEvent)
    {
        Debug.Log("Day 2");
    }

    private void CallConsecutifGift3(ClickEvent clickEvent)
    {
        Debug.Log("Day 3");
    }

    private void CallConsecutifGift4(ClickEvent clickEvent)
    {
        Debug.Log("Day 4");
    }

    private void CallConsecutifGift5(ClickEvent clickEvent)
    {
        Debug.Log("Day 5");
    }

    private void CallConsecutifGift6(ClickEvent clickEvent)
    {
        Debug.Log("Day 6");
    }

    private void CallConsecutifGift7(ClickEvent clickEvent)
    {
        Debug.Log("Day 7");
    }

    private void CallButtonShut(ClickEvent clickEvent)
    {
        shutGiftsPopUp.Fire?.Invoke();
    }
}
