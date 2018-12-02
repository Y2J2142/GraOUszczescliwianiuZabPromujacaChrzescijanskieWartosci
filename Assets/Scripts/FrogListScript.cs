using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogListScript : MonoBehaviour
{
    [SerializeField]
    private Sprite lockedFrogSprite;
    private Text titleText;
    private Transform frogsPanel;
    private GameObject frogButtonPrefab;

    private string title;
    private List<FrogData> frogDatas;

    void Start()
    {
        titleText = gameObject.transform.Find("Title").GetComponent<Text>();
        frogsPanel = gameObject.transform.Find("FrogsPanel");
        frogButtonPrefab = Resources.Load<GameObject>("Prefabs/FrogButton");

        titleText.text = title;

        frogDatas.ForEach(frog =>
        {
            var newButton = Instantiate(frogButtonPrefab, Vector3.zero, Quaternion.identity);
            newButton.transform.SetParent(frogsPanel.GetComponent<GridLayoutGroup>().transform);
            var buttonImage = newButton.transform.GetComponent<Image>();
            newButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            buttonImage.sprite = lockedFrogSprite;
            if (frog.isUnlocked)
            {
                buttonImage.sprite = frog.happySprite;
            }
        });
    }

    public void SetProperties(List<FrogData> frogs, string title)
    {
        this.frogDatas = frogs;
        this.title = title;
    }

    void Update()
    {

    }
}
