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

    public RSO_PointerPositionOnScreen pointerPosition;
    public RSE_PointerDown pointerDown;
    public RSE_PointerUp pointerUp;

    [Header("Input Events")]
    public RSO_PlayerMoney playerMoney;

    private VisualElement root;

    private Button buttonGiftsPopUp;
    private Button buttonFortuneWheelPopUp;
    private Button buttonQuestsPopUp;

    private Label moneyLabel;

    const string POINTER_BLOCKER_STYLE_CLASS = "pointerBlocker";

    [SerializeField] VisualTreeAsset m_ListEntryTemplate;

    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        var upgradeListController = new UpgradeListController();
        upgradeListController.InitializeCharacterList(uiDocument.rootVisualElement, m_ListEntryTemplate);

        root = uiDocument.rootVisualElement;

        root.RegisterCallback<PointerDownEvent>(GetPointerPosition);
        root.RegisterCallback<PointerDownEvent>(TriggerPointerDown);
        root.RegisterCallback<PointerMoveEvent>(GetPointerPosition);
        root.RegisterCallback<PointerUpEvent>(TriggerPointerUp);

        buttonGiftsPopUp = uiDocument.rootVisualElement.Q("Button_Gifts") as Button;
        buttonFortuneWheelPopUp = uiDocument.rootVisualElement.Q("Button_FortuneWheel") as Button;
        buttonQuestsPopUp = uiDocument.rootVisualElement.Q("Button_Quests") as Button;

        buttonGiftsPopUp.RegisterCallback<ClickEvent>(CallGiftsPopUp);
        buttonFortuneWheelPopUp.RegisterCallback<ClickEvent>(CallFortuneWheelPopUp);
        buttonQuestsPopUp.RegisterCallback<ClickEvent>(CallQuestsPopUp);

        moneyLabel = uiDocument.rootVisualElement.Q("MoneyValue") as Label;

        InitializePointerBlockers(root);

        playerMoney.onValueChanged += UpdateMoney;
    }

    private void UpdateMoney(double amount)
    {
        moneyLabel.text = amount.ToString();
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

    private void OnDisable()
    {
        root.UnregisterCallback<PointerDownEvent>(GetPointerPosition);

        buttonGiftsPopUp.UnregisterCallback<ClickEvent>(CallGiftsPopUp);
        buttonFortuneWheelPopUp.UnregisterCallback<ClickEvent>(CallFortuneWheelPopUp);
        buttonQuestsPopUp.UnregisterCallback<ClickEvent>(CallQuestsPopUp);

        playerMoney.onValueChanged -= UpdateMoney;
    }

    void BlockPointerMovement(PointerMoveEvent pointerMoveEvent)
    {
        pointerMoveEvent.StopPropagation();
    }

    void BlockPointerMovement(PointerDownEvent pointerDownEvent)
    {
        pointerDownEvent.StopPropagation();
    }

    void BlockPointerMovement(PointerUpEvent pointerUpEvent)
    {
        pointerUpEvent.StopPropagation();
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
