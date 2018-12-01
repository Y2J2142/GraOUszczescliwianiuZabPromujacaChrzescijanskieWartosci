using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnSceneClick : MonoBehaviour
{
    private GameObject frogPrefab;
    private Frog frogScript;
    private List<GameObject> happyFrogs;
    private GameObject currentFrog;
    private RzabaSpawner rzabkaGiver;
    private FlowerManager flowerManager;
    private HudCounterController hudCounterController;
    private ParticleSystem frogThunder;
    private ParticleSystem puff;


    void Start()
    {
        this.rzabkaGiver = GameObject.Find("RzabaSpawner").GetComponent<RzabaSpawner>();
        ZaboPodmieniarka(rzabkaGiver.ProszemDacRzabke(new Vector3(0, 0, 0)));
        this.flowerManager = new FlowerManager();
        this.happyFrogs = new List<GameObject>();
        this.currentFrog = GameObject.Find("Frog");
        this.hudCounterController = GameObject.Find("HudCounter").GetComponent<HudCounterController>();
        this.frogThunder = GameObject.Find("FrogThunder").GetComponent<ParticleSystem>();
        this.puff = GameObject.Find("Puff").GetComponent<ParticleSystem>();

        Screen.orientation = ScreenOrientation.Portrait;
    }

    void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            this.flowerManager.SpawnFlower();
        }
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0,0, Color.red);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        float width = (float)frogScript.CurrentSadnessLevel * 100.0f / (float)frogScript.fullSadness;
        width /= 100.0f; 
        GUI.Box(new Rect(0,frogScript.transform.position.y, Screen.width * width , 100), GUIContent.none);
    }

    void Update()
    {
        this.flowerManager.flowers.ForEach(flower =>
        {
            flower.FlyToFrog(this.frogScript.gameObject);
            if (flower.hitsFrog(this.frogScript.gameObject))
            {
                this.puff.Play();
                this.flowerManager.RemoveFlower(flower);
                this.frogScript.TakeFlowers(10);
            }
        });

        if (!frogScript.IsSad())
        {
            this.hudCounterController.Increment();
            this.frogThunder.Play();
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
        if (currentFrog != null)
        {
            this.happyFrogs.Add(this.currentFrog);
            StartCoroutine(Remover(this.currentFrog));
        }
        this.currentFrog = frog;
        this.frogScript = this.currentFrog.GetComponent<Frog>();
    }
}
