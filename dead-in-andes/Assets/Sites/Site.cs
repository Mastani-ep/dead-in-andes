using System.Collections.Generic;
using Characters;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Site : MonoBehaviour
{
    [SerializeField] protected SiteType _site_type;
    [SerializeField] protected Character currentCharacter;
    [SerializeField] private GameObject connectedObject;
    public Character CurrentCharacter => currentCharacter;
    [SerializeField] private SkillType usefulSkillType;
    [SerializeField] private List<NeedType> needTypesImpacted;

    public SiteType SiteType => _site_type;

    [SerializeField] private Animator animator;
    // public string getSiteType()
    // {
        
    // }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void OnEnable()
    {
        if (!animator)
        {
            animator = gameObject.GetComponent<Animator>();
        }

        if (connectedObject)
        {
            connectedObject.SetActive(true);
        }
    }
    
    public void playFlashingAnimation()
    {
        animator.Play("Flashing");
    }
    
    public void stopFlashingAnimation()
    {
        animator.Play("NormalState");
    }
    
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
    
    public virtual void executeSiteFunctionality()
    {
        // Debug.Log("This should never happen");
    }

    public void ApplySkillsAndNeedsChanges()
    {
        ApplySkillImprovement();
        ApplyNeedsIncreases();
    }
    
    public void ApplySkillImprovement()
    {
        currentCharacter[usefulSkillType] = Mathf.Clamp(currentCharacter[usefulSkillType] + Random.Range(1, 3), 0, 100);
    }

    public void ApplyNeedsIncreases()
    {
        foreach (var needType in needTypesImpacted)
        {
            currentCharacter[needType] = Mathf.Clamp(currentCharacter[needType] + Random.Range(5, 15), 0, 100);
        }
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
