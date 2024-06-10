using UnityEngine;
[CreateAssetMenu(fileName = "new Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    public int hungerModifier;
    public bool isRaw;
}
