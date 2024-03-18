using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DayStagesController : MonoBehaviour
{
    [SerializeField] private DayStage _dayStage = DayStage.Morning;
    [SerializeField] private int _dayCounter = 0;
    [SerializeField] private List<Character> _characters_list;
    
    void ChangeDayStage()
    {
        switch (_dayStage)
        {
            case DayStage.Morning:
                _dayStage = DayStage.Afternoon;
                break;
            case DayStage.Afternoon:
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
        Debug.Log("siemano bycz");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeDayStage();
            Debug.Log(_dayStage);
            Debug.Log(_dayCounter);
        }
    }
}
