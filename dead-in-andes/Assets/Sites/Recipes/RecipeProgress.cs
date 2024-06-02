using System;

// Probably something better for dictionary serialization could be done but IDK
// It will not show progress properly in Unity Editor
// [Serializable]
// public class RecipeProgress : Dictionary<Recipe, int>
// {
//     [SerializeField] 
//     RecipeProgressItem[] recipeProgressList;
//
//     public Dictionary<Recipe, int> ToDictionary()
//     {
//         Dictionary<Recipe, int> newDict = new Dictionary<Recipe, int>();
//
//         foreach (var item in recipeProgressList)
//         {
//             newDict.Add(item.recipe, item.progress);
//         }
//
//         return newDict;
//     }
// }

[Serializable]
public class RecipeProgress
{
    public Recipe recipe;
    public int progress;
    public bool isDone;
    public bool isSelected;

    public RecipeProgress(Recipe recipe)
    {
        this.recipe = recipe;
        this.progress = 0;
        this.isDone = false;
        this.isSelected = false;
    }
}