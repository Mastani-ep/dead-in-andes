using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Workshop : Site
{
    public RecipeProgress recipeProgress;
    public Dictionary<Recipe, int> UnlockedRecipes = new Dictionary<Recipe, int>();
    public InventoryManager inventoryManager;
    public Recipe currentRecipe;
    public Site[] allSites;
    void Start()
    {
        _site_type = SiteType.Workshop;
        UnlockedRecipes = recipeProgress.ToDictionary();
        allSites = Object.FindObjectsOfType<Site>(true);
    }

    public bool checkRecipeRequirements(Recipe recipe)
    {
        // Check if we are starting the recipe (progress equals 0)
        // If not (>0) then we just continue the recipe - requirement met 
        if (UnlockedRecipes[recipe] == 0)
        {
            foreach (var item in recipe.requiredItems)
            {
                // If the item is not even in inventory then requirement is not met
                if (!inventoryManager.Items.ContainsKey(item.requiredItem))
                {
                    Debug.Log("The item is not in inventory");
                    return false;
                }
                // If the item there is not enough item in inventory the requirement is not met
                if (inventoryManager.Items[item.requiredItem] < item.requiredAmount)
                {
                    Debug.Log("Not enough item in inventory");
                    return false;
                }
            }
        }  
        // IT LACKS REMOVING ITEMS FROM INVENTORY
        return true;
    }

    public override void generateItem()
    {
        if (currentCharacter && currentRecipe)
        {
            // It is important to add some modificator for default progress later based on i.e. character stats
            if (checkRecipeRequirements(currentRecipe))
            {
                UnlockedRecipes[currentRecipe] += currentRecipe.defaultProgress;
                Debug.Log("Workshop progress on: " + currentRecipe+ "is:" + UnlockedRecipes[currentRecipe]);
                if (UnlockedRecipes[currentRecipe] >= 100)
                {
                    createStationFromRecipe(currentRecipe);
                }
            }
        }
    }

    public void createStationFromRecipe(Recipe recipe)
    {
        foreach (var station in allSites)
        {
            if (station.SiteType == recipe.createdSiteType)
            {
                station.gameObject.SetActive(true);
                Debug.Log("Created Site: " + station.SiteType);
            }
        }
    }
    
}
