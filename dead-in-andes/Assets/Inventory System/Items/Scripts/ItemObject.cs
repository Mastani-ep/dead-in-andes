using Characters;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

// [CreateAssetMenu(fileName = "new Item", menuName = "Create Item/Item")]
public abstract class ItemObject : ScriptableObject
{
    [FormerlySerializedAs("name")] public string itemName = "New Item";
    [FormerlySerializedAs("icon")] public Sprite itemIcon;
    [TextArea(15,20)]
    public string itemDescription;
    public NeedType needType;
    public ItemType type;
    // public int needChangeModifier = 0;
}
