using System.Collections.Generic;
using Characters;
using UnityEngine;

[CreateAssetMenu(fileName = "new Exploration target", menuName = "Create Exploration site")]
public class ExplorationTarget : ScriptableObject
{
    public string explorationTargetName = "New exploration site";
    public float defaultExplorationProgress = 50;
    public List<ExplorationTargetObject> Items = new();
    public SkillType usefulSkillType;
}
