using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class RUI_Upgrades : MonoBehaviour
{
    [Title("Output Events")]
    public RSE_ShutUpgrade shutUpgrade;

    public RSO_CurrentDisasterVelocity currentDisasterVelocity;
    public RSO_CurrentDisasterSize currentDisasterSize;
    public RSO_CurrentDisasterStrength currentDisasterStrength;
    public RSO_CurrentDisasterAttackSpeed currentDisasterAttackSpeed;
    public RSO_CurrentDisasterCriticChance currentDisasterCriticChance;
    public RSO_CurrentDisasterCriticMultiplier currentDisasterCriticMultiplier;

    public RSO_PlayerMoney currentPlayerMoney;

    [Title("Parameters TEMP")]
    public List<int> levels;
    public List<int> costs;

    private Button buttonGame;

    private Label textLevelVelocity;
    private Button buttonVelocity;
    private Label textStatVelocity;
    private Label textCostVelocity;

    private Label textLevelSize;
    private Button buttonSize;
    private Label textStatSize;
    private Label textCostSize;

    private Label textLevelStrength;
    private Button buttonStrength;
    private Label textStatStrength;
    private Label textCostStrength;

    private Label textLevelAttackSpeed;
    private Button buttonAttackSpeed;
    private Label textStatAttackSpeed;
    private Label textCostAttackSpeed;

    private Label textLevelCriticChance;
    private Button buttonCriticChance;
    private Label textStatCriticChance;
    private Label textCostCriticChance;

    private Label textLevelCriticMultiplier;
    private Button buttonCriticMultiplier;
    private Label textStatCriticMultiplier;
    private Label textCostCriticMultiplier;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        buttonGame = uiDocument.rootVisualElement.Q("Button_Game") as Button;

        textLevelVelocity = uiDocument.rootVisualElement.Q("Text_LevelVelocity") as Label;
        buttonVelocity = uiDocument.rootVisualElement.Q("Button_UpgradeVelocity") as Button;
        textStatVelocity = uiDocument.rootVisualElement.Q("Text_UpgradeStatVelocity") as Label;
        textCostVelocity = uiDocument.rootVisualElement.Q("Text_CostVelocity") as Label;

        textLevelSize = uiDocument.rootVisualElement.Q("Text_LevelSize") as Label;
        buttonSize = uiDocument.rootVisualElement.Q("Button_UpgradeSize") as Button;
        textStatSize = uiDocument.rootVisualElement.Q("Text_UpgradeStatSize") as Label;
        textCostSize = uiDocument.rootVisualElement.Q("Text_CostSize") as Label;

        textLevelStrength = uiDocument.rootVisualElement.Q("Text_LevelStrength") as Label;
        buttonStrength = uiDocument.rootVisualElement.Q("Button_UpgradeStrength") as Button;
        textStatStrength = uiDocument.rootVisualElement.Q("Text_UpgradeStatStrength") as Label;
        textCostStrength = uiDocument.rootVisualElement.Q("Text_CostStrength") as Label;

        textLevelAttackSpeed = uiDocument.rootVisualElement.Q("Text_LevelAttackSpeed") as Label;
        buttonAttackSpeed = uiDocument.rootVisualElement.Q("Button_UpgradeAttackSpeed") as Button;
        textStatAttackSpeed = uiDocument.rootVisualElement.Q("Text_UpgradeStatAttackSpeed") as Label;
        textCostAttackSpeed = uiDocument.rootVisualElement.Q("Text_CostAttackSpeed") as Label;

        textLevelCriticChance = uiDocument.rootVisualElement.Q("Text_LevelCriticChance") as Label;
        buttonCriticChance = uiDocument.rootVisualElement.Q("Button_UpgradeCriticChance") as Button;
        textStatCriticChance = uiDocument.rootVisualElement.Q("Text_UpgradeStatCriticChance") as Label;
        textCostCriticChance = uiDocument.rootVisualElement.Q("Text_CostCriticChance") as Label;

        textLevelCriticMultiplier = uiDocument.rootVisualElement.Q("Text_LevelCriticMultiplier") as Label;
        buttonCriticMultiplier = uiDocument.rootVisualElement.Q("Button_UpgradeCriticMultiplier") as Button;
        textStatCriticMultiplier = uiDocument.rootVisualElement.Q("Text_UpgradeStatCriticMultiplier") as Label;
        textCostCriticMultiplier = uiDocument.rootVisualElement.Q("Text_CostCriticMultiplier") as Label;

        buttonGame.RegisterCallback<ClickEvent>(CallButtonGame);

        buttonVelocity.RegisterCallback<ClickEvent>(CallButtonVelocity);
        buttonSize.RegisterCallback<ClickEvent>(CallButtonSize);
        buttonStrength.RegisterCallback<ClickEvent>(CallButtonStrength);
        buttonAttackSpeed.RegisterCallback<ClickEvent>(CallButtonAttackSpeed);
        buttonCriticChance.RegisterCallback<ClickEvent>(CallButtonCriticChance);
        buttonCriticMultiplier.RegisterCallback<ClickEvent>(CallButtonCriticMultipler);

        textStatVelocity.text = currentDisasterVelocity.Value.ToString();
        textStatSize.text = currentDisasterSize.Value.ToString();
        textStatStrength.text = currentDisasterStrength.Value.ToString();
        textStatAttackSpeed.text = currentDisasterAttackSpeed.Value.ToString();
        textStatCriticChance.text = currentDisasterCriticChance.Value.ToString();
        textStatCriticMultiplier.text = currentDisasterCriticMultiplier.Value.ToString();

        textLevelVelocity.text = "LvL. " + levels[0].ToString();
        textCostVelocity.text = costs[0].ToString();

        textLevelSize.text = "LvL. " + levels[1].ToString();
        textCostSize.text = costs[1].ToString();

        textLevelStrength.text = "LvL. " + levels[2].ToString();
        textCostStrength.text = costs[2].ToString();

        textLevelAttackSpeed.text = "LvL. " + levels[3].ToString();
        textCostAttackSpeed.text = costs[3].ToString();

        textLevelCriticChance.text = "LvL. " + levels[4].ToString();
        textCostCriticChance.text = costs[4].ToString();

        textLevelCriticMultiplier.text = "LvL. " + levels[5].ToString();
        textCostCriticMultiplier.text = costs[5].ToString();
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
        if (currentPlayerMoney.Value >= costs[0] && levels[0] < 50)
        {
            currentPlayerMoney.Value -= costs[0];

            levels[0] = levels[0] + 1;

            costs[0] = costs[0] * levels[0];

            currentDisasterVelocity.Value = 0.4f + 0.1f * levels[0];

            textLevelVelocity.text = "LvL. " + levels[0].ToString();
            textStatVelocity.text = currentDisasterVelocity.Value.ToString();
            textCostVelocity.text = costs[0].ToString();
        }
    }

    private void CallButtonSize(ClickEvent clickEvent)
    {
        if (currentPlayerMoney.Value >= costs[1] && levels[1] < 50)
        {
            currentPlayerMoney.Value -= costs[1];

            levels[1] = levels[1] + 1;

            costs[1] = costs[1] * levels[1];

            currentDisasterSize.Value = 0.2f + 0.05f * levels[1];

            textLevelSize.text = "LvL. " + levels[1].ToString();
            textStatSize.text = currentDisasterSize.Value.ToString();
            textCostSize.text = costs[1].ToString();
        }
    }

    private void CallButtonStrength(ClickEvent clickEvent)
    {
        if (currentPlayerMoney.Value >= costs[2] && levels[2] < 50)
        {
            currentPlayerMoney.Value -= costs[2];

            levels[2] = levels[2] + 1;

            costs[2] = costs[2] * levels[2];

            currentDisasterStrength.Value = Mathf.Pow(levels[2], 1f);

            textLevelStrength.text = "LvL. " + levels[2].ToString();
            textStatStrength.text = currentDisasterStrength.Value.ToString();
            textCostStrength.text = costs[2].ToString();
        }
    }

    private void CallButtonAttackSpeed(ClickEvent clickEvent)
    {
        if (currentPlayerMoney.Value >= costs[3] && levels[3] < 50)
        {
            currentPlayerMoney.Value -= costs[3];

            levels[3] = levels[3] + 1;

            costs[3] = costs[3] * levels[3];

            currentDisasterAttackSpeed.Value = 1 / (1f * levels[3]);

            textLevelAttackSpeed.text = "LvL. " + levels[3].ToString();
            textStatAttackSpeed.text = currentDisasterAttackSpeed.Value.ToString();
            textCostAttackSpeed.text = costs[3].ToString();
        }
    }

    private void CallButtonCriticChance(ClickEvent clickEvent)
    {
        if (currentPlayerMoney.Value >= costs[4] && levels[4] < 50)
        {
            currentPlayerMoney.Value -= costs[4];

            levels[4] = levels[4] + 1;

            currentDisasterCriticChance.Value = Mathf.Pow(levels[4], 1f);

            textLevelCriticChance.text = "LvL. " + levels[4].ToString();
            textStatCriticChance.text = currentDisasterCriticChance.Value.ToString();
            textCostCriticChance.text = costs[4].ToString();
        }
    }

    private void CallButtonCriticMultipler(ClickEvent clickEvent)
    {
        if (currentPlayerMoney.Value >= costs[5] && levels[5] < 50)
        {
            currentPlayerMoney.Value -= costs[5];

            levels[5] = levels[5] + 1;

            currentDisasterCriticMultiplier.Value = 1.1f + 0.05f * levels[5];

            textLevelCriticMultiplier.text = "LvL. " + levels[5].ToString();
            textStatCriticMultiplier.text = currentDisasterCriticMultiplier.Value.ToString();
            textCostCriticMultiplier.text = costs[5].ToString();
        }
    }
}
