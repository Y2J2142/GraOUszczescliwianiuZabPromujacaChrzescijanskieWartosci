using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    public enum GemType { Normal, Rare, Epic, Legendary }
    private float xSpeed;
    private float ySpeed;
    private GameObject destination;
    private SpriteRenderer spriteRenderer;
    private GemType type = GemType.Normal;
    [SerializeField]
    private Sprite normalGemSprite;
    [SerializeField]
    private Sprite rareGemSprite;
    [SerializeField]
    private Sprite epicGemSprite;
    [SerializeField]
    private Sprite legendaryGemSprite;

    void Start()
    {
        this.destination = GameObject.Find("GoToCollectionButton");
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetSprite();
    }

    private void SetSprite()
    {
        switch (this.type)
        {
            case GemType.Normal:
                spriteRenderer.sprite = normalGemSprite;
                break;
            case GemType.Rare:
                spriteRenderer.sprite = rareGemSprite;
                break;
            case GemType.Epic:
                spriteRenderer.sprite = epicGemSprite;
                break;
            case GemType.Legendary:
                spriteRenderer.sprite = legendaryGemSprite;
                break;
        }
    }

    void Update()
    {
        FlyToDestination();
    }

    public void SetType(GemType type)
    {
        this.type = type;
    }

    public void FlyToDestination()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination.transform.position, 15);
    }

    public bool hitsCounter()
    {
        return gameObject.transform.position.x >= destination.transform.position.x + 5;
    }
}
