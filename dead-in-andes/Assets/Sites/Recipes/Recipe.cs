using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new Recipe", menuName = "Create Recipe/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName = "New Recipe";
    public SiteType createdSiteType;
    public List<Ingridient> requiredItems = new ();
    [FormerlySerializedAs("_defaultProgress")] public int _defaultBuildProgress = 50;
    public int _defaultResearchProgress = 50;
    public Sprite recipeIcon;
    public int DefaultBuildProgress
    {
        get { return _defaultBuildProgress; }
        set
        {
            if (value > 0)
            {
                _defaultBuildProgress = value;
            }
            else
            {
                throw new ArgumentException("Default build progress must be greater than 0.");
            }
        }
    }
    
    public int DefaultResearchProgress
    {
        get { return _defaultResearchProgress; }
        set
        {
            if (value > 0)
            {
                _defaultResearchProgress = value;
            }
            else
            {
                throw new ArgumentException("Default research progress must be greater than 0.");
            }
        }
    }
    
}
