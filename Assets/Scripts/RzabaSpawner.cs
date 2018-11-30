using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RzabaSpawner : MonoBehaviour {
	public GameObject frogPrefab;

	[SerializeField]
	List<FrogData> frogTypes;
	
	void Start () {
    	this.frogPrefab = Resources.Load<GameObject>("Prefabs/Frog");
	}
	
	void Update () {
	}
	public GameObject ProszemDacRzabke(Vector3 pos) {
		var rzabulec = Instantiate(frogPrefab, pos, Quaternion.identity);
		rzabulec.transform.SetParent(GameObject.Find("Canvas").transform);
		rzabulec.GetComponent<Frog>().frogData = GetRandomFrogType();
		return rzabulec;
	}

	private FrogData GetRandomFrogType(){

		int sum = 0;
		frogTypes.ForEach(kamulec => sum += kamulec.rarity);
		int rnd = UnityEngine.Random.Range(0, sum);
		int counter = 0;
		foreach (var kamyczek in frogTypes) {
			counter += kamyczek.rarity;
			if (rnd <= counter) {
				return kamyczek;
			}
		}
		return frogTypes[frogTypes.Count - 1];
	}
}
