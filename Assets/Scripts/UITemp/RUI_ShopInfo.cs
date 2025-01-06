using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

public class RUI_ShopInfo : MonoBehaviour
{
    [Title("Output Events")]
    public RSE_ShutShopInfo shutShopInfo;

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
        shutShopInfo.Fire?.Invoke();
    }
}
