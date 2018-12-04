using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsPanelScript : MonoBehaviour
{
    private Text normalGemsCountText;
    private Text rareGemsCountText;
    private Text epicGemsCountText;
    private Text legendaryGemsCountText;

    void Start()
    {
        normalGemsCountText = gameObject.transform.Find("NormalGemCount").GetComponent<Text>();
        rareGemsCountText = gameObject.transform.Find("RareGemCount").GetComponent<Text>();
        epicGemsCountText = gameObject.transform.Find("EpicGemCount").GetComponent<Text>();
        legendaryGemsCountText = gameObject.transform.Find("LegendaryGemCount").GetComponent<Text>();
        PlayerResourcesScript.OnCurrentGemsNumberChange += OnCurrentGemsNumberChange;
    }

    void Update()
    {

    }

    void Destroy()
    {
        PlayerResourcesScript.OnCurrentGemsNumberChange -= OnCurrentGemsNumberChange;
    }

    void OnCurrentGemsNumberChange(int number, GemScript.GemType type)
    {
        switch (type)
        {
            case GemScript.GemType.Normal:
                normalGemsCountText.text = number.ToString();
                break;
            case GemScript.GemType.Rare:
                rareGemsCountText.text = number.ToString();
                break;
            case GemScript.GemType.Epic:
                epicGemsCountText.text = number.ToString();
                break;
            case GemScript.GemType.Legendary:
                legendaryGemsCountText.text = number.ToString();
                break;
        }
    }
}
