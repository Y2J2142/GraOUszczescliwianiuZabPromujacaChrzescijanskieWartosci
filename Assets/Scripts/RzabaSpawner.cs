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
		List<int> indexList = new List<int>();

		for(int i = 0; i < frogTypes.Count; i++) {
			for(int j = 0; j < frogTypes[i].rarity; j++){
				indexList.Add(i);
			}
		}

		var rnd = UnityEngine.Random.value;
		var index = indexList[(int)(indexList.Count * rnd)];
		Debug.Log(index);
		return frogTypes[index];
	}
}
