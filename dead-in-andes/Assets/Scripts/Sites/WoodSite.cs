using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSite : Site
{
    [SerializeField] private ItemParams generatedItem;

    [SerializeField] private string usedAbility;

    public override void generateItem()
    {
        if (currentCharacter)
        {
            Debug.Log("Generated: " + generatedItem.name);
        }
    }
}
