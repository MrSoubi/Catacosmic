using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

public class RUI_MainGame : MonoBehaviour
{
    [Title("Inputs Events")]
    public RSO_PlayerMoney playerMoney;

    [Title("Output Events")]
    public RSE_CallGiftPopUp callGiftsPopUp;
    public RSE_CallFortuneWheelPopUp callFortuneWheelPopUp;
    public RSE_CallQuestsPopUp callQuestsPopUp;

    public RSE_CallZoom callZoom;
    public RSE_CallDeZoom callDeZoom;

    public RSE_CallUpgrade callUpgrade;
    public RSE_CallInventory callInventory;
    public RSE_CallTimedPlanet callTimedPlanet;
    public RSE_CallShop callShop;

    public RSO_PointerPositionOnScreen pointerPosition;
    public RSE_PointerDown pointerDown;
    public RSE_PointerUp pointerUp;

    private VisualElement root;

    private Button buttonGiftsPopUp;
    private Button buttonFortuneWheelPopUp;
    private Button buttonQuestsPopUp;

    private Button buttonZoom;
    private Button buttonDeZoom;

    private Button buttonUpgrade;
    private Button buttonInventory;
    private Button buttonTimedPlanet;
    private Button buttonShop;

    private const string POINTER_BLOCKER_STYLE_CLASS = "pointerBlocker";

    private Label textMoney;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        root = uiDocument.rootVisualElement;

        root.RegisterCallback<PointerDownEvent>(GetPointerPosition);
        root.RegisterCallback<PointerDownEvent>(TriggerPointerDown);
        root.RegisterCallback<PointerMoveEvent>(GetPointerPosition);
        root.RegisterCallback<PointerUpEvent>(GetPointerPosition);
        root.RegisterCallback<PointerUpEvent>(TriggerPointerUp);

        buttonGiftsPopUp = uiDocument.rootVisualElement.Q("Button_Gifts") as Button;
        buttonFortuneWheelPopUp = uiDocument.rootVisualElement.Q("Button_FortuneWheel") as Button;
        buttonQuestsPopUp = uiDocument.rootVisualElement.Q("Button_Quests") as Button;

        buttonGiftsPopUp.RegisterCallback<ClickEvent>(CallGiftsPopUp);
        buttonFortuneWheelPopUp.RegisterCallback<ClickEvent>(CallFortuneWheelPopUp);
        buttonQuestsPopUp.RegisterCallback<ClickEvent>(CallQuestsPopUp);

        buttonZoom = uiDocument.rootVisualElement.Q("Button_Zoom") as Button;
        buttonDeZoom = uiDocument.rootVisualElement.Q("Button_DeZoom") as Button;

        buttonZoom.RegisterCallback<ClickEvent>(CallZoom);
        buttonDeZoom.RegisterCallback<ClickEvent>(CallDeZoom);

        buttonUpgrade = uiDocument.rootVisualElement.Q("Button_Upgrade") as Button;
        buttonInventory = uiDocument.rootVisualElement.Q("Button_Inventory") as Button;
        buttonTimedPlanet = uiDocument.rootVisualElement.Q("Button_TimedPlanet") as Button;
        buttonShop = uiDocument.rootVisualElement.Q("Button_Shop") as Button;

        buttonUpgrade.RegisterCallback<ClickEvent>(CallButtonUpgrade);
        buttonInventory.RegisterCallback<ClickEvent>(CallButtonInventory);
        buttonTimedPlanet.RegisterCallback<ClickEvent>(CallButtonTimedPlanet);
        buttonShop.RegisterCallback<ClickEvent>(CallButtonShop);

        InitializePointerBlockers(root);

        textMoney = uiDocument.rootVisualElement.Q("Text_Money") as Label;

