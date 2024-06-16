using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    [FormerlySerializedAs("hungerModifier")] public int hungerModifierMin;
    [FormerlySerializedAs("hungerModifier")] public int hungerModifierMax;
    public bool isRaw;
}
