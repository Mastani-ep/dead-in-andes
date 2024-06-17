using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class DayStagesController : MonoBehaviour
{
    [SerializeField] private DayStage _dayStage = DayStage.Morning;
    [SerializeField] private int _dayCounter = 0;
    [SerializeField] private Character[] _characters_list;
    [SerializeField] private Site[] allSites;
    [SerializeField] private ExplorationSiteManager explorationSiteManager;

    [Header("Day Stage UI")] 
    [SerializeField] private Sprite morningSprite;
    [SerializeField] private Sprite afternoonSprite;
    [SerializeField] private Sprite nightSprite;
    [SerializeField] private TextMeshProUGUI dayStageText;
    [SerializeField] private TextMeshProUGUI dayCounterText;
    [SerializeField] private Image dayStageImage;
    
    [Header("Light and surroundings parameters")]
    public Light2D globalLight;
    public List<SpriteRenderer> skies;
    public List<SpriteRenderer> mountains;
    public string morningColor = "#FFFFFF";
    public string afternoonColor = "#A1742E";
    public string nightColor = "#FFFFFF";
    public float nightIntensity = 0.1f;
    public string mountainNightColor = "#C1BDBD";
    public string skyNightColor = "#3F4054";
    
    void ChangeDayStage()
    {
        switch (_dayStage)
        {
            case DayStage.Morning:
                foreach (var site in allSites)
                {
                    site.executeSiteFunctionality();
                }
                explorationSiteManager.EvaluateExploration();
                CampfireManager.Instance.EvaluateImpactOnCharacters();
                _dayStage = DayStage.Afternoon;
                UpdateDayStageUI();
                ChangeSceneLight();
                break;
            case DayStage.Afternoon:
                foreach (var site in allSites)
                {
                    site.executeSiteFunctionality();
                }
                explorationSiteManager.EvaluateExploration();
                CampfireManager.Instance.EvaluateImpactOnCharacters();
                _dayStage = DayStage.Evening;
                UpdateDayStageUI();
                ChangeSceneLight();
                break;
            case DayStage.Evening:
                _dayStage = DayStage.Morning;
                UpdateDayStageUI();
                ChangeSceneLight();
                NextDayChanges();
                break;
        }
    }

    void NextDayChanges()
    {
        _dayCounter += 1;
        foreach (var character in _characters_list)
        {
            character.NextDayChanges();
        }
        _characters_list = _characters_list.Where(character => character.alive).ToArray();
        UpdateDayCounter();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        allSites = Object.FindObjectsOfType<Site>();
        _characters_list = Object.FindObjectsOfType<Character>();
        UpdateDayCounter();
        UpdateDayStageUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.LockedParallaxScrolling == 0)
        {
            ChangeDayStage();
            Debug.Log(_dayStage);
            Debug.Log("Day" + _dayCounter);
        }
    }

    public void UpdateDayCounter()
    {
        dayCounterText.text = "Day " + _dayCounter;
    }

    public void UpdateDayStageUI()
    {
        dayStageText.text = _dayStage.ToString();
        switch (_dayStage)
        {
            case DayStage.Morning:
                dayStageImage.sprite = morningSprite;
                break;
            case DayStage.Afternoon:
                dayStageImage.sprite = afternoonSprite;
                break;
            case DayStage.Evening:
                dayStageImage.sprite = nightSprite;
                break;
        }

    }

    public void ChangeSceneLight()
    {
        Color newColor;
        switch (_dayStage)
        {
            case DayStage.Morning:
                ColorUtility.TryParseHtmlString(morningColor, out newColor);
                globalLight.color = newColor;
                globalLight.intensity = 1.0f;
                ColorUtility.TryParseHtmlString("#FFFFFF", out newColor);
                foreach (var sky in skies)
                {
                    sky.color = newColor;
                }
                ColorUtility.TryParseHtmlString("#FFFFFF", out newColor);
                foreach (var mountain in mountains)
                {
                    mountain.color = newColor;
                }
                break;
            case DayStage.Afternoon:
                ColorUtility.TryParseHtmlString(afternoonColor, out newColor);
                globalLight.color = newColor;
                break;
            case DayStage.Evening:
                ColorUtility.TryParseHtmlString(nightColor, out newColor);
                globalLight.color = newColor;
                globalLight.intensity = nightIntensity;
                ColorUtility.TryParseHtmlString(skyNightColor, out newColor);
                foreach (var sky in skies)
                {
                    sky.color = newColor;
                }
                ColorUtility.TryParseHtmlString(mountainNightColor, out newColor);
                foreach (var mountain in mountains)
                {
                    mountain.color = newColor;
                }
                break;
        }
    }
}
