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

        var normalFrogList = Instantiate(frogListPrefab, Vector3.zero, Quaternion.identity);
        normalFrogList.transform.SetParent(content.GetComponent<GridLayoutGroup>().transform);
        normalFrogList.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        normalFrogList.GetComponent<FrogListScript>().SetProperties(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Normal), "Normal frogs");

        var rareFrogList = Instantiate(frogListPrefab, Vector3.zero, Quaternion.identity);
        rareFrogList.transform.SetParent(content.GetComponent<GridLayoutGroup>().transform);
        rareFrogList.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        rareFrogList.GetComponent<FrogListScript>().SetProperties(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Rare), "Rare frogs");

        var epicFrogList = Instantiate(frogListPrefab, Vector3.zero, Quaternion.identity);
        epicFrogList.transform.SetParent(content.GetComponent<GridLayoutGroup>().transform);
        epicFrogList.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        epicFrogList.GetComponent<FrogListScript>().SetProperties(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Epic), "Epic frogs");

        var legendaryFrogList = Instantiate(frogListPrefab, Vector3.zero, Quaternion.identity);
        legendaryFrogList.transform.SetParent(content.GetComponent<GridLayoutGroup>().transform);
        legendaryFrogList.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        legendaryFrogList.GetComponent<FrogListScript>().SetProperties(frogTypes.FindAll(frog => frog.type == FrogData.FrogType.Legendary), "Legendary frogs");
    }

    void Update()
    {

    }
}
