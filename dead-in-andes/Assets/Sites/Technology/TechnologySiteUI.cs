using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechnologySiteUI : MonoBehaviour
{
    [Header("Technology UI Elements")]
    [SerializeField] private Slider technologyProgressSlider;
    [SerializeField] private TextMeshProUGUI technologyName;
    [Header("Technology PopUp Elements")]
    [SerializeField] private TextMeshProUGUI technologyNamePopUp;
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private Image technologySprite;
    
    public void ResetAndSetNextTechnology(string nextTechName)
    {
        technologyProgressSlider.value = 0f;
        technologyName.text = nextTechName;
    }

    public void SetResearchAsFinished()
    {
        // technologyProgressSlider.value = 0f;
        technologyName.text = "COMPLETED";
    }

    public void IncreaseResearchSliderProgress(float researchProgress)
    {
        technologyProgressSlider.value += researchProgress;
    }

    public void OpenPopUpPanel(Recipe recipe)
    {
        EventSystem.current.SetSelectedGameObject(null);
        technologyNamePopUp.text = recipe.recipeName;
        technologySprite.sprite = recipe.recipeIcon;
        GameManager.Instance.IncreaseLockingInteractionCount();
        popUpPanel.SetActive(true);
    }

    public void ClosePopUpPanel()
    {
        GameManager.Instance.DecreaseLockingInteractionCount();
        popUpPanel.SetActive(false);
    }
}