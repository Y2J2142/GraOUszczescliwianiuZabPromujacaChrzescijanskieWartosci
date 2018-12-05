using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField]
    public List<ItemData> itemDatas;
    private GameObject content;
    private GameObject itemListPrefab;
    private TrzepaczHajsu trzepacz;
    private PlayerResourcesScript prs;




    void Start()
    {
        content = GameObject.Find("PrawdziwyShopContent");
        itemListPrefab = Resources.Load<GameObject>("Prefabs/ItemList");
        this.prs = GameObject.Find("PlayerResources").GetComponent<PlayerResourcesScript>();


        this.trzepacz = GameObject.Find("TrzepaczHajsu").GetComponent<TrzepaczHajsu>();        

        InstantiateList(itemDatas.FindAll(item => item.rarity == ItemData.rarities.GOWNO), "Gowno");
        InstantiateList(itemDatas.FindAll(item => item.rarity == ItemData.rarities.ZWYKLE), "Zwykle");
        InstantiateList(itemDatas.FindAll(item => item.rarity == ItemData.rarities.DOBRE), "Dobre");
        InstantiateList(itemDatas.FindAll(item => item.rarity == ItemData.rarities.ZAJEBISTE), "Zajebiste");
	}

    private void InstantiateList(List<ItemData> items, string title)
    {
        var itemList = Instantiate(itemListPrefab, Vector3.zero, Quaternion.identity);
        itemList.transform.SetParent(GameObject.Find("PrawdziwyShopContent").GetComponent<GridLayoutGroup>().transform);
        itemList.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        var itemListScript = itemList.GetComponent<ItemListScript>();
        itemListScript.SetProperties(items, title);
        itemListScript.addAction(BuyItem);
    }


    private void BuyItem(ItemData item)
    {
        if(prs.CurrentCoinsNumber >= item.price)
        {
            if(!item.isBought)
            {
                prs.DecrementCurrentCoinNumber(item.price);
                trzepacz.rewarder(true);
                item.isBought = true;
                
            }

        }
    }

    void Update()
    {

    }
}
