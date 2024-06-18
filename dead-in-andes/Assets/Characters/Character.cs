using System;
using Characters;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    [FormerlySerializedAs("_name")] [SerializeField] public string characterName = "lol";
    [TextArea(15,20)]
    public string characterDescription;
    [SerializeField] public bool alive = true;
    public SpriteRenderer characterImage;
    public NeedType causeOfDeath;
    
    [Header("Needs")]    
    [SerializeField] public int hungerMeter = 0;
    [SerializeField] public int fatigueMeter = 0;
    [SerializeField] public int sicknessMeter = 0;
    [SerializeField] public int injuryMeter = 0;
    [SerializeField] public int depressionMeter = 0;
    
    [Header("Skills")]
    [SerializeField] public int gathering;
    [SerializeField] public int hunting;
    [SerializeField] public int fishing;
    [SerializeField] public int cooking;
    [SerializeField] public int communication;
    [SerializeField] public int knowledge;
    [SerializeField] public int craftsmanship;
    [SerializeField] public int exploration;
    [SerializeField] public int constitution;
    [SerializeField] public int strength;

    // Start is called before the first frame update
    void Start()
    {
        if (!characterImage)
        {
            characterImage = GetComponent<SpriteRenderer>();
        }
    }
    
    public int this[SkillType skillType]
    {
        get
        {
            return skillType switch
            {
                SkillType.Gathering => gathering,
                SkillType.Hunting => hunting,
                SkillType.Fishing => fishing,
                SkillType.Cooking => cooking,
                SkillType.Communication => communication,
                SkillType.Knowledge => knowledge,
                SkillType.Craftsmanship => craftsmanship,
                SkillType.Exploration => exploration,
                SkillType.Constitution => constitution,
                SkillType.Strength => strength,
            };
        }
        set
        {
            switch (skillType)
            {
                case SkillType.Gathering:
                    gathering = value;
                    break;
                case SkillType.Hunting:
                    hunting = value;
                    break;
                case SkillType.Fishing:
                    fishing = value;
                    break;
                case SkillType.Cooking:
                    cooking = value;
                    break;
                case SkillType.Communication:
                    communication = value;
                    break;
                case SkillType.Knowledge:
                    knowledge = value;
                    break;
                case SkillType.Craftsmanship:
                    craftsmanship = value;
                    break;
                case SkillType.Exploration:
                    exploration = value;
                    break;
                case SkillType.Constitution:
                    constitution = value;
                    break;
                case SkillType.Strength:
                    strength = value;
                    break;
            }
        }
    }
    
    public int this[NeedType needType]
    {
        get
        {
            return needType switch
            {
                NeedType.Hunger => hungerMeter,
                NeedType.Fatigue => fatigueMeter,
                NeedType.Sickness => sicknessMeter,
                NeedType.Injury => injuryMeter,
                NeedType.Depression => depressionMeter,
            };
        }
        set
        {
            switch (needType)
            {
                case NeedType.Hunger:
                    hungerMeter = value;
                    break;
                case NeedType.Fatigue:
                    fatigueMeter = value;
                    break;
                case NeedType.Sickness:
                    sicknessMeter = value;
                    break;
                case NeedType.Injury:
                    injuryMeter = value;
                    break;
                case NeedType.Depression:
                    depressionMeter = value;
                    break;
            }
        }
    }
    
    public void NextDayChanges()
    {
        if (alive)
        {
            if (hungerMeter >= 100 || fatigueMeter >= 100 || sicknessMeter >= 100 || injuryMeter >= 100 || depressionMeter >= 100)
            {
                alive = false;
            
                if (hungerMeter >= 100)
                    causeOfDeath = NeedType.Hunger;
                else if (fatigueMeter >= 100)
                    causeOfDeath = NeedType.Fatigue;
                else if (sicknessMeter >= 100)
                    causeOfDeath = NeedType.Sickness;
                else if (injuryMeter >= 100)
                    causeOfDeath = NeedType.Injury;
                else if (depressionMeter >= 100)
                    causeOfDeath = NeedType.Depression;
                
                var dragDrop = GetComponent<DragDropCharacter>();
                var site = dragDrop.currentSite;
                dragDrop.currentSite = null;
                site.ChangeCurrentCharacter(null);
                
                gameObject.SetActive(false);
            }
        }
        hungerMeter = Math.Min(hungerMeter + Random.Range(15, 25), 100);
        fatigueMeter = Math.Min(fatigueMeter - Random.Range(10, 15), 100);
        sicknessMeter = Math.Min((int)(sicknessMeter * 1.5), 100);
        injuryMeter = Math.Min((int)(injuryMeter * 1.5), 100);
        depressionMeter = Math.Min(depressionMeter + Random.Range(15, 25), 100);
    }
}
