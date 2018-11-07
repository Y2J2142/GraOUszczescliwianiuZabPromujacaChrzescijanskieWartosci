using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RzabaSpawner : MonoBehaviour {
	public GameObject frogPrefab;
	
	void Start () {
    	this.frogPrefab = Resources.Load<GameObject>("Prefabs/Frog");
	}
	
	void Update () {
	}
	public GameObject ProszemDacRzabke(Vector3 pos) {
		var rzabulec = Instantiate(frogPrefab, pos, Quaternion.identity);
		rzabulec.transform.SetParent(GameObject.Find("Canvas").transform);
		return rzabulec;
	}
}
