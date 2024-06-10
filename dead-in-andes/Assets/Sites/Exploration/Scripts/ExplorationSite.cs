using UnityEngine;

public class ExplorationSite : Site
{
    [SerializeField] private ExplorationManager explorationManager;
    
    public override void executeSiteFunctionality()
    {
        if (currentCharacter)
        {
            explorationManager.ProgressExploration();
        }
    }
}
