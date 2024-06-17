using System;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectGrantingSite : Site
{
    [SerializeField] private List<ItemObject> generatedItems;
    [SerializeField] private SkillType usedAbility;

    public override void executeSiteFunctionality()
    {
        if (currentCharacter)
        {
            foreach (var generatedItem in generatedItems)
            {
                int usedAbilityLevel = currentCharacter[usedAbility];
                int generatedAmount = Random.Range(
                    (int)Math.Round(generatedItem.generatedMin * (usedAbilityLevel / 100f)), 
                    (int)Math.Round(generatedItem.generatedMax  - (generatedItem.generatedMin * (1 - usedAbilityLevel / 100f)))
                );
                if (generatedAmount > 0)
                {
                    InventoryManager.Instance.Add(generatedItem, generatedAmount);
                }
                Debug.Log("Generated " + generatedAmount + " of : " + generatedItem.itemName);
                ApplySkillsAndNeedsChanges();
            }
        }
    }
}
