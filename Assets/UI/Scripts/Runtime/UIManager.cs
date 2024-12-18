using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [Header("References Main")]
    [SerializeField] UIDocument gameUI;
    [SerializeField] UIDocument giftsPopUp;
    [SerializeField] UIDocument fortuneWheelPopUp;
    [SerializeField] UIDocument questsPopUp;

    [Header("References Challenge")]
    [SerializeField] UIDocument challengeUI;

    [Header("References Shop")]
    [SerializeField] UIDocument shopUI;

    [SerializeField] UIDocument shopInfo;

    [SerializeField] UIDocument shopInvoke;

    [SerializeField] UIDocument shopInvokeAd;

    [SerializeField] UIDocument shopDisasterTab;
    [SerializeField] UIDocument shopEquipmentTab;
    [SerializeField] UIDocument shopAbilitiesTab;
    [SerializeField] UIDocument shopMarketTab;

    [Header("Input Events Main")]
    public RSE_CallFortuneWheelPopUp callFortuneWheelPopUp;
    public RSE_CallGiftPopUp callGiftPopUp;
    public RSE_CallQuestsPopUp callQuestsPopUp;

    [Header("Input Events Challenge")]
    public RSE_CallTimedPlanet callTimedPlanet;

    [Header("Input Events Shop")]
    public RSE_CallShop callShop;

    public RSE_CallInfo callInfo;

    public RSE_CallInvoke1 callInvoke1;
    public RSE_CallInvoke10 callInvoke10;
    public RSE_CallInvokeAdd callInvokeAdd;

    public RSE_CallDisasterTab callDisasterTab;
    public RSE_CallEquipmentTab callEquipmentTab;
    public RSE_CallAbilitiesTab callAbilitiesTab;
    public RSE_CallMarketTab callMarketTab;

    private int index;

    private void OnEnable()
    {
        callQuestsPopUp.Fire += OnQuestsPopUpCalled;
        callFortuneWheelPopUp.Fire += OnFortuneWheelPopUpCalled;
        callGiftPopUp.Fire += OnGiftPopUpCalled;


        callTimedPlanet.Fire += OnOpenChallenge;


        callShop.Fire += OnOpenShop;

        callInfo.Fire += OnOpenShopInfo;

        callInvoke1.Fire += OnOpenShopInvoke;
        callInvoke10.Fire += OnOpenShopInvoke;
        callInvokeAdd.Fire += OnOpenShopInvokeAdd;

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


        callTimedPlanet.Fire -= OnOpenChallenge;


        callShop.Fire -= OnOpenShop;

        callInfo.Fire -= OnOpenShopInfo;

        callInvoke1.Fire -= OnOpenShopInvoke;
        callInvoke10.Fire -= OnOpenShopInvoke;
        callInvokeAdd.Fire -= OnOpenShopInvokeAdd;

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
    /// Close All Tab
    /// </summary>
    private void CleanTab()
    {
        challengeUI.gameObject.SetActive(false);

        CleanShopTab();
    }

    /// <summary>
    /// Open the Challenge
    /// </summary>
    private void OnOpenChallenge()
    {
        if (index == 3)
        {
            index = 0;

            challengeUI.gameObject.SetActive(false);
        }
        else
        {
            if(index != 0)
            {
                CleanTab();
            }

            index = 3;

            challengeUI.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Open the Shop
    /// </summary>
    private void OnOpenShop()
    {
        if (index == 4)
        {
            index = 0;

            CleanShopTab();
        }
        else
        {
            if (index != 0)
            {
                CleanTab();
            }

            index = 4;

            shopUI.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Open the Shop Info
    /// </summary>
    private void OnOpenShopInfo()
    {
        shopInfo.gameObject.SetActive(true);
    }

    /// <summary>
    /// Open the Shop Invoke
    /// </summary>
    private void OnOpenShopInvoke()
    {
        shopInvoke.gameObject.SetActive(true);
    }

    /// <summary>
    /// Open the Shop Invoke Add
    /// </summary>
    private void OnOpenShopInvokeAdd()
    {
        shopInvokeAd.gameObject.SetActive(true);
    }

    /// <summary>
    /// Close All Tab in the Shop
    /// </summary>
    private void CleanShopTab()
    {
        shopDisasterTab.gameObject.SetActive(false);
        shopEquipmentTab.gameObject.SetActive(false);
        shopAbilitiesTab.gameObject.SetActive(false);
        shopMarketTab.gameObject.SetActive(false);
    }

    /// <summary>
    /// Open the Shop Disasaster Tab
    /// </summary>
    private void OnOpenShopDisasterTab()
    {
        if(!shopDisasterTab.gameObject.activeInHierarchy)
        {
            CleanShopTab();

            shopDisasterTab.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Open the Shop Equipment Tab
    /// </summary>
    private void OnOpenShopEquipmentTab()
    {
        if (!shopEquipmentTab.gameObject.activeInHierarchy)
        {
            CleanShopTab();

            shopEquipmentTab.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Open the Shop Abilities Tab
    /// </summary>
    private void OnOpenShopAbilitiesTab()
    {
        if (!shopAbilitiesTab.gameObject.activeInHierarchy)
        {
            CleanShopTab();

            shopAbilitiesTab.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Open the Shop Market Tab
    /// </summary>
    private void OnOpenShopMarketTab()
    {
        if (!shopMarketTab.gameObject.activeInHierarchy)
        {
            CleanShopTab();

            shopMarketTab.gameObject.SetActive(true);
        }
    }
}