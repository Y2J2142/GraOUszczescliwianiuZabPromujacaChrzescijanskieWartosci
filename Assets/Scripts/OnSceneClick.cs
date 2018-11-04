using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnSceneClick : MonoBehaviour {

	private GameObject prefab;
	private GameObject spawnedFlower;
	private GameObject currentFrog;
	private Vector3 frogPosition;
    public Sprite sadSprite;

	void Start () {
		this.prefab = GameObject.Find("Flower");
		this.sadSprite = Resources.Load<Sprite>("Sprites/sad_frog.png");
	}

    void OnGUI()
    {
		if(Input.GetButtonDown("Fire1"))
		{
			initCurrentFrog();
			if(this.spawnedFlower == null)
			{
				spawnFlower();
			}
			
		}
    }

	void Update () {
		if(this.spawnedFlower != null)
		{
			this.spawnedFlower.transform.Rotate(new Vector3(0, 0, 2));
			this.spawnedFlower.transform.position = Vector3.MoveTowards(this.spawnedFlower.transform.position, this.frogPosition, 20);

			if(this.spawnedFlower.transform.position == new Vector3(0, 0, -10)) 
			{
				Destroy(this.spawnedFlower);
				this.currentFrog.GetComponent<Image>().sprite = sadSprite;
			}

			Debug.Log(this.spawnedFlower.transform.position);
			Debug.Log(this.currentFrog.transform.position);
		}
	}

	void initCurrentFrog()
	{
		this.currentFrog = GameObject.Find("Frog");
		this.frogPosition = Camera.main.ScreenToWorldPoint(this.currentFrog.transform.position);
	}

	void spawnFlower()
	{
		Vector3 mousePositionIn3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.spawnedFlower = Instantiate(this.prefab, new Vector3(mousePositionIn3D.x, mousePositionIn3D.y, 0), Quaternion.identity);
	}
}
