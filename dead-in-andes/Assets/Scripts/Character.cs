using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string _name = "lol";
    [SerializeField] private bool _alive = true;

    [Header("Needs")]    
    [SerializeField] private int _hungerMeter = 0;
    [SerializeField] private int _fatigueMeter = 0;
    [SerializeField] private int _sicknessMeter = 0;


    public void NextDayChanges()
    {
        if (_alive)
        {
            _hungerMeter += 1;
            _fatigueMeter += 1;
            _sicknessMeter += 1;

            if (_hungerMeter <= 10)
            {
                _alive = false;
            }
        }
    }
    
    
    // [SerializeField] private string _sex;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
