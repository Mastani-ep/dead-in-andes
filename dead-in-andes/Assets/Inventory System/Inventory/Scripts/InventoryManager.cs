using System.Collections.Generic;
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

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemObject item)
    {
        if (Items.ContainsKey(item))
        {
            // If it exists, increment its value
            Items[item]++;
        }
        else
        {
            // If it doesn't exist, add it with value 1
            Items[item] = 1;
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

            itemName.text = item.Key.itemName;
            itemCount.text = item.Value.ToString();
            itemIcon.sprite = item.Key.itemIcon;
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
}