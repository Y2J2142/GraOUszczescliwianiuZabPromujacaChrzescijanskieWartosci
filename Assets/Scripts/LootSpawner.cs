using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    private GameObject coinPrefab { get; set; }
    private GameObject gemPrefab { get; set; }

    public List<GameObject> coins { get; set; }
    public List<GameObject> gems { get; set; }

    void Start()
    {
        this.coinPrefab = Resources.Load<GameObject>("Prefabs/GoldCoin");
        this.gemPrefab = Resources.Load<GameObject>("Prefabs/Gem");
        this.coins = new List<GameObject>();
        this.gems = new List<GameObject>();
    }

    void Update()
    {
    }

    public void SpawnCoins(GameObject frog)
    {
        var frogData = frog.GetComponent<Frog>().frogData;
        var amount = UnityEngine.Random.Range(frogData.minCoinsLoot, frogData.maxCoinsLoot + 1);
        for (int i = 0; i < amount; i++)
        {
            var spawnedCoin = Instantiate(coinPrefab, frog.transform.position + Random.insideUnitSphere * 200, Quaternion.identity);
            spawnedCoin.transform.SetParent(GameObject.Find("Game").transform);
            this.coins.Add(spawnedCoin);
        }
    }

    public void SpawnGems(GameObject frog)
    {
        var frogData = frog.GetComponent<Frog>().frogData;
        SpawnGem(frogData.normalLootChance, GemScript.GemType.Normal, frog.transform.position);
        SpawnGem(frogData.rareLootChance, GemScript.GemType.Rare, frog.transform.position);
        SpawnGem(frogData.epicLootChance, GemScript.GemType.Epic, frog.transform.position);
        SpawnGem(frogData.legendaryLootChance, GemScript.GemType.Legendary, frog.transform.position);
    }

    private void SpawnGem(int lootChance, GemScript.GemType gemType, Vector3 frogPosition)
    {
        var rand = UnityEngine.Random.Range(0, 101);
        if (rand < lootChance)
        {
            var spawnedGem = Instantiate(gemPrefab, frogPosition + Random.insideUnitSphere * 200, Quaternion.identity);
            spawnedGem.transform.SetParent(GameObject.Find("Game").transform);
            spawnedGem.GetComponent<GemScript>().SetType(gemType);
            this.gems.Add(spawnedGem);
        }
    }

    public bool RemoveCoin(GameObject coin)
    {
        GameObject.Destroy(coin.gameObject, 0);
        return this.coins.Remove(coin.gameObject);
    }

    public bool RemoveGem(GameObject gem)
    {
        GameObject.Destroy(gem.gameObject, 0);
        return this.gems.Remove(gem.gameObject);
    }
}
