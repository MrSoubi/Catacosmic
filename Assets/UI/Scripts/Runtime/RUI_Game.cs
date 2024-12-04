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

    public RSO_PointerPosition pointerPosition;
    public RSE_PointerDown pointerDown;

    private VisualElement root;

    private Button buttonGiftsPopUp;
    private Button buttonFortuneWheelPopUp;
    private Button buttonQuestsPopUp;

    public StyleSheet pointerBlocker;

    [SerializeField] VisualTreeAsset m_ListEntryTemplate;

    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        var upgradeListController = new UpgradeListController();
        upgradeListController.InitializeCharacterList(uiDocument.rootVisualElement, m_ListEntryTemplate);

        root = uiDocument.rootVisualElement;

        root.RegisterCallback<PointerDownEvent>(GetPointerPosition);
        root.RegisterCallback<PointerMoveEvent>(GetPointerPosition);

        buttonGiftsPopUp = uiDocument.rootVisualElement.Q("Button_Gifts") as Button;
        buttonFortuneWheelPopUp = uiDocument.rootVisualElement.Q("Button_FortuneWheel") as Button;
        buttonQuestsPopUp = uiDocument.rootVisualElement.Q("Button_Quests") as Button;

        buttonGiftsPopUp.RegisterCallback<ClickEvent>(CallGiftsPopUp);
        buttonFortuneWheelPopUp.RegisterCallback<ClickEvent>(CallFortuneWheelPopUp);
        buttonQuestsPopUp.RegisterCallback<ClickEvent>(CallQuestsPopUp);

        InitializePointerBlockers(root);
    }

    void InitializePointerBlockers(VisualElement root)
    {
        if (root.hierarchy.childCount != 0)
        {
            foreach (var child in root.Children())
            {
                InitializePointerBlockers(child);
            }
        }

        if (root.ClassListContains("pointerBlocker"))
        {
            root.RegisterCallback<PointerMoveEvent>(BlockPointerMovement);
        }
    }

    private void OnDisable()
    {
        root.UnregisterCallback<PointerDownEvent>(GetPointerPosition);
        root.UnregisterCallback<PointerMoveEvent>(GetPointerPosition);

        buttonGiftsPopUp.UnregisterCallback<ClickEvent>(CallGiftsPopUp);
        buttonFortuneWheelPopUp.UnregisterCallback<ClickEvent>(CallFortuneWheelPopUp);
        buttonQuestsPopUp.UnregisterCallback<ClickEvent>(CallQuestsPopUp);
    }

    void BlockPointerMovement(PointerMoveEvent pointerMoveEvent)
    {
        pointerMoveEvent.StopPropagation();
    }

    void GetPointerPosition(PointerDownEvent pointerDownEvent)
    {
        pointerPosition.Value = pointerDownEvent.position;
        pointerDown.Fire?.Invoke();
    }

    void GetPointerPosition(PointerMoveEvent pointerMoveEvent)
    {
        pointerPosition.Value = pointerMoveEvent.position;
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
