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
    
    [SerializeField] private TextMeshProUGUI hungerPercentageValue;
    [SerializeField] private TextMeshProUGUI fatiguePercentageValue;
    [SerializeField] private TextMeshProUGUI sicknessPercentageValue;
    
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
        // characterSprite.sprite = charactersList[currentCharacter].characterSprite;
        // characterBiography.text = 

        hungerSlider.value = character.hungerMeter;
        fatigueSlider.value = character.fatigueMeter;
        sicknessSlider.value = character.sicknessMeter;

        hungerPercentageValue.text = character.hungerMeter + " %";
        fatiguePercentageValue.text = character.fatigueMeter + " %";
        sicknessPercentageValue.text = character.sicknessMeter + " %";
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
