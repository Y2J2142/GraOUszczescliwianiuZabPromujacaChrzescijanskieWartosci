using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RzabaSpawner : MonoBehaviour
{
    public GameObject frogPrefab;
    double currentSadnessMultiplier = 1.0;

    void Start()
    {
        this.frogPrefab = Resources.Load<GameObject>("Prefabs/Frog");
    }

    void Update()
    {
    }
    public GameObject ProszemDacRzabke(Vector3 pos)
    {
        var rzabulec = Instantiate(frogPrefab, pos, Quaternion.identity);
        rzabulec.transform.SetParent(GameObject.Find("Game").transform);
        rzabulec.GetComponent<Frog>().frogData = GetRandomFrogType();
        rzabulec.GetComponent<Frog>().CurrentSadnessmultiplier = currentSadnessMultiplier;
        currentSadnessMultiplier += 0.01;
        var parentPosition = rzabulec.transform.parent.transform.position;
        rzabulec.transform.position = new Vector3(parentPosition.x, parentPosition.y - 50, parentPosition.z);
        return rzabulec;
    }

    private FrogData GetRandomFrogType()
    {
        var frogTypes = GameObject.Find("Collection").GetComponent<CollectionScript>().GetUnlockedFrogs();
        int sum = 0;
        frogTypes.ForEach(kamulec => sum += kamulec.rarity);
        int rnd = UnityEngine.Random.Range(0, sum);
        int counter = 0;
        foreach (var kamyczek in frogTypes)
        {
            counter += kamyczek.rarity;
            if (rnd <= counter)
            {
                return kamyczek;
            }
        }
        return frogTypes[frogTypes.Count - 1];
    }
}
