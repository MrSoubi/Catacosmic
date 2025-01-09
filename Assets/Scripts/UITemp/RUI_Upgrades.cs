using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;
using System.Diagnostics;

public class RUI_Upgrades : MonoBehaviour
{
    [Title("Output Events")]
    public RSE_ShutUpgrade shutUpgrade;

    public RSO_CurrentDisasterAttackSpeed currentDisasterAttackSpeed;
    public RSO_CurrentDisasterCriticChance currentDisasterCriticChance;
    public RSO_CurrentDisasterCriticMultiplier currentDisasterCriticMultiplier;
    public RSO_CurrentDisasterSize currentDisasterSize;
    public RSO_CurrentDisasterStrength currentDisasterStrength;
    public RSO_CurrentDisasterVelocity currentDisasterVelocity;

    public RSO_PlayerMoney currentPlayerMoney;

    private Button buttonGame;

    private Button buttonSpeed;
    private Button buttonSize;
    private Button buttonDamage;
    private Button buttonFrequency;
    private Button buttonCriticChance;
    private Button buttonCriticMultiplier;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        buttonGame = uiDocument.rootVisualElement.Q("Button_Game") as Button;

        buttonSpeed = uiDocument.rootVisualElement.Q("Button_UpgradeSpeed") as Button;
        buttonSize = uiDocument.rootVisualElement.Q("Button_UpgradeSize") as Button;
        buttonDamage = uiDocument.rootVisualElement.Q("Button_UpgradeDamage") as Button;
        buttonFrequency = uiDocument.rootVisualElement.Q("Button_UpgradeFrequency") as Button;
        buttonCriticChance = uiDocument.rootVisualElement.Q("Button_UpgradeCriticChance") as Button;
        buttonCriticMultiplier = uiDocument.rootVisualElement.Q("Button_UpgradeCriticMultiplier") as Button;

        buttonGame.RegisterCallback<ClickEvent>(CallButtonGame);

        buttonSpeed.RegisterCallback<ClickEvent>(CallButtonSpeed);
        buttonSize.RegisterCallback<ClickEvent>(CallButtonSize);
        buttonDamage.RegisterCallback<ClickEvent>(CallButtonDamage);
        buttonFrequency.RegisterCallback<ClickEvent>(CallButtonFrequency);
        buttonCriticChance.RegisterCallback<ClickEvent>(CallButtonCriticChance);
        buttonCriticMultiplier.RegisterCallback<ClickEvent>(CallButtonCriticMultipler);
    }

    private void OnDisable()
    {
        buttonGame.UnregisterCallback<ClickEvent>(CallButtonGame);

        buttonSpeed.UnregisterCallback<ClickEvent>(CallButtonSpeed);
        buttonSize.UnregisterCallback<ClickEvent>(CallButtonSize);
        buttonDamage.UnregisterCallback<ClickEvent>(CallButtonDamage);
        buttonFrequency.UnregisterCallback<ClickEvent>(CallButtonFrequency);
        buttonCriticChance.UnregisterCallback<ClickEvent>(CallButtonCriticChance);
        buttonCriticMultiplier.UnregisterCallback<ClickEvent>(CallButtonCriticMultipler);
    }

    private void CallButtonGame(ClickEvent clickEvent)
    {
        shutUpgrade.Fire?.Invoke();
    }

    private void CallButtonSpeed(ClickEvent clickEvent)
    {
        float price = 50 * Mathf.Pow(1, 1);

        if (currentPlayerMoney.Value >= price)
        {
            currentDisasterVelocity.Value = 1 * 1;
            currentPlayerMoney.Value -= price;
        }
    }

    private void CallButtonSize(ClickEvent clickEvent)
    {
        float price = 60 * Mathf.Pow(3.4f, 1);

        if (currentPlayerMoney.Value >= price)
        {
            currentDisasterSize.Value = 0.2f * 1;
            currentPlayerMoney.Value -= price;
        }
    }

    private void CallButtonDamage(ClickEvent clickEvent)
    {
        float price = 40 * Mathf.Pow(3, 1);

        if (currentPlayerMoney.Value >= price)
        {
            currentDisasterStrength.Value = Mathf.Pow(1, 1.8f);
            currentPlayerMoney.Value -= price;
        }
    }

    private void CallButtonFrequency(ClickEvent clickEvent)
    {
        float price = 50 * Mathf.Pow(3.2f, 1);

        if (currentPlayerMoney.Value >= price)
        {
            currentDisasterAttackSpeed.Value = 1 * 1;
            currentPlayerMoney.Value -= price;
        }
    }

    private void CallButtonCriticChance(ClickEvent clickEvent)
    {
        float price = 80 * Mathf.Pow(3.8f, 1);

        if (currentPlayerMoney.Value >= price)
        {
            currentDisasterCriticChance.Value = Mathf.Pow(1, 1.1f);
            currentPlayerMoney.Value -= price;
        }
    }

    private void CallButtonCriticMultipler(ClickEvent clickEvent)
    {
        float price = 70 * Mathf.Pow(3.6f, 1);

        if (currentPlayerMoney.Value >= price)
        {
            currentDisasterCriticMultiplier.Value = 1 * 0.01f;
            currentPlayerMoney.Value -= price;
        }
    }
}
