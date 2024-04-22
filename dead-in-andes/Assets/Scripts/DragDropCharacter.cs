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
        
    }

    public void OnMouseUp()
    {
        if (availableSite)
        {
            changeCurrentSite(availableSite);
        }
        changeColorAlpha(1f);

        isBeingDragged = false;
    }

    public void changeCurrentSite(Site site)
    {
        currentSite.ChangeCurrentCharacter(null);
        currentSite = site;
        print("CHANGED SITE::" + currentSite + site.SiteType);
    }

    public void changeColorAlpha(float alpha)
    {
        Color currentColor = spriteRenderer.color;
        currentColor.a = alpha;
        spriteRenderer.color = currentColor;
    }
}
