using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Dictionary<ItemObject, int> Items = new Dictionary<ItemObject, int>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject Inventory;
    public GameObject InventoryButton;
    [SerializeField] private Character[] charactersList;
    [SerializeField] private GameObject itemCharacterInteraction;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform itemCharacterInteractionContent;
    
    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        charactersList = Object.FindObjectsOfType<Character>().Where(character => character.alive).ToArray();
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
            EventTrigger tooltipEventTrigger = obj.GetComponent<EventTrigger>();
            
            AddEventTriggerListener(tooltipEventTrigger, EventTriggerType.PointerEnter, (data) => InventoryTooltip.ShowTooltip_Static(item.Key.itemDescription));
            AddEventTriggerListener(tooltipEventTrigger, EventTriggerType.PointerExit, (data) => InventoryTooltip.HideTooltip_Static());
            
            itemName.text = item.Key.itemName;
            itemCount.text = item.Value.ToString();
            itemIcon.sprite = item.Key.itemIcon;
            if (item.Key.needType != NeedType.NotApplicable)
            {
                itemButton.onClick.AddListener(() => { InteractWithItemInInventory(item.Key, obj); });
                itemButton.interactable = true;
            }
        }
    }

    public void ToggleInventoryButton()
    {
        if (Inventory.activeSelf)
        {
            GameManager.Instance.DecreaseLockingInteractionCount();
            Inventory.SetActive(false);
            InventoryButton.SetActive(true);
        }
        else
        {
            charactersList = Object.FindObjectsOfType<Character>().Where(character => character.alive).ToArray();
            GameManager.Instance.IncreaseLockingInteractionCount();
            Inventory.SetActive(true);
            InventoryButton.SetActive(false);
            ListItems();
        }
    }

    private void InteractWithItemInInventory(ItemObject item, GameObject inventoryItemObject)
    {
        itemCharacterInteraction.SetActive(true);
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
            if (character[item.needType] > 0)
            {
                characterButton.onClick.AddListener(() => { ItemWithCharacterInteraction(character, item, inventoryItemObject); });
                characterButton.interactable = true;
            }
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
        itemCharacterInteraction.SetActive(false);
        
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
        character[NeedType.Hunger] = Math.Max(character[NeedType.Hunger] - Random.Range(item.hungerModifierMin, item.hungerModifierMax), 0);
        if (item.isRaw)
        {
            character[NeedType.Sickness] = Math.Min(character[NeedType.Sickness] + Random.Range(0, 5), 100);
        }
    }
    
    private void UsableItemInteraction(Character character, UsableItemObject item)
    {
        character[NeedType.Fatigue] = Math.Max(character[NeedType.Fatigue] - item.fatigueModifier, 0);
        character[NeedType.Hunger] = Math.Max(character[NeedType.Hunger] - item.hungerModifier, 0);
        character[NeedType.Sickness] = Math.Max(character[NeedType.Sickness] - item.sicknessModifier, 0);
        character[NeedType.Depression] = Math.Max(character[NeedType.Depression] - item.depressionModifier, 0);
        character[NeedType.Injury] = Math.Max(character[NeedType.Injury] - item.injuryModifier, 0);
    }

    public void CloseItemInteractionMenu()
    {
        itemCharacterInteraction.SetActive(false);
    }
    
    private void AddEventTriggerListener(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((data) => callback(data));
        trigger.triggers.Add(entry);
    }

    public int GetFoodForCookingAmount(int cookingLimit)
    {
        // var filteredValues = new Dictionary<FoodObject, int>();
        int amount = 0;
        var cumulativeSum = 0;

        foreach (var kvp in Items.ToList())
        {
            if (kvp.Key is FoodObject foodObj && foodObj.isRaw)
            {
                if (cumulativeSum + kvp.Value <= cookingLimit)
                {
                    // filteredValues.Add(foodObj, kvp.Value);
                    amount += kvp.Value;
                    Items.Remove(kvp.Key);
                    cumulativeSum += kvp.Value;
                }
                else if (cumulativeSum < cookingLimit)
                {
                    // filteredValues.Add(foodObj, cookingLimit - cumulativeSum);
                    amount += cookingLimit - cumulativeSum;
                    Items[kvp.Key] -= cookingLimit - cumulativeSum;
                    cumulativeSum = cookingLimit;
                }
            }
        }

        return amount;
        // return filteredValues;
        
        // return Items
        //     .Where(kvp => kvp.Key is FoodObject foodObj && foodObj.isRaw)
        //     .ToDictionary(kvp => (FoodObject)kvp.Key, kvp => kvp.Value);
    }
}