using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WorkshopUI : MonoBehaviour
{
    public Workshop Workshop;
    public Transform RecipesContent;
    public GameObject RecipeItem;
    public GameObject WorkshopUIPanel;
    public GameObject WorkshopUIButton;
    
    [Header("Recipe Description Elements")]
    public Transform RecipeIngredientsContent;
    public GameObject RecipeObjectItem;
    public Slider RecipeProgress;
    public Image RecipeImage;
    public TextMeshProUGUI RecipeName;
    public Button recipeButton;
    // private void Awake()
    // {
    //     Instance = this;
    // }

    // public void Add(ItemObject item)
    // {
    //     if (Items.ContainsKey(item))
    //     {
    //         // If it exists, increment its value
    //         Items[item]++;
    //     }
    //     else
    //     {
    //         // If it doesn't exist, add it with value 1
    //         Items[item] = 1;
    //     }
    // }
    //
    // public void Remove(ItemObject item, int count)
    // {
    //     if (Items[item] > count)
    //     {
    //         Items[item] -= count;
    //     }
    //     else
    //     {
    //         Items.Remove(item);
    //     }
    // }

    public void ListRecipes()
    {
        foreach (var recipeProgress in Workshop.UnlockedRecipes)
        {
            GameObject obj = Instantiate(RecipeItem, RecipesContent);
            Button recipeButton = obj.transform.Find("RecipeButton").GetComponent<Button>();
            var itemName = obj.transform.Find("RecipeButton/RecipeText").GetComponent<TMPro.TextMeshProUGUI>();
            var doneMask = obj.transform.Find("RecipeButton/DoneRecipeImage");
            var itemIcon = obj.transform.Find("RecipeButton/RecipeImage").GetComponent<Image>();
            itemIcon.sprite = recipeProgress.recipe.recipeIcon;
            if (!recipeProgress.isDone)
            {
                itemName.text = recipeProgress.recipe.recipeName;
                recipeButton.onClick.AddListener(() => { ClickRecipe(recipeProgress); });
            }
            else
            {
                doneMask.gameObject.SetActive(true);
                recipeButton.enabled = false;
                itemName.text = "FINISHED";
            }
        }
    }

    public void ClickRecipe(RecipeProgress recipeProgress)
    {
        UnlistRecipesIngredients();
        RecipeImage.sprite = recipeProgress.recipe.recipeIcon;
        RecipeName.text = recipeProgress.recipe.recipeName;
        RecipeProgress.value = recipeProgress.progress;
        
        foreach (var item in recipeProgress.recipe.requiredItems)
        {
            GameObject obj = Instantiate(RecipeObjectItem, RecipeIngredientsContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemCount = obj.transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            
            itemName.text = item.requiredItem.itemName;
            itemCount.text = item.requiredAmount.ToString();
            itemIcon.sprite = item.requiredItem.itemIcon;
            recipeButton.onClick.RemoveAllListeners();
            recipeButton.onClick.AddListener(() =>
            {
                choseRecipe(recipeProgress);
            });
            
        }
        
    }

    public void choseRecipe(RecipeProgress recipeProgress)
    {
        Workshop.changeCurrentRecipe(recipeProgress);
        ToggleWorkshopUI();
    }

    public void UnlistRecipesIngredients()
    {
        foreach (Transform item in RecipeIngredientsContent)
        {
            Destroy(item.gameObject);
        }
    }
    
    public void UnlistRecipes()
    {
        foreach (Transform recipe in RecipesContent)
        {
            Destroy(recipe.gameObject);
        }
    }

    public void ToggleWorkshopUI()
    {
        if (WorkshopUIPanel.activeSelf)
        {
            WorkshopUIPanel.SetActive(false);
            WorkshopUIButton.SetActive(true);
            UnlistRecipes();
        }
        else
        {
            WorkshopUIPanel.SetActive(true);
            WorkshopUIButton.SetActive(false);
            ListRecipes();
            if (Workshop.currentRecipe != -1)
            {
                ClickRecipe(Workshop.UnlockedRecipes[Workshop.currentRecipe]);
            }
            else
            {
                ClickRecipe(Workshop.UnlockedRecipes.FirstOrDefault(obj => obj.isDone == false));
            }
        }
    }
}
