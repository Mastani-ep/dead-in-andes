using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrantingSite : Site
{
    [SerializeField] private ItemObject generatedItem;

    [SerializeField] private string usedAbility;
    public InventoryObject inventory;

    public override void generateItem()
    {
        if (currentCharacter)
        {
            inventory.AddItem(generatedItem, 1);
            Debug.Log("Generated: " + generatedItem.name);
        }
    }
}
