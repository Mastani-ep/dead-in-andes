using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WhyDoesItAssignItself,
    Food,
    UsableItem,
    Crafting
}

// [CreateAssetMenu(fileName = "new Item", menuName = "Create Item/Item")]
public abstract class ItemObject : ScriptableObject
{
    public string name = "New Item";
    public Sprite image;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
}
