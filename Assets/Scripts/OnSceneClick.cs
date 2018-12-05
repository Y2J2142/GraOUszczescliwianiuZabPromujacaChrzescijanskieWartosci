using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;


public class OnSceneClick : MonoBehaviour
{
    private GameObject frogPrefab;
    private Frog frogScript;
    private List<GameObject> happyFrogs;
    private GameObject currentFrog;
    private RzabaSpawner rzabkaGiver;
    private FlowerManager flowerManager;
    private LootSpawner lootSpawner;
    private List<FlowerModifier> modifiers;

    private TrzepaczHajsu trzepacz;
    private Transform wybuch;
    private Transform dymek;
    private GameObject goToCollectionButton;
    private PlayerResourcesScript playerResourcesScript;

    void Start()
    {
        Monetization.Initialize("2943700", true);
        this.rzabkaGiver = GameObject.Find("RzabaSpawner").GetComponent<RzabaSpawner>();
        ZaboPodmieniarka(rzabkaGiver.ProszemDacRzabke(new Vector3(0, 0, 0)));
        this.flowerManager = new FlowerManager();
        this.lootSpawner = GameObject.Find("LootSpawner").GetComponent<LootSpawner>();
        this.happyFrogs = new List<GameObject>();
        this.currentFrog = GameObject.Find("Frog");
        this.modifiers = new List<FlowerModifier>();
        Screen.orientation = ScreenOrientation.Portrait;
        this.wybuch = GameObject.Find("Wybuch").transform.Find("Explosion");
        this.wybuch.gameObject.SetActive(false);
        this.dymek = GameObject.Find("Dymek").transform.Find("Smoke");
        this.dymek.gameObject.SetActive(false);
        this.goToCollectionButton = GameObject.Find("GoToCollectionButton");
        this.trzepacz = GameObject.Find("TrzepaczHajsu").GetComponent<TrzepaczHajsu>();

        this.trzepacz.rewarder = delegate (bool p)
        {
            modifiers.Add(new FlowerModifier(2, 60, p));
        };
        this.playerResourcesScript = GameObject.Find("PlayerResources").GetComponent<PlayerResourcesScript>();
        LoadData();


        GameObject.Find("Shop").GetComponent<ShopScript>().itemDatas.FindAll(x => x.isBought).ForEach(x => {
            modifiers.Add(new FlowerModifier(2, 60, true));
        });
    }

    void OnGUI()
    {
        if (!MenuController.isGameActive())
        {
            return;
        }
        if (Event.current.type == EventType.MouseDown)
        {
            this.flowerManager.SpawnFlower();
        }
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.red);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        float width = (float)frogScript.CurrentSadnessLevel * 100.0f / (float)frogScript.fullSadness;
        width /= 100.0f;
        GUI.Box(new Rect(0, frogScript.transform.position.y, Screen.width * width, 100), GUIContent.none);

    }

    void Update()
    {
        if (!MenuController.isGameActive())
        {
            return;
        }

        modifiers.ForEach(m =>
        {
            if (m.timer < 0)
                modifiers.Remove(m);
        });
        this.flowerManager.flowers.ForEach(flower =>
        {
            flower.FlyToFrog(this.frogScript.gameObject);
            if (flower.hitsFrog(this.frogScript.gameObject))
            {
                this.dymek.gameObject.SetActive(true);
                this.dymek.GetComponent<Animator>().Play(0);
                this.flowerManager.RemoveFlower(flower);
                float happinesToDeal = flower.Happines;
                modifiers.ForEach(m => { happinesToDeal *= m.modifier; });
                Debug.Log(happinesToDeal);
                this.frogScript.TakeFlowers(happinesToDeal);
            }
        });

        this.lootSpawner.coins.ForEach(coin =>
        {
            if (coin.GetComponent<CoinScript>().hitsCounter())
            {
                this.playerResourcesScript.IncrementCurrentCoinsNumber();
                this.lootSpawner.RemoveCoin(coin);
            }
        });

        this.lootSpawner.gems.ForEach(gem =>
        {
            GemScript gemScript = gem.GetComponent<GemScript>();
            if (gemScript.hitsDestination())
            {
                this.playerResourcesScript.IncrementCurrentGemsNumber(gemScript.GetGemType());
                this.goToCollectionButton.GetComponent<HighlightableObjectScript>().setHighlightSprite(true);
                this.lootSpawner.RemoveGem(gem);
            }
        });

        if (!frogScript.IsSad())
        {
            lootSpawner.SpawnCoins(frogScript.gameObject);
            lootSpawner.SpawnGems(frogScript.gameObject);
            this.wybuch.gameObject.SetActive(true);
            this.wybuch.GetComponent<Animator>().Play(0);
            ZaboPodmieniarka(rzabkaGiver.ProszemDacRzabke(this.frogScript.transform.position));
        }

    }

    void FixedUpdate()
    {
        modifiers.ForEach(m =>
        {
            if (!m.permanent)
                m.timer -= Time.deltaTime;
        });
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


    void LoadData()
    {
        GameObject.Find("Shop").GetComponent<ShopScript>().itemDatas.ForEach(x => {
            if(PlayerPrefs.HasKey(x.itemName))
                x.isBought = PlayerPrefs.GetInt(x.itemName) == 1 ? true : false;
            else
                x.isBought = false;
        });

        GameObject.Find("Collection").GetComponent<CollectionScript>().frogTypes.ForEach(x => {
            if(PlayerPrefs.HasKey(x.frogName))
                x.isUnlocked = PlayerPrefs.GetInt(x.frogName) == 1 ? true : false;
            else
                x.isUnlocked = false;
            if(x.frogName == "Najzwyklejszy Żabol Na Świecie")
                x.isUnlocked = true;
        });


        if(PlayerPrefs.HasKey("hajs"))
            this.playerResourcesScript.CurrentCoinsNumber = PlayerPrefs.GetInt("hajs");
        else
            this.playerResourcesScript.CurrentCoinsNumber = 0;
            
            this.playerResourcesScript.DecrementCurrentCoinNumber(0);


        if(PlayerPrefs.HasKey("multi"))
            this.rzabkaGiver.CurrentSadnessMultiplier = PlayerPrefs.GetFloat("multi");
        else
            this.rzabkaGiver.CurrentSadnessMultiplier = 1.0f;

    }


    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
            return;
        PlayerPrefs.DeleteAll();
        GameObject.Find("Shop").GetComponent<ShopScript>().itemDatas.ForEach(x => {
            PlayerPrefs.SetInt(x.itemName, x.isBought ? 1 : 0);
        });

        GameObject.Find("Collection").GetComponent<CollectionScript>().frogTypes.ForEach(x => {
            PlayerPrefs.SetInt(x.frogName, x.isUnlocked ? 1 : 0);
        });

        GameObject.Find("Collection").GetComponent<CollectionScript>().RefreshListsSprites();

        PlayerPrefs.SetInt("hajs", this.playerResourcesScript.CurrentCoinsNumber);
        PlayerPrefs.SetFloat("multi", this.rzabkaGiver.CurrentSadnessMultiplier);

        PlayerPrefs.Save();

    }

}
