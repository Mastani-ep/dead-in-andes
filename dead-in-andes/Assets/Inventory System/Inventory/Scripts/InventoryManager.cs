using System.Collections.Generic;
using Characters;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Dictionary<ItemObject, int> Items = new Dictionary<ItemObject, int>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject Inventory;
    public GameObject InventoryButton;
    [SerializeField] private Character[] charactersList;

    [SerializeField] private GameObject ItemCharacterInteraction;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform itemCharacterInteractionContent;
    
    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        charactersList = Object.FindObjectsOfType<Character>();
        // TODO I don't know if it will ever update and if so if it won't update too much
    }
    
    public void Add(ItemObject item, int count)
    {
        if (Items.ContainsKey(item))
        {
            // If it exists, increment its value
            Items[item] += count;
        }
        else
        {
            // If it doesn't exist, add it with value 1
            Items[item] = count;
        }
    }

    public void Remove(ItemObject item, int count)
    {
        if (Items[item] > count)
        {
            Items[item] -= count;
        }
        else
        {
            Items.Remove(item);
        }
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemCount = obj.transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            Button itemButton = obj.GetComponent<Button>();
            
            itemName.text = item.Key.itemName;
            itemCount.text = item.Value.ToString();
            itemIcon.sprite = item.Key.itemIcon;
            if (item.Key.needType != NeedType.NotApplicable)
            {
                itemButton.onClick.AddListener(() => { InteractWithItemInInventory(item.Key, obj); });
            }
            // TODO turn off the button if it's not applicable
        }
    }

    public void ToggleInventoryButton()
    {
        if (Inventory.activeSelf)
        {
            Inventory.SetActive(false);
            InventoryButton.SetActive(true);
        }
        else
        {
            Inventory.SetActive(true);
            InventoryButton.SetActive(false);
            ListItems();
        }
    }

    private void InteractWithItemInInventory(ItemObject item, GameObject inventoryItemObject)
    {
        ItemCharacterInteraction.SetActive(true);
        ListCharacters(item, inventoryItemObject);
    }
    
    private void ListCharacters(ItemObject item, GameObject inventoryItemObject)
    {
        foreach (Transform character in itemCharacterInteractionContent)
        {
            Destroy(character.gameObject);
        }

        foreach (var character in charactersList)
        {
            GameObject obj = Instantiate(characterPrefab, itemCharacterInteractionContent);
            var characterName = obj.transform.Find("CharacterName").GetComponent<TMPro.TextMeshProUGUI>();
            var abilityDisplayed = obj.transform.Find("AbilityDisplayed").GetComponent<TMPro.TextMeshProUGUI>();
            var characterIcon = obj.transform.Find("CharacterIcon").GetComponent<Image>();
            var abilitySlider = obj.transform.Find("AbilityValues/AbilitySlider").GetComponent<Slider>();
            var abilityValue = obj.transform.Find("AbilityValues/AbilityValueText").GetComponent<TMPro.TextMeshProUGUI>();
            
            Button characterButton = obj.GetComponent<Button>();

            characterName.text = character.characterName;
            abilityDisplayed.text = item.needType.ToString();
            characterIcon.sprite = character.characterImage.sprite;
            abilitySlider.value = character[item.needType];
            abilityValue.text = character[item.needType].ToString();

            characterButton.onClick.AddListener(() => { ItemWithCharacterInteraction(character, item, inventoryItemObject); });
        }
    }

    private void ItemWithCharacterInteraction(Character character, ItemObject item, GameObject inventoryItemObject)
    {
        if (item.type == ItemType.Food)
        {
            FoodItemInteraction(character, (FoodObject)item);
        }
        else if (item.type == ItemType.Usable)
        {
            UsableItemInteraction(character, (UsableItemObject)item);
        }
        ItemCharacterInteraction.SetActive(false);
        
        if (Items[item] > 1)
        {
            var itemCount = inventoryItemObject.transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();
            itemCount.text = (Items[item] - 1).ToString();
        }
        else
        {
            Destroy(inventoryItemObject.gameObject);
        }
        Remove(item, 1);
    }

    private void FoodItemInteraction(Character character, FoodObject item)
    {
        character[NeedType.Hunger] -= item.hungerModifier;
        if (item.isRaw)
        {
            character[NeedType.Sickness] += Random.Range(0, 5);
        }
    }
    
    private void UsableItemInteraction(Character character, UsableItemObject item)
    {
        character[NeedType.Fatigue] -= item.fatigueModifier;
        character[NeedType.Hunger] -= item.hungerModifier;
        character[NeedType.Sickness] -= item.sicknessMeter;
    }

    public void CloseItemInteractionMenu()
    {
        ItemCharacterInteraction.SetActive(false);
    }
}