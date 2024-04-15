using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropCharacter : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    [SerializeField] private Canvas canvas;
    private RectTransform _rectTransform;
    private CanvasGroup canvasGroup;
    public bool isBeingDragged = false;
    [Header("Site")] 
    [SerializeField] private Site currentSite;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (currentSite)
        {
            GetComponent<RectTransform>().anchoredPosition = currentSite.GetComponent<RectTransform>().anchoredPosition;
            currentSite.ChangeCurrentCharacter(GetComponent<Character>());
        }
    }
    
    private void Update()
    {
        if (!isBeingDragged)
        {
            GetComponent<RectTransform>().anchoredPosition = currentSite.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        isBeingDragged = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        isBeingDragged = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //Debug.Log("OnDrag");
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
    }

    public void changeCurrentSite(Site site)
    {
        currentSite.ChangeCurrentCharacter(null);
        currentSite = site;
        print("CHANGED SITE::" + currentSite + site.SiteType);
    }
    
    public void resetToPreviousSite()
    {
        GetComponent<RectTransform>().anchoredPosition = currentSite.GetComponent<RectTransform>().anchoredPosition;
    }
}
