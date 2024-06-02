using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
    [FormerlySerializedAs("RecipeProgress")] public Slider RecipeProgressSlider;
    public Image RecipeImage;
    public TextMeshProUGUI RecipeName;
    public Button recipeButton;

    public GameObject inventoryButton;
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
                if (recipeProgress.isSelected)
                {
                    obj.transform.Find("SelectedUnderlay").gameObject.SetActive(true);
                }
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
        recipeButton.onClick.RemoveAllListeners();
        UnlistRecipesIngredients();
        if (recipeProgress == null)
        {
            RecipeImage.sprite = null;
            RecipeName.text = null;
            RecipeProgressSlider.value = 0f;
            recipeButton.enabled = false;
            recipeButton.interactable = false;
        }
        else
        {
            RecipeImage.sprite = recipeProgress.recipe.recipeIcon;
            RecipeName.text = recipeProgress.recipe.recipeName;
            RecipeProgressSlider.value = recipeProgress.progress;

            foreach (var item in recipeProgress.recipe.requiredItems)
            {
                GameObject obj = Instantiate(RecipeObjectItem, RecipeIngredientsContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
                var itemCount = obj.transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

                itemName.text = item.requiredItem.itemName;
                itemCount.text = item.requiredAmount.ToString();
                itemIcon.sprite = item.requiredItem.itemIcon;

                // if (can craft)
                // {
                //     recipeButton.onClick.AddListener(() =>
                //     {
                //         choseRecipe(recipeProgress);
                //     });    
                // }
                // else
                // {
                //     buton disabled so sad
                // }
            }
            
            if (Workshop.CheckRecipeRequirements(Workshop.UnlockedRecipes.IndexOf(recipeProgress)))
            {
                recipeButton.onClick.AddListener(() => { ChoseRecipe(recipeProgress); });
                recipeButton.enabled = true;
                recipeButton.interactable = true;
            }
            else
            {
                recipeButton.enabled = false;
                recipeButton.interactable = false;
            }
        }
    }

    public void ChoseRecipe(RecipeProgress recipeProgress)
    {
        Workshop.ChangeCurrentRecipe(recipeProgress);
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
            inventoryButton.SetActive(true);
            UnlistRecipes();
        }
        else
        {
            WorkshopUIPanel.SetActive(true);
            WorkshopUIButton.SetActive(false);
            ListRecipes();
            if (Workshop.currentRecipe != -1 && Workshop.UnlockedRecipes.Count > 0)
            {
                ClickRecipe(Workshop.UnlockedRecipes[Workshop.currentRecipe]);
            }
            else
            {
                var recipe = Workshop.UnlockedRecipes.FirstOrDefault(obj => obj.isDone == false);
                ClickRecipe(recipe);
            }

            inventoryButton.SetActive(false);
        }
    }
}