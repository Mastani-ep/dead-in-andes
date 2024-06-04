using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragDropCharacter : MonoBehaviour
{

    [SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;
    public bool isBeingDragged = false;
    public SpriteRenderer spriteRenderer;
    [Header("Site")] 
    public Site currentSite;
    public Site availableSite;
    private void Awake()
    {
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (currentSite)
        {
            transform.position = currentSite.transform.position;
            availableSite = currentSite;
            currentSite.ChangeCurrentCharacter(GetComponent<Character>());
        }
    }
    
    private void Update()
    {
        if (!isBeingDragged)
        {
            transform.position = currentSite.transform.position;
        }
        else
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }

    public void OnMouseDown()
    {
        // offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        changeColorAlpha(0.6f);
        isBeingDragged = true;
        foreach(var site in FindObjectsOfType<Site>())
        {
            site.playFlashingAnimation();
        }
        
    }

    public void OnMouseUp()
    {
        foreach(var site in FindObjectsOfType<Site>())
        {
            site.stopFlashingAnimation();
        }
        if (availableSite)
        {
            if (availableSite.CurrentCharacter)
            {
                switchSitesBetweenCharacters(availableSite);
            }
            else
            {
                changeCurrentSite(availableSite);
            }
        }
        changeColorAlpha(1f);

        isBeingDragged = false;
    }

    public void switchSitesBetweenCharacters(Site site)
    {
        availableSite.CurrentCharacter.gameObject.GetComponent<DragDropCharacter>()
            .changeCurrentSite(currentSite);
        currentSite = site;
        currentSite.ChangeCurrentCharacter(GetComponent<Character>());
    }
    public void changeCurrentSite(Site site)
    {
        currentSite.ChangeCurrentCharacter(null);
        currentSite = site;
        currentSite.ChangeCurrentCharacter(GetComponent<Character>());
        print("CHANGED SITE::" + currentSite + site.SiteType);
    }

    public void changeColorAlpha(float alpha)
    {
        Color currentColor = spriteRenderer.color;
        currentColor.a = alpha;
        spriteRenderer.color = currentColor;
    }
}
