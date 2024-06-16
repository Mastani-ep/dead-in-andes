using UnityEngine;

public class ExplorationSiteManager : MonoBehaviour
{
    [SerializeField] private ExplorationManager explorationManager;

    public void EvaluateExploration()
    {
        explorationManager.EvaluateExploration();
    }
}