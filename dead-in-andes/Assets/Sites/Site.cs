using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class Site : MonoBehaviour
{
    [SerializeField] protected SiteType _site_type;
    [SerializeField] protected Character currentCharacter;

    public SiteType SiteType => _site_type;
    // public string getSiteType()
    // {
        
    // }

    public void ChangeCurrentCharacter(Character character)
    {
        currentCharacter = character;
        if (currentCharacter)
        {
            ShowUI();
        }
        else
        {
            HideUI();
        }
    }

    public virtual void ShowUI()
    {
        // function for showing UI e.g. button for certain sites like workshop
    }
    
    public virtual void HideUI()
    {
        // function for showing UI e.g. button for certain sites like workshop
    }
    
    public virtual void generateItem()
    {
        // Debug.Log("This should never happen");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entering " + gameObject.name);
        if (other.CompareTag("Character"))
        {
            other.GetComponent<DragDropCharacter>().availableSite = this;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exiting " + gameObject.name);
        if (other.CompareTag("Character"))
        {

            other.GetComponent<DragDropCharacter>().availableSite = null;
        }
    }
}
