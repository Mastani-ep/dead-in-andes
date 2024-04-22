using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Site : MonoBehaviour
{
    [SerializeField] protected string _site_type;
    [SerializeField] protected Character currentCharacter;

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
