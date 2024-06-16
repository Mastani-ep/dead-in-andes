using System;
using Characters;
using UnityEngine;
using Random = UnityEngine.Random;

public class CookingSite : Site
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FoodObject mealWeak;
    [SerializeField] private FoodObject mealMedium;
    [SerializeField] private FoodObject mealStrong;

    public override void executeSiteFunctionality()
    {
        if (currentCharacter)
        {
            // Dictionary<FoodObject, int> foodForCooking = inventoryManager.GetFoodForCooking(10);
            int foodForCookingAmount = inventoryManager.GetFoodForCookingAmount(10);


            int meal3 = Random.Range(
                (int)Math.Round((0.4 * currentCharacter[SkillType.Cooking] / 100f) * foodForCookingAmount),
                (int)Math.Round(foodForCookingAmount -
                                foodForCookingAmount * (1 - currentCharacter[SkillType.Cooking] / 100f))
            );

            int meal2 = Math.Min(
                Random.Range(
                    (int)Math.Round((0.8 * currentCharacter[SkillType.Cooking] / 100f) * foodForCookingAmount),
                    (int)Math.Round(foodForCookingAmount -
                                    foodForCookingAmount * (0.75 - currentCharacter[SkillType.Cooking] / 100f))
                ), foodForCookingAmount - meal3);

            int meal1 = foodForCookingAmount - meal2 - meal3;
            // This means that currently it gets easier to get a good meal if we cook more at once,
            // the lower the cooking level the more impact, no impact at 100

            if (meal1 != 0)
                inventoryManager.Add(mealWeak, meal1);
            if (meal2 != 0)
                inventoryManager.Add(mealMedium, meal2);
            if (meal3 != 0)
                inventoryManager.Add(mealStrong, meal3);
        }
    }
}