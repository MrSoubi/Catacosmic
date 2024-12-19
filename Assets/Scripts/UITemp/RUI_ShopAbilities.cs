using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

public class RUI_ShopAbilities : MonoBehaviour
{
    [Title("Output Events")]
    public RSE_CallInfo callInfo;

    public RSE_CallInvoke1 callInvoke1;
    public RSE_CallInvoke10 callInvoke10;
    public RSE_CallInvokeAdd callInvokeAdd;

    public RSE_CallDisasterTab callDisasterTab;
    public RSE_CallEquipmentTab callEquipmentTab;
    public RSE_CallAbilitiesTab callAbilitiesTab;
    public RSE_CallMarketTab callMarketTab;

    private Button buttonInfo;

    private Button button1Invoke;
    private Button button10Invoke;
    private Button buttonAd;

    private Button buttonDisasterTab;
    private Button buttonEquipementTab;
    private Button buttonAbilitiesTab;
    private Button buttonMarketTab;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        buttonInfo = uiDocument.rootVisualElement.Q("InfoButton") as Button;

        button1Invoke = uiDocument.rootVisualElement.Q("Invoke1Button") as Button;
        button10Invoke = uiDocument.rootVisualElement.Q("Invoke10Button") as Button;
        buttonAd = uiDocument.rootVisualElement.Q("InvokeAdButton") as Button;

        buttonDisasterTab = uiDocument.rootVisualElement.Q("DisasterTabButton") as Button;
        buttonEquipementTab = uiDocument.rootVisualElement.Q("EquipmentTabButton") as Button;
        buttonAbilitiesTab = uiDocument.rootVisualElement.Q("AbilitiesTabButton") as Button;
        buttonMarketTab = uiDocument.rootVisualElement.Q("MarketTabButton") as Button;

        buttonInfo.RegisterCallback<ClickEvent>(CallButtonInfo);

        button1Invoke.RegisterCallback<ClickEvent>(CallButton1Invoke);
        button10Invoke.RegisterCallback<ClickEvent>(CallButton10Invoke);
        buttonAd.RegisterCallback<ClickEvent>(CallButtonAdInvoke);

        buttonDisasterTab.RegisterCallback<ClickEvent>(CallButtonDisasterTab);
        buttonEquipementTab.RegisterCallback<ClickEvent>(CallButtonEquipmentTab);
        buttonAbilitiesTab.RegisterCallback<ClickEvent>(CallButtonAbilitiesTab);
        buttonMarketTab.RegisterCallback<ClickEvent>(CallButtonMarketTab);
    }

    private void OnDisable()
    {
        buttonInfo.UnregisterCallback<ClickEvent>(CallButtonInfo);

        button1Invoke.UnregisterCallback<ClickEvent>(CallButton1Invoke);
        button10Invoke.UnregisterCallback<ClickEvent>(CallButton10Invoke);
        buttonAd.UnregisterCallback<ClickEvent>(CallButtonAdInvoke);

        buttonDisasterTab.UnregisterCallback<ClickEvent>(CallButtonDisasterTab);
        buttonEquipementTab.UnregisterCallback<ClickEvent>(CallButtonEquipmentTab);
        buttonAbilitiesTab.UnregisterCallback<ClickEvent>(CallButtonAbilitiesTab);
        buttonMarketTab.UnregisterCallback<ClickEvent>(CallButtonMarketTab);
    }

    private void CallButtonInfo(ClickEvent clickEvent)
    {
        callInfo.Fire?.Invoke();
    }

    private void CallButton1Invoke(ClickEvent clickEvent)
    {
        callInvoke1.Fire?.Invoke();
    }

    private void CallButton10Invoke(ClickEvent clickEvent)
    {
        callInvoke10.Fire?.Invoke();
    }

    private void CallButtonAdInvoke(ClickEvent clickEvent)
    {
        callInvokeAdd.Fire?.Invoke();
    }

    private void CallButtonDisasterTab(ClickEvent clickEvent)
    {
        callDisasterTab.Fire?.Invoke();
    }

    private void CallButtonEquipmentTab(ClickEvent clickEvent)
    {
        callEquipmentTab.Fire?.Invoke();
    }

    private void CallButtonAbilitiesTab(ClickEvent clickEvent)
    {
        callAbilitiesTab.Fire?.Invoke();
    }

    private void CallButtonMarketTab(ClickEvent clickEvent)
    {
        callMarketTab.Fire?.Invoke();
    }
}
