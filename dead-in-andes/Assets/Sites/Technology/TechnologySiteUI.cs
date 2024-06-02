using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechnologySiteUI : MonoBehaviour
{
    [Header("Technology UI Elements")]
    [SerializeField] private Slider technologyProgressSlider;
    [SerializeField] private TextMeshProUGUI technologyName;
    
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

    public void IncreaseResearchSliderProgress(int researchProgress)
    {
        technologyProgressSlider.value += researchProgress;
    }
}