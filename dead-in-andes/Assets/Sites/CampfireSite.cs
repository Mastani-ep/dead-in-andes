using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CampfireSite : Site
{
    // [SerializeField] private string _site_type = "Campfire";
    private void Start()
    {
        _site_type = SiteType.Campfire;
    }
}
