using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Food", menuName = "Create Item/Food")]
public class FoodParams : ItemParams
{
    public int hunger_modifier;
    public bool is_raw;
}
