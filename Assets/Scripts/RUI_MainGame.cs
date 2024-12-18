using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

public class RUI_MainGame : MonoBehaviour
{
    [Title("Output Events")]
    public RSE_CallUpgrade callUpgrade;
    public RSE_CallInventory callInventory;
    public RSE_CallTimedPlanet callTimedPlanet;
    public RSE_CallShop callShop;

    private Button buttonUpgrade;
    private Button buttonInventory;
    private Button buttonTimedPlanet;
    private Button buttonShop;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        buttonUpgrade = uiDocument.rootVisualElement.Q("UpgradePanelButton") as Button;
        buttonInventory = uiDocument.rootVisualElement.Q("InventoryPanelButton") as Button;
        buttonTimedPlanet = uiDocument.rootVisualElement.Q("TimedPlanetPanelButton") as Button;
        buttonShop = uiDocument.rootVisualElement.Q("ShopPanelButton") as Button;

        buttonUpgrade.RegisterCallback<ClickEvent>(CallButtonUpgrade);
        buttonInventory.RegisterCallback<ClickEvent>(CallButtonInventory);
        buttonTimedPlanet.RegisterCallback<ClickEvent>(CallButtonTimedPlanet);
        buttonShop.RegisterCallback<ClickEvent>(CallButtonShop);
    }

    private void OnDisable()
    {
        buttonUpgrade.UnregisterCallback<ClickEvent>(CallButtonUpgrade);
        buttonInventory.UnregisterCallback<ClickEvent>(CallButtonInventory);
        buttonTimedPlanet.UnregisterCallback<ClickEvent>(CallButtonTimedPlanet);
        buttonShop.UnregisterCallback<ClickEvent>(CallButtonShop);
    }

    private void CallButtonUpgrade(ClickEvent clickEvent)
    {
        callUpgrade.Fire?.Invoke();
    }

    private void CallButtonInventory(ClickEvent clickEvent)
    {
        callInventory.Fire?.Invoke();
    }

    private void CallButtonTimedPlanet(ClickEvent clickEvent)
    {
        callTimedPlanet.Fire?.Invoke();
    }

    private void CallButtonShop(ClickEvent clickEvent)
    {
        callShop.Fire?.Invoke();
    }
}
