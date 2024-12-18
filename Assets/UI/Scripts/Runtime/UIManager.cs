using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [Header("References Main")]
    [SerializeField] UIDocument gameUI;
    [SerializeField] UIDocument giftsPopUp;
    [SerializeField] UIDocument fortuneWheelPopUp;
    [SerializeField] UIDocument questsPopUp;

    [Header("References Shop")]
    [SerializeField] UIDocument shopUI;

    [SerializeField] UIDocument shopInfo;

    [SerializeField] UIDocument shopInvoke1;
    [SerializeField] UIDocument shopInvoke10;
    [SerializeField] UIDocument shopInvokeAd;

    [SerializeField] UIDocument shopDisasterTab;
    [SerializeField] UIDocument shopEquipmentTab;
    [SerializeField] UIDocument shopAbilitiesTab;
    [SerializeField] UIDocument shopMarketTab;

    [Header("Input Events")]
    public RSE_CallFortuneWheelPopUp callFortuneWheelPopUp;
    public RSE_CallGiftPopUp callGiftPopUp;
    public RSE_CallQuestsPopUp callQuestsPopUp;


    public RSE_CallShop callShop;

    public RSE_CallInfo callInfo;

    public RSE_CallInvoke1 callInvoke1;
    public RSE_CallInvoke10 callInvoke10;
    public RSE_CallInvokeAdd callInvokeAdd;

    public RSE_CallDisasterTab callDisasterTab;
    public RSE_CallEquipmentTab callEquipmentTab;
    public RSE_CallAbilitiesTab callAbilitiesTab;
    public RSE_CallMarketTab callMarketTab;

    private void OnEnable()
    {
        callQuestsPopUp.Fire += OnQuestsPopUpCalled;
        callFortuneWheelPopUp.Fire += OnFortuneWheelPopUpCalled;
        callGiftPopUp.Fire += OnGiftPopUpCalled;


        callShop.Fire += OnOpenShop;

        callDisasterTab.Fire += OnOpenShopDisasterTab;
        callEquipmentTab.Fire += OnOpenShopEquipmentTab;
        callAbilitiesTab.Fire += OnOpenShopAbilitiesTab;
        callMarketTab.Fire += OnOpenShopMarketTab;
    }

    private void OnDisable()
    {
        callQuestsPopUp.Fire -= OnQuestsPopUpCalled;
        callFortuneWheelPopUp.Fire -= OnFortuneWheelPopUpCalled;
        callGiftPopUp.Fire -= OnGiftPopUpCalled;


        callShop.Fire -= OnOpenShop;

        callDisasterTab.Fire -= OnOpenShopDisasterTab;
        callEquipmentTab.Fire -= OnOpenShopEquipmentTab;
        callAbilitiesTab.Fire -= OnOpenShopAbilitiesTab;
        callMarketTab.Fire -= OnOpenShopMarketTab;
    }

    void OnGiftPopUpCalled()
    {
        giftsPopUp.enabled = true;
    }

    void OnFortuneWheelPopUpCalled()
    {
        fortuneWheelPopUp.enabled = true;
    }

    void OnQuestsPopUpCalled()
    {
        questsPopUp.enabled = true;
    }

    /// <summary>
    /// Close All Tab in the Shop
    /// </summary>
    private void CleanShopTab()
    {
        shopDisasterTab.enabled = false;
        shopEquipmentTab.enabled = false;
        shopAbilitiesTab.enabled = false;
        shopMarketTab.enabled = false;
    }

    /// <summary>
    /// Open the Shop
    /// </summary>
    private void OnOpenShop()
    {
        if(!shopUI.enabled)
        {
            shopUI.enabled = true;
        }
        else
        {
            shopUI.enabled = false;
        }
    }

    /// <summary>
    /// Open the Shop Info
    /// </summary>
    private void OnOpenShopInfo()
    {
        
    }

    /// <summary>
    /// Open the Shop Invoke 1
    /// </summary>
    private void OnOpenShopInvoke1()
    {

    }

    /// <summary>
    /// Open the Shop Invoke 10
    /// </summary>
    private void OnOpenShopInvoke10()
    {

    }

    /// <summary>
    /// Open the Shop Invoke Add
    /// </summary>
    private void OnOpenShopInvokeAdd()
    {

    }

    /// <summary>
    /// Open the Shop Disasaster Tab
    /// </summary>
    private void OnOpenShopDisasterTab()
    {
        CleanShopTab();

        shopDisasterTab.enabled = true;
    }

    /// <summary>
    /// Open the Shop Equipment Tab
    /// </summary>
    private void OnOpenShopEquipmentTab()
    {
        CleanShopTab();

        shopEquipmentTab.enabled = true;
    }

    /// <summary>
    /// Open the Shop Abilities Tab
    /// </summary>
    private void OnOpenShopAbilitiesTab()
    {
        CleanShopTab();

        shopAbilitiesTab.enabled = true;
    }

    /// <summary>
    /// Open the Shop Market Tab
    /// </summary>
    private void OnOpenShopMarketTab()
    {
        CleanShopTab();

        shopMarketTab.enabled = true;
    }
}