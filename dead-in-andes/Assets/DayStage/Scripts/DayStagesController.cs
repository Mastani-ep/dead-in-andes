using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DayStagesController : MonoBehaviour
{
    [SerializeField] private DayStage _dayStage = DayStage.Morning;
    [SerializeField] private int _dayCounter = 0;
    [SerializeField] private Character[] _characters_list;
    [SerializeField] private Site[] allSites;
    [SerializeField] private ExplorationSiteManager explorationSiteManager;
    
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
                _dayStage = DayStage.Afternoon;
                break;
            case DayStage.Afternoon:
                foreach (var site in allSites)
                {
                    site.executeSiteFunctionality();
                }
                explorationSiteManager.EvaluateExploration();
                _dayStage = DayStage.Evening;
                break;
            case DayStage.Evening:
                _dayStage = DayStage.Morning;
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
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        allSites = Object.FindObjectsOfType<Site>();
        _characters_list = Object.FindObjectsOfType<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeDayStage();
            Debug.Log(_dayStage);
            Debug.Log("Day" + _dayCounter);
        }
    }
}
