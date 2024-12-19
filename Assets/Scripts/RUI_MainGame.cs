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

    public RSO_PointerPositionOnScreen pointerPosition;
    public RSE_PointerDown pointerDown;
    public RSE_PointerUp pointerUp;

    private VisualElement root;

    private Button buttonUpgrade;
    private Button buttonInventory;
    private Button buttonTimedPlanet;
    private Button buttonShop;

    private const string POINTER_BLOCKER_STYLE_CLASS = "pointerBlocker";

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        root = uiDocument.rootVisualElement;

        root.RegisterCallback<PointerDownEvent>(GetPointerPosition);
        root.RegisterCallback<PointerDownEvent>(TriggerPointerDown);
        root.RegisterCallback<PointerMoveEvent>(GetPointerPosition);
        root.RegisterCallback<PointerUpEvent>(TriggerPointerUp);

        buttonUpgrade = uiDocument.rootVisualElement.Q("UpgradePanelButton") as Button;
        buttonInventory = uiDocument.rootVisualElement.Q("InventoryPanelButton") as Button;
        buttonTimedPlanet = uiDocument.rootVisualElement.Q("TimedPlanetPanelButton") as Button;
        buttonShop = uiDocument.rootVisualElement.Q("ShopPanelButton") as Button;

        buttonUpgrade.RegisterCallback<ClickEvent>(CallButtonUpgrade);
        buttonInventory.RegisterCallback<ClickEvent>(CallButtonInventory);
        buttonTimedPlanet.RegisterCallback<ClickEvent>(CallButtonTimedPlanet);
        buttonShop.RegisterCallback<ClickEvent>(CallButtonShop);

        InitializePointerBlockers(root);
    }

    private void OnDisable()
    {
        root.UnregisterCallback<PointerDownEvent>(GetPointerPosition);

        buttonUpgrade.UnregisterCallback<ClickEvent>(CallButtonUpgrade);
        buttonInventory.UnregisterCallback<ClickEvent>(CallButtonInventory);
        buttonTimedPlanet.UnregisterCallback<ClickEvent>(CallButtonTimedPlanet);
        buttonShop.UnregisterCallback<ClickEvent>(CallButtonShop);
    }

    // TODO: Make the same thing but for unregistering
    // Searches recursively all children of the VisualElement and registers the PointerMoveEvent to BlockPointerMovement of each child if it has the pointerBlocker class in its style
    void InitializePointerBlockers(VisualElement root)
    {
        if (root.hierarchy.childCount != 0)
        {
            foreach (var child in root.Children())
            {
                InitializePointerBlockers(child);
            }
        }

        if (root.ClassListContains(POINTER_BLOCKER_STYLE_CLASS))
        {
            root.RegisterCallback<PointerDownEvent>(BlockPointerMovement);
        }
    }

    void BlockPointerMovement(PointerDownEvent pointerDownEvent)
    {
        pointerDownEvent.StopPropagation();
    }

    void TriggerPointerDown(PointerDownEvent pointerDownEvent)
    {
        pointerDown.Fire?.Invoke();
    }

    void TriggerPointerUp(PointerUpEvent pointerUpEvent)
    {
        pointerUp.Fire?.Invoke();
    }

    void GetPointerPosition(PointerDownEvent pointerDownEvent)
    {
        pointerPosition.Value = pointerDownEvent.position * root.panel.scaledPixelsPerPoint;
    }

    void GetPointerPosition(PointerMoveEvent pointerMoveEvent)
    {
        pointerPosition.Value = pointerMoveEvent.position * root.panel.scaledPixelsPerPoint;
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
