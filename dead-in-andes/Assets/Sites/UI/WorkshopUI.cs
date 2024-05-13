using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorkshopUI : MonoBehaviour
{
    public Workshop Workshop;
    public Transform RecipesContent;
    public GameObject RecipeItem;
    public GameObject WorkshopUIPanel;
    public GameObject WorkshopUIButton;
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
            Debug.Log(obj.transform.Find("RecipeButton/RecipeText"));
            Debug.Log(obj.transform.Find("RecipeButton/RecipeImage"));
            var itemName = obj.transform.Find("RecipeButton/RecipeText").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("RecipeButton/RecipeImage").GetComponent<Image>();

            itemName.text = recipeProgress.recipe.recipeName;
            itemIcon.sprite = recipeProgress.recipe.recipeIcon;
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
        }
    }
}
