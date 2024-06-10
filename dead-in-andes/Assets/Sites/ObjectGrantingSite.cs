using UnityEngine;

public class ObjectGrantingSite : Site
{
    [SerializeField] private ItemObject generatedItem;

    [SerializeField] private string usedAbility;
    // public InventoryObject inventory;

    public override void executeSiteFunctionality()
    {
        if (currentCharacter)
        {
            InventoryManager.Instance.Add(generatedItem, 1);
            // TODO Currently only adds one
            Debug.Log("Generated: " + generatedItem.itemName);
        }
    }
}
