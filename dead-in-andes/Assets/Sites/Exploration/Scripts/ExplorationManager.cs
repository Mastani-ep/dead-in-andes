using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExplorationManager : MonoBehaviour
{
    public static ExplorationManager Instance;
    [SerializeField] private List<ExplorationTarget> explorationTargetList;
    [SerializeField] private List<ExplorationTarget> possibleSitesType;
    [SerializeField] private ExplorationTarget explorationTarget;
    [SerializeField] private int exploredSitesCount = 0;
    [SerializeField] private GameObject explorationItem;
    [SerializeField] private Transform itemContent;
    [SerializeField] private TextMeshProUGUI currentlyExploredText;
    [SerializeField] private float currentProgress;
    [SerializeField] private Slider explorationProgressSlider;
    [SerializeField] private ExplorationFinishedUI explorationFinishedUI;
    private List<ExplorationTarget> exploredSites = new List<ExplorationTarget>();
    private float additionalProgress = 0;
    
    private void Awake()
    {
        Instance = this;
        UpdateExplorationScreen();
    }


    public void UpdateExplorationScreen()
    {
        explorationTarget = explorationTargetList[exploredSitesCount];

        currentlyExploredText.text = explorationTarget.explorationTargetName;
        currentProgress = 0;
        explorationProgressSlider.value = 0;
        ListItems();
    }


    public void ProgressExploration(Character currentCharacter)
    {
        float progress = (explorationTarget.defaultExplorationProgress) *
                         (1f + 1f * (currentCharacter[SkillType.Exploration] / 100f));
        currentProgress += progress;
        if (currentProgress >= 100)
        {
            additionalProgress = currentProgress - 100;
            if (exploredSitesCount == explorationTargetList.Count - 1)
            {
                RandomizeNewSite();
            }
            exploredSites.Add(explorationTarget);
            exploredSitesCount++;
            UpdateExplorationScreen();
            
            currentProgress = additionalProgress;
            explorationProgressSlider.value = currentProgress;
            additionalProgress = 0;
        }

    }

    public void EvaluateExploration()
    {
        if (exploredSites.Any())
        {
            explorationFinishedUI.UpdateExplorationFinishedScreen(exploredSites);
        }
    }

    public void ClearExploredSites()
    {
        exploredSites.Clear();
    }
    
    private void RandomizeNewSite()
    {
        var chosenSiteType = Random.Range(0, possibleSitesType.Count - 1);
        explorationTargetList.Add(possibleSitesType[chosenSiteType]);
    }

    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in explorationTarget.Items)
        {
            GameObject obj = Instantiate(explorationItem, itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemCount = obj.transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.Item.itemName;
            if (item.MinAmount == item.MaxAmount)
            {
                itemCount.text = item.MinAmount.ToString();
            }
            else
            {
                itemCount.text = item.MinAmount + "-" + item.MaxAmount;
            }
            itemIcon.sprite = item.Item.itemIcon;
        }
    }
}