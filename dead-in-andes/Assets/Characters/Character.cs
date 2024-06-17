using Characters;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    [FormerlySerializedAs("_name")] [SerializeField] public string characterName = "lol";
    [TextArea(15,20)]
    public string characterDescription;
    [SerializeField] private bool alive = true;
    public SpriteRenderer characterImage;
    // [SerializeField] public Sprite characterSprite;
    
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
            hungerMeter += 15;
            fatigueMeter += 25;
            sicknessMeter += 10;

            if (hungerMeter >= 100)
            {
                alive = false;
            }
        }
    }
}
