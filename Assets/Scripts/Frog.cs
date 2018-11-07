using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frog : MonoBehaviour
{
    [SerializeField]
    public Sprite sadSprite;

    [SerializeField]
    public Sprite happySprite;

    [SerializeField]
    public Sprite headgearSprite;

    [SerializeField]
    public Sprite outfitSprite;

    [SerializeField]
    public int currentSadnessLevel = 10;
    private SpriteRenderer bodySpriteRenderer;
    private SpriteRenderer headGearSpriteRenderer;
    private SpriteRenderer outfitSpriteRenderer;
    private float xSpeed;
    private float ySpeed;

    void Start()
    {
        bodySpriteRenderer = gameObject.transform.Find("Body").GetComponent<SpriteRenderer>();
        headGearSpriteRenderer = gameObject.transform.Find("Headgear").GetComponent<SpriteRenderer>();
        outfitSpriteRenderer = gameObject.transform.Find("Outfit").GetComponent<SpriteRenderer>();
        SetFrogSprites();
        initFlyDestination();
    }

    private void SetFrogSprites()
    {
        if (sadSprite != null)
        {
            bodySpriteRenderer.sprite = sadSprite;
        }

        if (headgearSprite != null)
        {
            headGearSpriteRenderer.sprite = headgearSprite;
        }

        if (outfitSprite != null)
        {
            outfitSpriteRenderer.sprite = outfitSprite;
        }
    }

    void Update()
    {
        if (!IsSad())
        {
            FlyAway();
        }
    }

    public void TakeFlowers(int flowers)
    {
        ReceiveHapiness(flowers);
    }

    private void ReceiveHapiness(int happiness)
    {
        Debug.Log("Frog received " + happiness + " happiness.");
        currentSadnessLevel -= happiness;
        if (!IsSad())
        {
            MakeHappy();
        }
    }

    public bool IsSad()
    {
        return currentSadnessLevel > 0;
    }

    private void MakeHappy()
    {
        if (happySprite != null)
        {
            bodySpriteRenderer.sprite = happySprite;
        }
        bodySpriteRenderer.sortingOrder += 5;
        outfitSpriteRenderer.sortingOrder += 5;
        headGearSpriteRenderer.sortingOrder += 5;
    }

    private void FlyAway()
    {
        gameObject.transform.localScale += new Vector3(-0.75f, -0.75f, 0);
        gameObject.transform.position = new Vector3(xSpeed, ySpeed, 0);
        xSpeed = xSpeed < 0 ? xSpeed - 5f : xSpeed + 5f;
        ySpeed = ySpeed < 0 ? ySpeed - 15f : ySpeed + 15f;
        if (xSpeed < 0)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 2.5f));
        }
        else
        {
            gameObject.transform.Rotate(new Vector3(0, 0, -2.5f));
        }
    }

    private void initFlyDestination()
    {
        var rnd = UnityEngine.Random.value;
        var rnd1 = UnityEngine.Random.value;

        xSpeed = rnd > 0.5f ? 5f : -5f;
        ySpeed = rnd1 > 0.5f ? 5f : -5f;
    }
}
