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
    public int currentSadnessLevel = 10;
    private SpriteRenderer spriteRenderer;
    private RectTransform rectTransform;
    private float xSpeed;
    private float ySpeed;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sadSprite;
        rectTransform = gameObject.GetComponent<RectTransform>();
        initFlyDestination();
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
        spriteRenderer.sprite = happySprite;
    }

    private void FlyAway()
    {
        gameObject.transform.localScale += new Vector3(-0.75f, -0.75f, 0);
        gameObject.transform.position = new Vector3(xSpeed, ySpeed, 0);
        xSpeed = xSpeed < 0 ? xSpeed - 5f : xSpeed + 5f;
        ySpeed = ySpeed < 0 ? ySpeed - 15 : ySpeed + 15f;
        if(xSpeed < 0)
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
