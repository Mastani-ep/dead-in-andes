using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CampfireSite : Site
{
    // [SerializeField] private string _site_type = "Campfire";
    private void Awake()
    {
        _site_type = SiteType.Campfire;
    }
    
    public override void executeSiteFunctionality()
    {
        if (currentCharacter)
        {
            CampfireManager.Instance.AppendToCharacterList(currentCharacter);
            ApplySkillsAndNeedsChanges();
        }
    }
}
