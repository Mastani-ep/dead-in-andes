using System.Net.Mime;
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

    [Header("Generel UI variables")] 
    [SerializeField] private Gradient sliderGradient;
    // [Header("Skills Screen")]
    
    void Start()
    {
        charactersList = Object.FindObjectsOfType<Character>();
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
        ReloadGeneralScreen();
        ReloadSkillsScreen();
    }

    private void ReloadGeneralScreen()
    {
        var character = charactersList[currentCharacter];
        characterName.text = character.characterName;
        characterSprite.sprite = charactersList[currentCharacter].GetComponent<SpriteRenderer>().sprite;
        // characterBiography.text = 

        hungerSlider.value = character.hungerMeter;
        hungerSlider.fillRect.GetComponent<Image>().color = sliderGradient.Evaluate(character.hungerMeter/100f);
        fatigueSlider.value = character.fatigueMeter;
        fatigueSlider.fillRect.GetComponent<Image>().color = sliderGradient.Evaluate(character.fatigueMeter/100f);
        sicknessSlider.value = character.sicknessMeter;
        sicknessSlider.fillRect.GetComponent<Image>().color = sliderGradient.Evaluate(character.sicknessMeter/100f);
        depressionSlider.value = character.depressionMeter;
        depressionSlider.fillRect.GetComponent<Image>().color = sliderGradient.Evaluate(character.depressionMeter/100f);
        injurySlider.value = character.injuryMeter;
        injurySlider.fillRect.GetComponent<Image>().color = sliderGradient.Evaluate(character.injuryMeter/100f);

        hungerPercentageValue.text = character.hungerMeter + " %";
        fatiguePercentageValue.text = character.fatigueMeter + " %";
        sicknessPercentageValue.text = character.sicknessMeter + " %";
        depressionPercentageValue.text = character.depressionMeter + " %";
        injuryPercentageValue.text = character.injuryMeter + " %";
    }

    private void ReloadSkillsScreen()
    {
        //
    }
    
    public void OpenCharacterScreen()
    {
        characterScreen.SetActive(true);
        inventoryButton.SetActive(false);
        characterScreenButton.SetActive(false);
        ReloadScreens();
    }
    
    public void CloseCharacterScreen()
    {
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
