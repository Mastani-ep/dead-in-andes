using System.Collections.Generic;
using Characters;
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
        currentTechnology++;
        if (currentTechnology == technologyRecipeList.Count)
        {
            technologySiteUI.SetResearchAsFinished();
            return;
        }
        technologySiteUI.ResetAndSetNextTechnology(technologyRecipeList[currentTechnology].recipe.recipeName);
    }

    private void ProgressCurrentTechnology()
    {
        if (currentTechnology == technologyRecipeList.Count)
        {
            return;
        }

        var recipe = technologyRecipeList[currentTechnology].recipe;
        
        float progress = (recipe.DefaultResearchProgress) *
                             (1f + 1f * (currentCharacter[SkillType.Knowledge] / 100f));
            
        technologyRecipeList[currentTechnology].progress += progress;
        technologySiteUI.IncreaseResearchSliderProgress(progress);
        
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
            ApplySkillsAndNeedsChanges();
        }
    }
}
