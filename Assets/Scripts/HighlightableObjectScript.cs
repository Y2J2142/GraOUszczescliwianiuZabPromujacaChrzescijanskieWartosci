using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightableObjectScript : MonoBehaviour
{
    [SerializeField]
    Sprite normalSprite;
    [SerializeField]
    Sprite highlightSprite;
    Image image;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.sprite = normalSprite;
    }

    void Update()
    {

    }

    public void setHighlightSprite(bool shouldHighlightSprite)
    {
        if (shouldHighlightSprite)
        {
            image.sprite = highlightSprite;
        }
        else
        {
            image.sprite = normalSprite;
        }
    }
}
