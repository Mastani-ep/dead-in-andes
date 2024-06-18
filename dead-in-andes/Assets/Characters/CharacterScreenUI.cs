using System.Collections.Generic;
using System.Linq;
using Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScreenUI : MonoBehaviour
{
    [Header("Shared")]
    [SerializeField] private int currentCharacter = 0;
    [SerializeField] private Character[] charactersList;
    [SerializeField] private GameObject characterScreen;
    [SerializeField] private GameObject generalScreen;
    [SerializeField] private GameObject skillsScreen;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject characterScreenButton;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSprite;
    
    [Header("General Screen")]
    [SerializeField] private TextMeshProUGUI characterBiography;
    
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider fatigueSlider;
    [SerializeField] private Slider sicknessSlider;
    [SerializeField] private Slider depressionSlider;
    [SerializeField] private Slider injurySlider;
    
    [SerializeField] private TextMeshProUGUI hungerPercentageValue;
    [SerializeField] private TextMeshProUGUI fatiguePercentageValue;
    [SerializeField] private TextMeshProUGUI sicknessPercentageValue;
    [SerializeField] private TextMeshProUGUI depressionPercentageValue;
    [SerializeField] private TextMeshProUGUI injuryPercentageValue;

    [Header("Skills Screen")]
    [SerializeField] private Slider constitutionSlider;
    [SerializeField] private Slider conversationSlider;
    [SerializeField] private Slider knowledgeSlider;
    [SerializeField] private Slider gatheringSlider;
    [SerializeField] private Slider craftingSlider;
    [SerializeField] private Slider exploringSlider;
    [SerializeField] private Slider cookingSlider;
    [SerializeField] private Slider huntingSlider;
    [SerializeField] private Slider fishingSlider;
    [SerializeField] private Slider strengthSlider;
    
    [SerializeField] private TextMeshProUGUI constitutionPercentageValue;
    [SerializeField] private TextMeshProUGUI conversationPercentageValue;
    [SerializeField] private TextMeshProUGUI knowledgePercentageValue;
    [SerializeField] private TextMeshProUGUI gatheringPercentageValue;
    [SerializeField] private TextMeshProUGUI craftingPercentageValue;
    [SerializeField] private TextMeshProUGUI exploringPercentageValue;
    [SerializeField] private TextMeshProUGUI cookingPercentageValue;
    [SerializeField] private TextMeshProUGUI huntingPercentageValue;
    [SerializeField] private TextMeshProUGUI fishingPercentageValue;
    [SerializeField] private TextMeshProUGUI strengthPercentageValue;
    
    [Header("General UI variables")] 
    [SerializeField] private Gradient needsSliderGradient;
    
    void Start()
    {
        charactersList = Object.FindObjectsOfType<Character>().Where(character => character.alive).ToArray();
        ReloadScreens();
    }

    public void ShowPrevCharacter()
    {
        if (currentCharacter == 0)
        {
            currentCharacter = charactersList.Length - 1;
        }
        else
        {
            currentCharacter--;
        }
        ReloadScreens();
    }
    
    public void ShowNextCharacter()
    {
        if (currentCharacter == charactersList.Length - 1)
        {
            currentCharacter = 0;
        }
        else
        {
            currentCharacter++;
        }
        ReloadScreens();
    }

    private void ReloadScreens()
    {
        var character = charactersList[currentCharacter];
        
        characterName.text = character.characterName;
        characterSprite.sprite = charactersList[currentCharacter].GetComponent<SpriteRenderer>().sprite;
        characterBiography.text = character.characterDescription;
        
        ReloadGeneralScreen();
        ReloadSkillsScreen();
    }

    private void ReloadGeneralScreen()
    {
        var character = charactersList[currentCharacter];

        hungerSlider.value = character[NeedType.Hunger];
        hungerSlider.fillRect.GetComponent<Image>().color = needsSliderGradient.Evaluate(character[NeedType.Hunger]/100f);
        fatigueSlider.value = character[NeedType.Fatigue];
        fatigueSlider.fillRect.GetComponent<Image>().color = needsSliderGradient.Evaluate(character[NeedType.Fatigue]/100f);
        sicknessSlider.value = character[NeedType.Sickness];
        sicknessSlider.fillRect.GetComponent<Image>().color = needsSliderGradient.Evaluate(character[NeedType.Sickness]/100f);
        depressionSlider.value = character[NeedType.Depression];
        depressionSlider.fillRect.GetComponent<Image>().color = needsSliderGradient.Evaluate(character[NeedType.Depression]/100f);
        injurySlider.value = character[NeedType.Injury];
        injurySlider.fillRect.GetComponent<Image>().color = needsSliderGradient.Evaluate(character[NeedType.Injury]/100f);

        hungerPercentageValue.text = character[NeedType.Hunger] + " %";
        fatiguePercentageValue.text = character[NeedType.Fatigue] + " %";
        sicknessPercentageValue.text = character[NeedType.Sickness] + " %";
        depressionPercentageValue.text = character[NeedType.Depression] + " %";
        injuryPercentageValue.text = character[NeedType.Injury] + " %";
    }

    private void ReloadSkillsScreen()
    {
        var character = charactersList[currentCharacter];
     
        constitutionSlider.value = character[SkillType.Constitution];
        conversationSlider.value = character[SkillType.Communication];
        knowledgeSlider.value = character[SkillType.Knowledge];
        gatheringSlider.value = character[SkillType.Gathering];
        craftingSlider.value = character[SkillType.Craftsmanship];
        exploringSlider.value = character[SkillType.Exploration];
        cookingSlider.value = character[SkillType.Cooking];
        huntingSlider.value = character[SkillType.Hunting];
        fishingSlider.value = character[SkillType.Fishing];
        strengthSlider.value = character[SkillType.Strength];
        
        constitutionPercentageValue.text = character[SkillType.Constitution] + " %";
        conversationPercentageValue.text = character[SkillType.Communication] + " %";
        knowledgePercentageValue.text = character[SkillType.Knowledge] + " %";
        gatheringPercentageValue.text = character[SkillType.Gathering] + " %";
        craftingPercentageValue.text = character[SkillType.Craftsmanship] + " %";
        exploringPercentageValue.text = character[SkillType.Exploration] + " %";
        cookingPercentageValue.text = character[SkillType.Cooking] + " %";
        huntingPercentageValue.text = character[SkillType.Hunting] + " %";
        fishingPercentageValue.text = character[SkillType.Fishing] + " %";
        strengthPercentageValue.text = character[SkillType.Strength] + " %";
        
    }
    
    public void OpenCharacterScreen()
    {
        GameManager.Instance.IncreaseLockingInteractionCount();
        characterScreen.SetActive(true);
        inventoryButton.SetActive(false);
        characterScreenButton.SetActive(false);
        ReloadScreens();
    }
    
    public void CloseCharacterScreen()
    {
        GameManager.Instance.DecreaseLockingInteractionCount();
        characterScreen.SetActive(false);
        inventoryButton.SetActive(true);
        characterScreenButton.SetActive(true);
    }

    public void OpenGeneralScreen()
    {
        generalScreen.SetActive(true);
        skillsScreen.SetActive(false);
    }
    
    public void OpenSkillsScreen()
    {
        generalScreen.SetActive(false);
        skillsScreen.SetActive(true);
    }
    
}
