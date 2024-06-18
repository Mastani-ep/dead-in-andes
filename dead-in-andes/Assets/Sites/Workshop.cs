using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
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
    
    public WorkshopUI workshopUI;
    void Start()
    {
        _site_type = SiteType.Workshop;
        // UnlockedRecipes = recipeProgress.ToDictionary();
        allSites = Object.FindObjectsOfType<Site>(true);
    }

    public void ChangeCurrentRecipe(RecipeProgress recipeProgress)
    {
        if(currentRecipe != -1){
            UnlockedRecipes[currentRecipe].isSelected = false;
        }
        currentRecipe = UnlockedRecipes.IndexOf(recipeProgress);
        recipeProgress.isSelected = true;
    }

    public bool CheckRecipeRequirements(int recipeIndex)
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
                    Debug.Log("The " + item + " item is not in inventory");
                    return false;
                }
                // If the item there is not enough item in inventory the requirement is not met
                if (inventoryManager.Items[item.requiredItem] < item.requiredAmount)
                {
                    Debug.Log("Not enough of " + item + " item in inventory");
                    return false;
                }
            }
        }
        return true;
    }

    public bool CheckAndValidateRecipeRequirements(int recipeIndex)
    {
        if (CheckRecipeRequirements(recipeIndex))
        {
            RemoveItemsIfFirstCraftingIteration(recipeIndex);
            return true;
        }

        return false;
    }

    public void RemoveItemsIfFirstCraftingIteration(int recipeIndex)
    {
        if (UnlockedRecipes[recipeIndex].progress == 0)
        {
            foreach (var item in UnlockedRecipes[recipeIndex].recipe.requiredItems)
            {
                inventoryManager.Remove(item.requiredItem, item.requiredAmount);
            }
        }
    }

    public override void executeSiteFunctionality()
    {
        if (currentCharacter && currentRecipe != -1)
        {
            Recipe recipe = UnlockedRecipes[currentRecipe].recipe;
            if (CheckAndValidateRecipeRequirements(currentRecipe))
            {
                float progress = (recipe.DefaultResearchProgress) *
                                 (1f + 1f * (currentCharacter[SkillType.Craftsmanship] / 100f));
                UnlockedRecipes[currentRecipe].progress += progress;
                Debug.Log("Workshop progress on: " + recipe + "is:" + UnlockedRecipes[currentRecipe]);
                if (UnlockedRecipes[currentRecipe].progress >= 100)
                {
                    UnlockedRecipes[currentRecipe].isDone = true;
                    currentRecipe = -1;
                    CreateStationFromRecipe(recipe);
                    // CheckEndGameCondition();
                }
            }
            ApplySkillsAndNeedsChanges();
        }
    }

    public void CreateStationFromRecipe(Recipe recipe)
    {
        foreach (var station in allSites)
        {
            if (station.SiteType == recipe.createdSiteType)
            {
                // DayStagesController.Instance.allSites.Add(station);
                station.gameObject.SetActive(true);
                Debug.Log("Created Site: " + station.SiteType);
            }
        }
    }

    public override void ShowUI()
    {
        workshopUI.WorkshopUIButton.gameObject.SetActive(true);
    } 
    
    public override void HideUI()
    {
        workshopUI.WorkshopUIButton.gameObject.SetActive(false);
    }

    public void AddRecipeAfterResearch(Recipe researchedRecipe)
    {
        UnlockedRecipes.Add(new RecipeProgress(researchedRecipe));
    }
    
}
