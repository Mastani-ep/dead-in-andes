using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Usable Item Object", menuName = "Inventory System/Items/Usable Item")]
public class UsableItemObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.UsableItem;
    }
    public int hungerModifier;
    public int fatigueModifier;
    public int sicknessMeter;
}
