using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    [SerializeField] private static GameManager instance;
    [SerializeField] private GameObject PauseGameMenu;
    [SerializeField] private GameObject RadioStationSite;
    [SerializeField] private GameObject EndGameNotificationWin;
    [SerializeField] private GameObject EndGameNotificationLoss;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform characterContentWin;
    [SerializeField] private Transform characterContentLoss;
    [SerializeField] private List<Character> fullCharactersList;
    
    public static GameManager Instance
    {
        get => instance;
        set => instance = value;
    }

    [SerializeField] private int lockingInteractionCount = 0;

    public void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        fullCharactersList = Object.FindObjectsOfType<Character>().ToList();
    }

    public int LockedParallaxScrolling
    {
        get => lockingInteractionCount;
        set => lockingInteractionCount = value;
    }

    public void IncreaseLockingInteractionCount()
    {
        lockingInteractionCount++;
    }

    public void DecreaseLockingInteractionCount()
    {
        lockingInteractionCount--;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseGameMenu.activeSelf)
            {
                IncreaseLockingInteractionCount();
                PauseGameMenu.SetActive(true);
            }else
            {
                DecreaseLockingInteractionCount();
                PauseGameMenu.SetActive(false);
            }
        }
    }

    public void CheckGameEndCondition()
    {
        if (RadioStationSite.activeSelf)
        {
            EndGameNotificationWin.SetActive(true);
            IncreaseLockingInteractionCount();
            List<Character> charactersList = Object.FindObjectsOfType<Character>().Where(character => character.alive).ToList();
            ListCharacters(charactersList, characterContentWin);
        }
    }
    
    public void DisplayFailureScreen()
    {
        EndGameNotificationLoss.SetActive(true);
        IncreaseLockingInteractionCount();
        ListCharacters(fullCharactersList, characterContentLoss);
    }
    
    private void ListCharacters(List<Character> charactersList, Transform characterContent)
    {
        
        foreach (var character in charactersList)
        {
            GameObject obj = Instantiate(characterPrefab, characterContent);
            var characterName = obj.transform.Find("CharacterName").GetComponent<TextMeshProUGUI>();
            var abilityDisplayed = obj.transform.Find("AbilityDisplayed").GetComponent<TextMeshProUGUI>();
            var characterIcon = obj.transform.Find("CharacterIcon").GetComponent<Image>();
            var abilitySlider = obj.transform.Find("AbilityValues/AbilitySlider").GetComponent<Slider>();
            var abilityValue = obj.transform.Find("AbilityValues/AbilityValueText").GetComponent<TextMeshProUGUI>();

            characterName.text = character.characterName;
            abilityDisplayed.gameObject.SetActive(false);
            characterIcon.sprite = character.characterImage.sprite;
            abilitySlider.gameObject.SetActive(false);
            abilityValue.gameObject.SetActive(false);
        }
    }
}
