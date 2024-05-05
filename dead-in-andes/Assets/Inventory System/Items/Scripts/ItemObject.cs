using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// [CreateAssetMenu(fileName = "new Item", menuName = "Create Item/Item")]
public abstract class ItemObject : ScriptableObject
{
    [FormerlySerializedAs("name")] public string itemName = "New Item";
    [FormerlySerializedAs("icon")] public Sprite itemIcon;
    [TextArea(15,20)]
    public string description;
}
