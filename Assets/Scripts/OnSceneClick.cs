using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnSceneClick : MonoBehaviour
{

    private GameObject flowerPrefab;
    private List<GameObject> spawnedFlowers;
    private GameObject frogPrefab;
    private Frog frogScript;
    private List<GameObject> happyFrogs;
    private GameObject currentFrog;
	private RzabaSpawner rzabkaGiver;


    void Start()
    {
        this.frogScript = GameObject.Find("Frog").GetComponent<Frog>();
        this.flowerPrefab = Resources.Load<GameObject>("Prefabs/Flower");
        this.spawnedFlowers = new List<GameObject>();
        this.happyFrogs = new List<GameObject>();
        this.currentFrog = GameObject.Find("Frog");
		this.rzabkaGiver = GetComponent<RzabaSpawner>();
    }

    void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            spawnFlower();
        }
    }

    void Update()
    {
        this.spawnedFlowers.ForEach(flower =>
        {
            flower.transform.Rotate(new Vector3(0, 0, 2));
            flower.transform.position = Vector3.MoveTowards(flower.transform.position, this.frogScript.transform.position, 15);

            if (flower.transform.position == frogScript.transform.position)
            {
                this.spawnedFlowers.Remove(flower);
                Destroy(flower, 0);
                this.frogScript.TakeFlowers(10);
            }
        });

        if (!frogScript.IsSad())
        {
            ZaboPodmieniarka(rzabkaGiver.ProszemDacRzabke(this.frogScript.transform.position));
        }
    }

    IEnumerator Remover(GameObject frogo)
    {
        yield return new WaitForSeconds(2);
        happyFrogs.Remove(frogo);
        Destroy(frogo);
    }

	private void ZaboPodmieniarka(GameObject frog)
	{
            this.happyFrogs.Add(this.currentFrog);
            StartCoroutine(Remover(this.currentFrog));
            this.currentFrog = frog;
            this.frogScript = this.currentFrog.GetComponent<Frog>();
	}

    void spawnFlower()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject spawnedFlower = Instantiate(this.flowerPrefab, mousePosition, Quaternion.identity);
        spawnedFlower.transform.SetParent(GameObject.Find("Canvas").transform);
        this.spawnedFlowers.Add(spawnedFlower);
    }
}
