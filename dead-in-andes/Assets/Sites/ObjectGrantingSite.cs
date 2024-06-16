using System;
using Characters;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectGrantingSite : Site
{
    [SerializeField] private ItemObject generatedItem;
    [SerializeField] private SkillType usedAbility;
    [SerializeField] private int generatedMin;
    [SerializeField] private int generatedMax;

    public override void executeSiteFunctionality()
    {
        if (currentCharacter)
        {
            int usedAbilityLevel = currentCharacter[usedAbility];
            int generatedAmount = Random.Range(
                (int)Math.Round(generatedMin * (usedAbilityLevel / 100f)), 
                (int)Math.Round(generatedMax  - (generatedMin * (1 - usedAbilityLevel / 100f)))
                );
            if (generatedAmount > 0)
            {
                InventoryManager.Instance.Add(generatedItem, generatedAmount);
            }
            Debug.Log("Generated " + generatedAmount + " of : " + generatedItem.itemName);
        }
    }
}
