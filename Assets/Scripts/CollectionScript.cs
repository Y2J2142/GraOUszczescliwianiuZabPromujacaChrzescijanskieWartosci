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

    void Start()
    {
        content = GameObject.Find("ShopContent");
        frogListPrefab = Resources.Load<GameObject>("Prefabs/FrogList");
        HideDetailsPanel();

        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Normal), "Normal frogs");
        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Rare), "Rare frogs");
        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Epic), "Epic frogs");
        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Legendary), "Legendary frogs");
    }

    private void InstantiateFrogList(List<FrogData> frogs, string title)
    {
        var normalFrogList = Instantiate(frogListPrefab, Vector3.zero, Quaternion.identity);
        normalFrogList.transform.SetParent(content.GetComponent<GridLayoutGroup>().transform);
        normalFrogList.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        var frogListScript = normalFrogList.GetComponent<FrogListScript>();
        frogListScript.SetProperties(frogs, title);
        frogListScript.registerOnFrogInCollectionClicked(OnFrogInCollectionClicked);
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
        ShowDetailsPanel();
    }

    public List<FrogData> GetUnlockedFrogs()
    {
        return frogTypes.FindAll(frog => frog.isUnlocked);
    }

    void Update()
    {

    }
}
