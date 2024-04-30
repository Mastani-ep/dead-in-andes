using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrantingSite : Site
{
    [SerializeField] private ItemObject generatedItem;

    [SerializeField] private string usedAbility;

    public override void generateItem()
    {
        if (currentCharacter)
        {
            Debug.Log("Generated: " + generatedItem.name);
        }
    }
}
