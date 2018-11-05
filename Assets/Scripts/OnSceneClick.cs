using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnSceneClick : MonoBehaviour {

	private GameObject prefab;
	private List<GameObject> spawnedFlowers;
	private Frog frog;
	private Vector3 frogPosition;
    public Sprite happySprite;

	void Start () {
		this.prefab = Resources.Load<GameObject>("Flower");
		this.happySprite = Resources.Load<Sprite>("happy_frog");
		this.spawnedFlowers = new List<GameObject>();
	}

    void OnGUI()
    {
		if(Input.GetButtonDown("Fire1"))
		{
			initCurrentFrog();
			spawnFlower();	
		}
    }

	void Update () {
		this.spawnedFlowers.ForEach(flower => {
			flower.transform.Rotate(new Vector3(0, 0, 2));
			flower.transform.position = Vector3.MoveTowards(flower.transform.position, this.frogPosition, 15);

			if(flower.transform.position == new Vector3(0, 0, -10)) 
			{
				this.spawnedFlowers.Remove(flower);
				Destroy(flower);
				this.frog.TakeFlowers(10);
			}
		});
	}

	void initCurrentFrog()
	{
		this.frog = GameObject.Find("Frog").GetComponent<Frog>();
		this.frogPosition = Camera.main.ScreenToWorldPoint(this.frog.gameObject.transform.position);
	}

	void spawnFlower()
	{
		Vector3 mousePositionIn3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.spawnedFlowers.Add(Instantiate(this.prefab, new Vector3(mousePositionIn3D.x, mousePositionIn3D.y, 0), Quaternion.identity));
	}
}
