using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnSceneClick : MonoBehaviour
{

    private GameObject flowerPrefab;
    private GameObject frogPrefab;
    private Frog frogScript;
    private List<GameObject> happyFrogs;
    private GameObject currentFrog;
	private RzabaSpawner rzabkaGiver;

    private FlowerManager flowerManager;


    void Start()
    {
        this.frogScript = GameObject.Find("Frog").GetComponent<Frog>();
        this.flowerPrefab = Resources.Load<GameObject>("Prefabs/Flower");
        this.flowerManager = new FlowerManager();
        this.happyFrogs = new List<GameObject>();
        this.currentFrog = GameObject.Find("Frog");
		this.rzabkaGiver = GameObject.Find("RzabaSpawner").GetComponent<RzabaSpawner>();
        Screen.orientation = ScreenOrientation.Portrait;
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
        this.flowerManager.flowers.ForEach(flower =>
        {
            flower.FlyToFrog(this.frogScript.gameObject);
            if (flower.gameObject.transform.position == frogScript.transform.position)
            {
                this.flowerManager.flowers.Remove(flower);
                Destroy(flower.gameObject, 0);
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
        Flower spawnedFlower = new Flower(Instantiate(this.flowerPrefab, mousePosition, Quaternion.identity));
        spawnedFlower.gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
        this.flowerManager.flowers.Add(spawnedFlower);
    }
}
