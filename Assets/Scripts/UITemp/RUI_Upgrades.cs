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

    private Button buttonVelocity;
    private Button buttonSize;
    private Button buttonStrength;
    private Button buttonAttackSpeed;
    private Button buttonCriticChance;
    private Button buttonCriticMultiplier;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        buttonGame = uiDocument.rootVisualElement.Q("Button_Game") as Button;

        buttonVelocity = uiDocument.rootVisualElement.Q("Button_UpgradeVelocity") as Button;
        buttonSize = uiDocument.rootVisualElement.Q("Button_UpgradeSize") as Button;
        buttonStrength = uiDocument.rootVisualElement.Q("Button_UpgradeStrength") as Button;
        buttonAttackSpeed = uiDocument.rootVisualElement.Q("Button_UpgradeAttackSpeed") as Button;
        buttonCriticChance = uiDocument.rootVisualElement.Q("Button_UpgradeCriticChance") as Button;
        buttonCriticMultiplier = uiDocument.rootVisualElement.Q("Button_UpgradeCriticMultiplier") as Button;

        buttonGame.RegisterCallback<ClickEvent>(CallButtonGame);

        buttonVelocity.RegisterCallback<ClickEvent>(CallButtonVelocity);
        buttonSize.RegisterCallback<ClickEvent>(CallButtonSize);
        buttonStrength.RegisterCallback<ClickEvent>(CallButtonStrength);
        buttonAttackSpeed.RegisterCallback<ClickEvent>(CallButtonAttackSpeed);
        buttonCriticChance.RegisterCallback<ClickEvent>(CallButtonCriticChance);
        buttonCriticMultiplier.RegisterCallback<ClickEvent>(CallButtonCriticMultipler);
    }

    private void OnDisable()
    {
        buttonGame.UnregisterCallback<ClickEvent>(CallButtonGame);

        buttonVelocity.UnregisterCallback<ClickEvent>(CallButtonVelocity);
        buttonSize.UnregisterCallback<ClickEvent>(CallButtonSize);
        buttonStrength.UnregisterCallback<ClickEvent>(CallButtonStrength);
        buttonAttackSpeed.UnregisterCallback<ClickEvent>(CallButtonAttackSpeed);
        buttonCriticChance.UnregisterCallback<ClickEvent>(CallButtonCriticChance);
        buttonCriticMultiplier.UnregisterCallback<ClickEvent>(CallButtonCriticMultipler);
    }

    private void CallButtonGame(ClickEvent clickEvent)
    {
        shutUpgrade.Fire?.Invoke();
    }

    private void CallButtonVelocity(ClickEvent clickEvent)
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

    private void CallButtonStrength(ClickEvent clickEvent)
    {
        float price = 40 * Mathf.Pow(3, 1);

        if (currentPlayerMoney.Value >= price)
        {
            currentDisasterStrength.Value = Mathf.Pow(1, 1.8f);
            currentPlayerMoney.Value -= price;
        }
    }

    private void CallButtonAttackSpeed(ClickEvent clickEvent)
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
