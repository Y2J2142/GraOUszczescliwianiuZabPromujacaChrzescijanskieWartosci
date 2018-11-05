using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnSceneClick : MonoBehaviour {

	private GameObject flowerPrefab;
	private List<GameObject> spawnedFlowers;
	private Frog frog;

	void Start () {
		this.frog = GameObject.Find("Frog").GetComponent<Frog>();
		this.flowerPrefab = Resources.Load<GameObject>("Flower");
		this.spawnedFlowers = new List<GameObject>();
	}

    void OnGUI()
    {
		if(Input.GetButtonDown("Fire1"))
		{
			spawnFlower();	
		}
    }

	void Update () {
		this.spawnedFlowers.ForEach(flower => {
			flower.transform.Rotate(new Vector3(0, 0, 2));
			flower.transform.position = Vector3.MoveTowards(flower.transform.position, this.frog.transform.position, 15);

			if(flower.transform.position == frog.transform.position) 
			{
				this.spawnedFlowers.Remove(flower);
				Destroy(flower);
				this.frog.TakeFlowers(10);
			}
		});

		if(!frog.IsSad())
		{
			this.frog.FlyAway();
		}
	}

	void spawnFlower()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		GameObject spawnedFlower = Instantiate(this.flowerPrefab, mousePosition, Quaternion.identity);
		spawnedFlower.transform.SetParent(GameObject.Find("Canvas").transform);
		this.spawnedFlowers.Add(spawnedFlower);
	}
}
