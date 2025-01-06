using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;
using System;

public class UIManager : MonoBehaviour
{
    [Title("References Main")]
    [SerializeField] UIDocument gameUI;
    [SerializeField] UIDocument giftsPopUp;
    [SerializeField] UIDocument fortuneWheelPopUp;
    [SerializeField] UIDocument questsPopUp;

    [Title("References Upgrades")]
    [SerializeField] UIDocument upgradesUI;

    [Title("References Inventory")]
    [SerializeField] UIDocument inventoryUI;

    [Title("References Challenge")]
    [SerializeField] UIDocument challengeUI;

    [Title("References Shop")]
    [SerializeField] UIDocument shopUI;

    [SerializeField] UIDocument shopInfo;

    [SerializeField] UIDocument shopInvoke;

    [SerializeField] UIDocument shopInvokeAd;

    [SerializeField] UIDocument shopDisasterTab;
    [SerializeField] UIDocument shopEquipmentTab;
    [SerializeField] UIDocument shopAbilitiesTab;
    [SerializeField] UIDocument shopMarketTab;

    [Title("Input Events Main")]
    public RSE_CallGiftPopUp callGiftPopUp;
    public RSE_ShutGiftPopUp shutGiftsPopUp;
    public RSE_CallFortuneWheelPopUp callFortuneWheelPopUp;
    public RSE_ShutFortuneWheelPopUp shutFortuneWheelPopUp;
    public RSE_CallQuestsPopUp callQuestsPopUp;
    public RSE_ShutQuestsPopUp shutQuestsPopUp;

    [Title("Input Events Upgrades")]
    public RSE_CallUpgrade callUpgrade;
    public RSE_ShutUpgrade shutUpgrade;

    [Title("Input Events Inventory")]
    public RSE_CallInventory callInventory;

    [Title("Input Events Challenge")]
    public RSE_CallTimedPlanet callTimedPlanet;

    [Title("Input Events Shop")]
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
        index = -1;

        callGiftPopUp.Fire += OnGiftPopUpCalled;
        shutGiftsPopUp.Fire += OnGiftPopUpShut;
        callFortuneWheelPopUp.Fire += OnFortuneWheelPopUpCalled;
        shutFortuneWheelPopUp.Fire += OnFortuneWheelPopUpShut;
        callQuestsPopUp.Fire += OnQuestsPopUpCalled;
        shutQuestsPopUp.Fire += OnQuestsPopUpShut;


        callUpgrade.Fire += OnOpenUpgrades;
        shutUpgrade.Fire += OnShutUpgrades;


        callInventory.Fire += OnOpenInventory;


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
        callGiftPopUp.Fire -= OnGiftPopUpCalled;
        shutGiftsPopUp.Fire -= OnGiftPopUpShut;
        callFortuneWheelPopUp.Fire -= OnFortuneWheelPopUpCalled;
        shutFortuneWheelPopUp.Fire -= OnFortuneWheelPopUpShut;
        callQuestsPopUp.Fire -= OnQuestsPopUpCalled;
        shutQuestsPopUp.Fire -= OnQuestsPopUpShut;


        callUpgrade.Fire -= OnOpenUpgrades;
        shutUpgrade.Fire -= OnShutUpgrades;


        callInventory.Fire -= OnOpenInventory;


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

    /// <summary>
    /// Open the Gift
    /// </summary>
    private void OnGiftPopUpCalled()
    {
        giftsPopUp.gameObject.SetActive(true);
    }

    /// <summary>
    /// Shut the Gift
    /// </summary>
    private void OnGiftPopUpShut()
    {
        giftsPopUp.gameObject.SetActive(false);
    }

    /// <summary>
    /// Open the Wheel
    /// </summary>
    private void OnFortuneWheelPopUpCalled()
    {
        fortuneWheelPopUp.gameObject.SetActive(true);
    }

    /// <summary>
    /// Shut the Wheel
    /// </summary>
    private void OnFortuneWheelPopUpShut()
    {
        fortuneWheelPopUp.gameObject.SetActive(false);
    }

    /// <summary>
    /// Open the Quests
    /// </summary>
    private void OnQuestsPopUpCalled()
    {
        questsPopUp.gameObject.SetActive(true);
    }

    /// <summary>
    /// Shut the Quests
    /// </summary>
    private void OnQuestsPopUpShut()
    {
        questsPopUp.gameObject.SetActive(false);
    }

    /// <summary>
    /// Close All Tab
    /// </summary>
    private void CleanTab()
    {
        upgradesUI.gameObject.SetActive(false);
        inventoryUI.gameObject.SetActive(false);
        challengeUI.gameObject.SetActive(false);

        CleanShopTab();
    }

    /// <summary>
    /// Open the Upgrades
    /// </summary>
    private void OnOpenUpgrades()
    {
        if (index == 1)
        {
            index = 0;

            upgradesUI.gameObject.SetActive(false);
        }
        else
        {
            if (index != 0)
            {
                CleanTab();
            }

            index = 1;

            upgradesUI.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Close the Upgrades
    /// </summary>
    private void OnShutUpgrades()
    {
        index = 0;

        upgradesUI.gameObject.SetActive(false);
        inventoryUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// Open the Inventory
    /// </summary>
    private void OnOpenInventory()
    {
        if (index == 2)
        {
            index = 0;

            inventoryUI.gameObject.SetActive(false);
        }
        else
        {
            if (index != 0)
            {
                CleanTab();
            }

            index = 2;

            inventoryUI.gameObject.SetActive(true);
        }
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