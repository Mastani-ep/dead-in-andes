using Characters;
using Random = UnityEngine.Random;

public class SleepingSite : Site
{
    public override void executeSiteFunctionality()
    {
        if (currentCharacter)
        {
            currentCharacter[NeedType.Fatigue] -= (int)(Random.Range(10, 20) * (1 + currentCharacter[SkillType.Constitution] / 100f)); 
            ApplySkillsAndNeedsChanges();
        }
    }
}
