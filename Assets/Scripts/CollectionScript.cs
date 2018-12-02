using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionScript : MonoBehaviour
{

    public List<FrogData> frogTypes;
    private GameObject content;
    private GameObject frogListPrefab;

    void Start()
    {
        content = GameObject.Find("Content");
        frogListPrefab = Resources.Load<GameObject>("Prefabs/FrogList");

        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Normal), "Normal frogs");
        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Rare), "Rare frogs");
        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Epic), "Epic frogs");
        InstantiateFrogList(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Legendary), "Legendary frogs");
    }

    private void InstantiateFrogList(List<FrogData> frogs, string title)
    {
        var normalFrogList = Instantiate(frogListPrefab, Vector3.zero, Quaternion.identity);
        normalFrogList.transform.SetParent(content.GetComponent<GridLayoutGroup>().transform);
        normalFrogList.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        normalFrogList.GetComponent<FrogListScript>().SetProperties(frogs, title);
    }

    void Update()
    {

    }
}
