using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Workshop : Site
{
    public List<RecipeProgress> UnlockedRecipes;
    // public Dictionary<Recipe, int> UnlockedRecipes = new Dictionary<Recipe, int>();
    public InventoryManager inventoryManager;
    // currentRecipe is the index of current recipe in the list of unlocked recipes
    public int currentRecipe;
    public Site[] allSites;
    void Start()
    {
        _site_type = SiteType.Workshop;
        // UnlockedRecipes = recipeProgress.ToDictionary();
        allSites = Object.FindObjectsOfType<Site>(true);
    }

    public bool checkRecipeRequirements(int recipeIndex)
    {
        // Check if we are starting the recipe (progress equals 0)
        // If not (>0) then we just continue the recipe - requirement met 
        Recipe recipe = UnlockedRecipes[recipeIndex].recipe;
        if (UnlockedRecipes[recipeIndex].isDone)
        {
            return false;
        }
        
        if (UnlockedRecipes[recipeIndex].progress == 0)
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
            
            // When above requirements are met, remove required items from inventory
            foreach (var item in recipe.requiredItems)
            {
                inventoryManager.Remove(item.requiredItem, item.requiredAmount);
            }
        }
        return true;
    }

    public override void generateItem()
    {
        if (currentCharacter && currentRecipe != -1)
        {
            Recipe recipe = UnlockedRecipes[currentRecipe].recipe;
            // It is important to add some modificator for default progress later based on i.e. character stats
            if (checkRecipeRequirements(currentRecipe))
            {
                UnlockedRecipes[currentRecipe].progress += recipe.defaultProgress;
                Debug.Log("Workshop progress on: " + recipe + "is:" + UnlockedRecipes[currentRecipe]);
                if (UnlockedRecipes[currentRecipe].progress >= 100)
                {
                    UnlockedRecipes[currentRecipe].isDone = true;
                    currentRecipe = -1;
                    createStationFromRecipe(recipe);
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
