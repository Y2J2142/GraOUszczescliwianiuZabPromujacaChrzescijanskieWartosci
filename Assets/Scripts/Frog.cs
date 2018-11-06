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
    private Image image;
    private float xSpeed;
    private float ySpeed;
    

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.sprite = sadSprite;
        initFlyDestination();
    }

    void Update()
    {
    }

    public void FlyAway()
    {
        Vector2 frogSize = gameObject.GetComponent<Image>().rectTransform.sizeDelta;
        image.rectTransform.sizeDelta = new Vector2(frogSize.x - 4, frogSize.y - 4);
        gameObject.transform.Translate(xSpeed, ySpeed, 0);
        xSpeed = xSpeed < 0 ? xSpeed - 0.1f : xSpeed + 0.1f;
        ySpeed = ySpeed < 0 ? ySpeed - 0.65f : ySpeed + 0.65f;
    }

    public void TakeFlowers(int flowers)
    {
        ReceiveHapiness(flowers);
        if (!IsSad())
        {
            MakeHappy();
        }
    }

    private void ReceiveHapiness(int happiness)
    {
        Debug.Log("Frog received " + happiness + " happiness.");
        currentSadnessLevel -= happiness;
    }

    public bool IsSad()
    {
        return currentSadnessLevel > 0;
    }

    private void MakeHappy()
    {
        image.sprite = happySprite;
    }

    private void initFlyDestination()
    {
        var rnd = UnityEngine.Random.value;
        var rnd1 = UnityEngine.Random.value;
     
        xSpeed = rnd > 0.5f ? 5f : -5f;
        ySpeed = rnd1 > 0.5f ? 5f : -5f;
    }
}
