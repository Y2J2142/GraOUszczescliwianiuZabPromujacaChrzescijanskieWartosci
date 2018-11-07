using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RzabaSpawner : MonoBehaviour {
	public GameObject frogPrefab;
	public Frog original;
	
	void Start () {
    	this.frogPrefab = Resources.Load<GameObject>("Prefabs/Frog");
	}
	
	void Update () {
	}
	public GameObject ProszemDacRzabke(Vector3 pos) {
		var rzabulec = Instantiate(Resources.Load<GameObject>("Prefabs/Frog"), pos, Quaternion.identity);
		rzabulec.transform.SetParent(GameObject.Find("Canvas").transform);
		return rzabulec;
	}
}
