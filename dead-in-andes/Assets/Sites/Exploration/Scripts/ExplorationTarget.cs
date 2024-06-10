using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Exploration target", menuName = "Create Exploration site")]
public class ExplorationTarget : ScriptableObject
{
    public string explorationTargetName = "New exploration site";
    public int defaultExplorationProgress = 50;
    public List<ExplorationTargetObject> Items = new();

    // public ExplorationTarget(string explorationTargetName, int defaultExplorationProgress, List<ExplorationTargetObject> items)
    // {
    //     this.explorationTargetName = explorationTargetName;
    //     this.defaultExplorationProgress = defaultExplorationProgress;
    //     this.Items = items;
    // }
    //
    // public object Clone()
    // {
    //     return new ExplorationTarget(explorationTargetName, defaultExplorationProgress, Items);
    //         
    //     'ScriptableObject' instances must be instantiated with 'ScriptableObject.CreateInstance<T>()' instead of 'new'
    //         
    //     //     
    //     //     
    //     // {
    //     //     explorationTargetName = this.explorationTargetName,
    //     //     defaultExplorationProgress = this.defaultExplorationProgress,
    //     //     Items = this.Items
    //     // }; 
    // }
    
}
