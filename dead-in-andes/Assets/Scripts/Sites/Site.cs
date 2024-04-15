using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Site : MonoBehaviour, IDropHandler
{
    [SerializeField] protected string _site_type;
    [SerializeField] protected Character currentCharacter;
    
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<DragDropCharacter>().changeCurrentSite(this);
            ChangeCurrentCharacter(eventData.pointerDrag.GetComponent<Character>());
        }
    }

    public string SiteType => _site_type;
    // public string getSiteType()
    // {
        
    // }

    public void ChangeCurrentCharacter(Character character)
    {
        currentCharacter = character;
    }
    
    public virtual void generateItem()
    {
        // Debug.Log("This should never happen");
    }
}
