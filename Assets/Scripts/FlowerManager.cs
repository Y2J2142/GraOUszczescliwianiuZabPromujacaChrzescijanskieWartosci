using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager {
	public GameObject flowerPrefab { get; set; }
	public List<Flower> flowers { get; set; }

	public FlowerManager()
	{
		this.flowerPrefab = Resources.Load<GameObject>("Prefabs/Flower");
		this.flowers = new List<Flower>();
	}

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void SpawnFlower()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Flower spawnedFlower = new Flower(GameObject.Instantiate(this.flowerPrefab, mousePosition, Quaternion.identity));
        spawnedFlower.gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
        this.flowers.Add(spawnedFlower);
	}

	public bool RemoveFlower(Flower flower)
	{
		GameObject.Destroy(flower.gameObject, 0);
		return this.flowers.Remove(flower);
	}
}
