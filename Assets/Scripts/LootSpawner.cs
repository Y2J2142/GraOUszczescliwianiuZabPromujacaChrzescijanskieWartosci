using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    private GameObject coinPrefab { get; set; }

    public List<GameObject> coins { get; set; }

    void Start()
    {
        this.coinPrefab = Resources.Load<GameObject>("Prefabs/GoldCoin");
        this.coins = new List<GameObject>();
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

    public bool RemoveCoin(GameObject coin)
    {
        GameObject.Destroy(coin.gameObject, 0);
        return this.coins.Remove(coin.gameObject);
    }
}
