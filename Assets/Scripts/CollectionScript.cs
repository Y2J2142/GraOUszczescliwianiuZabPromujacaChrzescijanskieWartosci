using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionScript : MonoBehaviour
{

    [SerializeField]
    private Transform frogDetailsPanel;
    [SerializeField]
    private List<FrogData> frogTypes;
    private GameObject content;
    private GameObject frogListPrefab;
    [SerializeField]
    private Sprite normalGemSprite;
    [SerializeField]
    private Sprite rareGemSprite;
    [SerializeField]
    private Sprite epicGemSprite;
    [SerializeField]
    private Sprite legendaryGemSprite;
    private PlayerResourcesScript playerResources;
    private List<GameObject> frogLists;

    void Start()
    {
        content = GameObject.Find("ShopContent");
        frogListPrefab = Resources.Load<GameObject>("Prefabs/FrogList");
        HideDetailsPanel();

        frogLists = new List<GameObject>();
        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Normal), "Normal frogs");
        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Rare), "Rare frogs");
        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Epic), "Epic frogs");
        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Legendary), "Legendary frogs");

        playerResources = GameObject.Find("PlayerResources").GetComponent<PlayerResourcesScript>();
    }

    private void RefreshListsSprites()
    {
        frogLists.ForEach(list => list.GetComponent<FrogListScript>().RefreshButtonSprites());
    }

    private void InstantiateFrogList(List<FrogData> frogs, string title)
    {
        var frogList = Instantiate(frogListPrefab, Vector3.zero, Quaternion.identity);
        frogList.transform.SetParent(content.GetComponent<GridLayoutGroup>().transform);
        frogList.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        var frogListScript = frogList.GetComponent<FrogListScript>();
        frogListScript.SetProperties(frogs, title);
        frogListScript.registerOnFrogInCollectionClicked(OnFrogInCollectionClicked);
        frogLists.Add(frogList);
    }

    public void HideDetailsPanel()
    {
        frogDetailsPanel.gameObject.SetActive(false);
    }

    private void ShowDetailsPanel()
    {
        frogDetailsPanel.gameObject.SetActive(true);
    }

    private void OnFrogInCollectionClicked(FrogData frog)
    {
        var clickedFrog = frogTypes[frogTypes.IndexOf(frog)];
        frogDetailsPanel.Find("DetailsFrogNameText").GetComponent<Text>().text = clickedFrog.frogName;
        frogDetailsPanel.Find("DetailsFrogRarity").transform.Find("RarityValue").GetComponent<Text>().text = clickedFrog.type.ToString();
        frogDetailsPanel.Find("DetailsFrogBaseHp").transform.Find("HpValue").GetComponent<Text>().text = clickedFrog.maximumSadnessLevel.ToString();
        frogDetailsPanel.Find("DetailsFrogMoneyLoot").transform.Find("MoneyValue").GetComponent<Text>().text = clickedFrog.minCoinsLoot + "-" + clickedFrog.maxCoinsLoot;
        frogDetailsPanel.Find("DetailsFrogNormalLoot").transform.Find("NormalLootValue").GetComponent<Text>().text = clickedFrog.normalLootChance + "%";
        frogDetailsPanel.Find("DetailsFrogRareLoot").transform.Find("RareLootValue").GetComponent<Text>().text = clickedFrog.rareLootChance + "%";
        frogDetailsPanel.Find("DetailsFrogEpicLoot").transform.Find("EpicLootValue").GetComponent<Text>().text = clickedFrog.epicLootChance + "%";
        frogDetailsPanel.Find("DetailsFrogLegendaryLoot").transform.Find("LegendaryLootValue").GetComponent<Text>().text = clickedFrog.legendaryLootChance + "%";
        var buyFrogButton = frogDetailsPanel.Find("BuyFrogButton");
        buyFrogButton.transform.Find("BuyText").GetComponent<Text>().text = "Buy";
        buyFrogButton.transform.Find("PriceText").GetComponent<Text>().text = clickedFrog.price.ToString();
        buyFrogButton.transform.Find("PriceGemImage").GetComponent<Image>().sprite = GetSpriteForGemType(clickedFrog.type);
        buyFrogButton.GetComponent<Button>().onClick.RemoveAllListeners();
        buyFrogButton.GetComponent<Button>().onClick.AddListener(() => UnlockFrog(clickedFrog));
        if (playerResources.getCurrentGemsNumber(CollectionScript.frogTypeToGemType(clickedFrog.type)) < clickedFrog.price)
        {
            buyFrogButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            buyFrogButton.GetComponent<Button>().enabled = true;
        }
        if(clickedFrog.isUnlocked){
            SetUnlockedFrogButton();
        }
        ShowDetailsPanel();
    }

    private void UnlockFrog(FrogData clickedFrog)
    {
        clickedFrog.isUnlocked = true;
        playerResources.SubtractFromCurrentGemsNumber(clickedFrog.price, CollectionScript.frogTypeToGemType(clickedFrog.type));
        SetUnlockedFrogButton();
        RefreshListsSprites();
    }

    private void SetUnlockedFrogButton()
    {
        var buyFrogButton = frogDetailsPanel.Find("BuyFrogButton");
        buyFrogButton.GetComponent<Button>().enabled = false;
        buyFrogButton.transform.Find("PriceText").GetComponent<Text>().text = "UNLOCKED";
        buyFrogButton.transform.Find("BuyText").GetComponent<Text>().text = "";
    }

    private Sprite GetSpriteForGemType(FrogData.FrogType type)
    {
        switch (type)
        {
            case FrogData.FrogType.Normal:
                return normalGemSprite;
            case FrogData.FrogType.Rare:
                return rareGemSprite;
            case FrogData.FrogType.Epic:
                return epicGemSprite;
            case FrogData.FrogType.Legendary:
                return legendaryGemSprite;
            default:
                return normalGemSprite;
        }
    }

    public List<FrogData> GetUnlockedFrogs()
    {
        return frogTypes.FindAll(frog => frog.isUnlocked);
    }

    void Update()
    {

    }

    public static GemScript.GemType frogTypeToGemType(FrogData.FrogType frogType)
    {
        switch (frogType)
        {
            case FrogData.FrogType.Normal:
                return GemScript.GemType.Normal;
            case FrogData.FrogType.Rare:
                return GemScript.GemType.Rare;
            case FrogData.FrogType.Epic:
                return GemScript.GemType.Epic;
            case FrogData.FrogType.Legendary:
                return GemScript.GemType.Legendary;
            default:
                return GemScript.GemType.Normal;
        }
    }
}
