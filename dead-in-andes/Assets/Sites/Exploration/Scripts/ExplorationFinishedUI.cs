using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExplorationFinishedUI : MonoBehaviour
{
    [Header("Exploration Choice Screen")]
    [SerializeField] private Character[] charactersList;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform itemContent;
    [SerializeField] private Transform characterContent;
    [SerializeField] private GameObject explorationItem;
    [SerializeField] private TextMeshProUGUI explorationSiteName;
    [SerializeField] private TextMeshProUGUI usefulSkillName;
    [SerializeField] private GameObject explorationFinishedScreen;
    
    [Header("Exploration Finished Screen")]
    [SerializeField] private GameObject explorationPopupScreen;
    [SerializeField] private Transform itemPopupContent;
    
    
    void OnEnable()
    {
        charactersList = Object.FindObjectsOfType<Character>();
    }

    public void UpdateExplorationFinishedScreen(ExplorationTarget explorationTarget)
    {
        explorationFinishedScreen.SetActive(true);
        ListItems(explorationTarget);
        ListCharacters(explorationTarget);
    }

    private void ListItems(ExplorationTarget explorationTarget)
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        explorationSiteName.text = explorationTarget.explorationTargetName;
        usefulSkillName.text = explorationTarget.usefulSkillType.ToString();
        
        foreach (var item in explorationTarget.Items)
        {
            GameObject obj = Instantiate(explorationItem, itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemCount = obj.transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.Item.itemName;
            itemCount.text = item.MinAmount.ToString() + "-" + item.MaxAmount.ToString();
            itemIcon.sprite = item.Item.itemIcon;
        }
    }

    private void ListCharacters(ExplorationTarget explorationTarget)
    {
        foreach (Transform character in characterContent)
        {
            Destroy(character.gameObject);
        }

        foreach (var character in charactersList)
        {
            GameObject obj = Instantiate(characterPrefab, characterContent);
            var characterName = obj.transform.Find("CharacterName").GetComponent<TMPro.TextMeshProUGUI>();
            var abilityDisplayed = obj.transform.Find("AbilityDisplayed").GetComponent<TMPro.TextMeshProUGUI>();
            var characterIcon = obj.transform.Find("CharacterIcon").GetComponent<Image>();
            var abilitySlider = obj.transform.Find("AbilityValues/AbilitySlider").GetComponent<Slider>();
            var abilityValue = obj.transform.Find("AbilityValues/AbilityValueText").GetComponent<TMPro.TextMeshProUGUI>();
            
            Button characterButton = obj.GetComponent<Button>();

            characterName.text = character.characterName;
            abilityDisplayed.text = explorationTarget.usefulSkillType.ToString();
            characterIcon.sprite = character.characterImage.sprite;
            abilitySlider.value = character[explorationTarget.usefulSkillType];
            abilityValue.text = character[explorationTarget.usefulSkillType].ToString();

            characterButton.onClick.AddListener(() => { ChoseCharacterForExplorationAction(character, explorationTarget); });
        }
    }

    private void ChoseCharacterForExplorationAction(Character character, ExplorationTarget explorationTarget)
    {
        AddItemsToInventoryAndPreparePopup(explorationTarget, character[explorationTarget.usefulSkillType]);
        explorationFinishedScreen.SetActive(false);
    }
    
    
    private void AddItemsToInventoryAndPreparePopup(ExplorationTarget explorationTarget, int abilityValue)
    {
        foreach (Transform item in itemPopupContent)
        {
            Destroy(item.gameObject);
        }
        explorationPopupScreen.SetActive(true);
        
        foreach (var item in explorationTarget.Items)
        {
            int itemsAdded = Random.Range(item.MinAmount + (abilityValue * (item.MaxAmount - item.MinAmount)) / 100, item.MaxAmount);
            InventoryManager.Instance.Add(item.Item, itemsAdded);
            
            GameObject obj = Instantiate(explorationItem, itemPopupContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemCount = obj.transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.Item.itemName;
            itemCount.text = itemsAdded.ToString();
            itemIcon.sprite = item.Item.itemIcon;
        }
    }

    public void CloseExplorationPopup()
    {
        explorationPopupScreen.SetActive(false);
    }
    
}