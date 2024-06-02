using System.Collections.Generic;
using UnityEngine;

public class TechnologySite : Site
{
    [SerializeField] private List<TechnologyProgress> technologyRecipeList;
    [SerializeField] private TechnologySiteUI technologySiteUI;
    [SerializeField] private int currentTechnology;
    [SerializeField] private Workshop workshop;
    
    private void Start()
    {
        technologySiteUI.ResetAndSetNextTechnology(technologyRecipeList[currentTechnology].recipe.recipeName);
    }

    private void SetNextTechnology()
    {
        if (currentTechnology == technologyRecipeList.Count - 1)
        {
            technologySiteUI.SetResearchAsFinished();
            return;
        }
        currentTechnology++;
        technologySiteUI.ResetAndSetNextTechnology(technologyRecipeList[currentTechnology].recipe.recipeName);
    }

    private void ProgressCurrentTechnology()
    {
        if (currentTechnology == technologyRecipeList.Count)
        {
            return;
        }

        var recipe = technologyRecipeList[currentTechnology].recipe;
        
        technologyRecipeList[currentTechnology].progress += recipe.DefaultResearchProgress;
        technologySiteUI.IncreaseResearchSliderProgress(recipe.DefaultResearchProgress);
        
        if (technologyRecipeList[currentTechnology].progress >= 100)
        {
            technologyRecipeList[currentTechnology].isDone = true;
            workshop.AddRecipeAfterResearch(recipe);
            technologySiteUI.OpenPopUpPanel(recipe);
            SetNextTechnology();
        }
    }

    public override void executeSiteFunctionality()
    {
        if (currentCharacter)
        {
            ProgressCurrentTechnology();
        }
    }
}
