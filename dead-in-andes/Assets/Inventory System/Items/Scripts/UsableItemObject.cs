using UnityEngine;
[CreateAssetMenu(fileName = "new Usable Item Object", menuName = "Inventory System/Items/Usable Item")]
public class UsableItemObject : ItemObject
{
    public int hungerModifier;
    public int fatigueModifier;
    public int sicknessMeter;
    public int depressionMeter;
}
