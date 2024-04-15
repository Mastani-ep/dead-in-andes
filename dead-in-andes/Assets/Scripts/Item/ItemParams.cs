using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Item", menuName = "Create Item/Item")]
public class ItemParams : ScriptableObject
{
    public string name = "New Item";
    public Sprite image;
}
