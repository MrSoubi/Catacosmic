using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeListController
{
    // UXML template for list entries
    VisualTreeAsset m_ListEntryTemplate;

    // UI element references
    ListView m_UpgradeList;
    Label m_UpgradeNameLabel;

    List<UpgradeData> m_AllUpgrades;

    public void InitializeCharacterList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllCharacters();

        // Store a reference to the template for the list entries
        m_ListEntryTemplate = listElementTemplate;

        // Store a reference to the character list element
        m_UpgradeList = root.Q<ListView>("ListView_Upgrades");
        m_UpgradeList.Q<ScrollView>().verticalScrollerVisibility = ScrollerVisibility.Hidden;

        FillCharacterList();
    }

    void EnumerateAllCharacters()
    {
        m_AllUpgrades = new List<UpgradeData>();
        m_AllUpgrades.AddRange(Resources.LoadAll<UpgradeData>("Upgrades"));
    }

    void FillCharacterList()
    {
        // Set up a make item function for a list entry
        m_UpgradeList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            var newListEntryLogic = new UpgradeListEntryController();

            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;

            // Initialize the controller script
            newListEntryLogic.SetVisualElement(newListEntry);

            // Return the root of the instantiated visual tree
            return newListEntry;
        };

        // Set up bind function for a specific list entry
        m_UpgradeList.bindItem = (item, index) =>
        {
            (item.userData as UpgradeListEntryController)?.SetUpgradeData(m_AllUpgrades[index]);
        };

        // Set a fixed item height matching the height of the item provided in makeItem. 
        // For dynamic height, see the virtualizationMethod property.
        m_UpgradeList.fixedItemHeight = 45;

        // Set the actual item's source list/array
        m_UpgradeList.itemsSource = m_AllUpgrades;
    }

    void OnCharacterSelected(IEnumerable<object> selectedItems)
    {
        // Get the currently selected item directly from the ListView
        var selectedCharacter = m_UpgradeList.selectedItem as UpgradeData;

        // Handle none-selection (Escape to deselect everything)
        if (selectedCharacter == null)
        {
            // Clear
            m_UpgradeNameLabel.text = "";

            return;
        }

        // Fill in character details
        m_UpgradeNameLabel.text = selectedCharacter.upgradeName;
    }
}