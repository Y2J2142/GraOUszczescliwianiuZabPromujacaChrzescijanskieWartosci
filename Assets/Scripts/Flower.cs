using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower {
    public GameObject gameObject {get; set; }

	public Flower(GameObject flower) 
	{
		this.gameObject = flower;
	}

	void Start () {	}

	void Update () { }

	public void FlyToFrog(GameObject frogObject)
	{
		gameObject.transform.Rotate(new Vector3(0, 0, 2));
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, frogObject.transform.position, 15);
	}
}
