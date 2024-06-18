using System;
using Characters;
using UnityEngine;
using Random = UnityEngine.Random;

public class SleepingSite : Site
{
    public override void executeSiteFunctionality()
    {
        if (currentCharacter)
        {
            // Debug.Log("Fatigue decrease" + );
            currentCharacter[NeedType.Fatigue] = Math.Max((int)(currentCharacter[NeedType.Fatigue] - Random.Range(20, 35) * (1 + currentCharacter[SkillType.Constitution] / 100f)), 0); 
            ApplySkillsAndNeedsChanges();
        }
    }
}