        playerMoney.onValueChanged += UpdateMoney;
    }

    private void OnDisable()
    {
        root.UnregisterCallback<PointerDownEvent>(GetPointerPosition);
        root.UnregisterCallback<PointerDownEvent>(TriggerPointerDown);
        root.UnregisterCallback<PointerMoveEvent>(GetPointerPosition);
        root.UnregisterCallback<PointerUpEvent>(GetPointerPosition);
        root.UnregisterCallback<PointerUpEvent>(TriggerPointerUp);

        buttonGiftsPopUp.UnregisterCallback<ClickEvent>(CallGiftsPopUp);
        buttonFortuneWheelPopUp.UnregisterCallback<ClickEvent>(CallFortuneWheelPopUp);
        buttonQuestsPopUp.UnregisterCallback<ClickEvent>(CallQuestsPopUp);

        buttonZoom.UnregisterCallback<ClickEvent>(CallZoom);
        buttonDeZoom.UnregisterCallback<ClickEvent>(CallDeZoom);

        buttonUpgrade.UnregisterCallback<ClickEvent>(CallButtonUpgrade);
        buttonInventory.UnregisterCallback<ClickEvent>(CallButtonInventory);
        buttonTimedPlanet.UnregisterCallback<ClickEvent>(CallButtonTimedPlanet);
        buttonShop.UnregisterCallback<ClickEvent>(CallButtonShop);

        DeInitializePointerBlockers(root);

        playerMoney.onValueChanged -= UpdateMoney;
    }

    // Searches recursively all children of the VisualElement and registers the PointerMoveEvent to BlockPointerMovement of each child if it has the pointerBlocker class in its style
    private void InitializePointerBlockers(VisualElement root)
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

    // Searches recursively all children of the VisualElement and unregisters the PointerMoveEvent to BlockPointerMovement of each child if it has the pointerBlocker class in its style
    private void DeInitializePointerBlockers(VisualElement root)
    {
        if (root.hierarchy.childCount != 0)
        {
            foreach (var child in root.Children())
            {
                DeInitializePointerBlockers(child);
            }
        }

        if (root.ClassListContains(POINTER_BLOCKER_STYLE_CLASS))
        {
            root.UnregisterCallback<PointerDownEvent>(BlockPointerMovement);
        }
    }

    private void BlockPointerMovement(PointerDownEvent pointerDownEvent)
    {
        pointerDownEvent.StopPropagation();
    }

    private void TriggerPointerDown(PointerDownEvent pointerDownEvent)
    {
        pointerDown.Fire?.Invoke();
    }

    private void TriggerPointerUp(PointerUpEvent pointerUpEvent)
    {
        pointerUp.Fire?.Invoke();
    }

    private void GetPointerPosition(PointerDownEvent pointerDownEvent)
    {
        pointerPosition.Value = pointerDownEvent.position * root.panel.scaledPixelsPerPoint;
    }

    private void GetPointerPosition(PointerMoveEvent pointerMoveEvent)
    {
        pointerPosition.Value = pointerMoveEvent.position * root.panel.scaledPixelsPerPoint;
    }

    private void GetPointerPosition(PointerUpEvent pointerUpEvent)
    {
        pointerPosition.Value = pointerUpEvent.position * root.panel.scaledPixelsPerPoint;
    }

    private void CallGiftsPopUp(ClickEvent clickEvent)
    {
        callGiftsPopUp.Fire?.Invoke();
    }

    private void CallFortuneWheelPopUp(ClickEvent clickEvent)
    {
        callFortuneWheelPopUp.Fire?.Invoke();
    }

    private void CallQuestsPopUp(ClickEvent clickEvent)
    {
        callQuestsPopUp.Fire?.Invoke();
    }

    private void CallZoom(ClickEvent clickEvent)
    {
        callZoom.Fire?.Invoke();
    }

    private void CallDeZoom(ClickEvent clickEvent)
    {
        callDeZoom.Fire?.Invoke();
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

    private void UpdateMoney(double money)
    {
        textMoney.text = money.ToString();
    }
}
