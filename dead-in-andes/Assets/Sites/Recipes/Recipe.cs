using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "new Recipe", menuName = "Create Recipe/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName = "New Recipe";
    public SiteType createdSiteType;
    public List<Ingridient> requiredItems = new ();
    public int _defaultProgress = 50;
    public int defaultProgress
    {
        get { return _defaultProgress; }
        set
        {
            if (value > 0)
            {
                _defaultProgress = value;
            }
            else
            {
                throw new ArgumentException("Default progress must be greater than 0.");
            }
        }
    }
}
