using System.Collections.Generic;
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

    // public GameObject 
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


    public void ProgressExploration()
    {
        currentProgress += explorationTarget.defaultExplorationProgress / 3f;
        explorationProgressSlider.value += explorationTarget.defaultExplorationProgress / 3f;
        // * HOWmanyPEOPLEareTHEREoutOFthree;
        if (currentProgress >= 100)
        {
            if (exploredSitesCount == explorationTargetList.Count - 1)
            {
                RandomizeNewSite();
            }
            AddItemsToInventory();
            exploredSitesCount++;
            UpdateExplorationScreen();
        }
    }

    private void AddItemsToInventory()
    {
        foreach (var item in explorationTarget.Items)
        {
            for (int i = 0; i < (item.MinAmount + item.MaxAmount) / 2; i++)
            {
                InventoryManager.Instance.Add(item.Item);
            }
        }
    }
    
    private void RandomizeNewSite()
    {
        var chosenSiteType = Random.Range(0, possibleSitesType.Count-1);
        
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
            itemCount.text = item.MinAmount.ToString() + "-" + item.MaxAmount.ToString();
            itemIcon.sprite = item.Item.itemIcon;
        }
    }
}