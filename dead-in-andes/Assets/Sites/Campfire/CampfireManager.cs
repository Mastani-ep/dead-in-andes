using System.Collections.Generic;
using Characters;
using UnityEngine;

public class CampfireManager : MonoBehaviour
{
    [SerializeField] private List<Character> charactersOnSite;
    [SerializeField] private static CampfireManager instance;
    
    [Tooltip("The percentage of total communication impact per person that affects the depression reduction")]
    [SerializeField] private float depressionReductionPerPerson = 1f;

    public static CampfireManager Instance
    {
        get => instance;
        set => instance = value;
    }
    
    public void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void AppendToCharacterList(Character character)
    {
        charactersOnSite.Add(character);
    }
    
    private void ClearCharacterList()
    {
        charactersOnSite.Clear();
    }

    public void EvaluateImpactOnCharacters()
    {
        if (charactersOnSite.Count <= 1)
        {
            ClearCharacterList();
            return;
        }
        int totalCharacterCommunicationLevel = 0;

        foreach(var character in charactersOnSite)
        {
            totalCharacterCommunicationLevel += character[SkillType.Communication];
        }

        float depressionReduction = ((totalCharacterCommunicationLevel * depressionReductionPerPerson) / charactersOnSite.Count);

        foreach(var character in charactersOnSite)
        {
            // Adding impact of communication skill on depression reduction ->
            // the less communication the less depression reduction
            var depressionReductionCalculated = depressionReduction * (character[SkillType.Communication] * 0.02f) + depressionReduction/2f;
            depressionReductionCalculated += 0.2f * depressionReductionCalculated * (charactersOnSite.Count - 1);
            // Apply depression reduction
            character[NeedType.Depression] = Mathf.Clamp(character[NeedType.Depression] - (int)depressionReductionCalculated, 0, 100);
            character[NeedType.Fatigue] = Mathf.Clamp(character[NeedType.Fatigue] - Random.Range(5, 10), 0, 100);
        }

        ClearCharacterList();
    }
}
