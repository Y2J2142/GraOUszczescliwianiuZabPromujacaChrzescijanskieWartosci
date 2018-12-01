using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frog : MonoBehaviour
{
    [SerializeField]
    public FrogData frogData;
    private HudCounterController hudCounterController;
    private SpriteRenderer bodySpriteRenderer;
    private SpriteRenderer headGearSpriteRenderer;
    private SpriteRenderer outfitSpriteRenderer;
    private Animator animator;
    private float xSpeed;
    private float ySpeed;

    private int currentSadnessLevel;

    void Start()
    {
        bodySpriteRenderer = gameObject.transform.Find("Body").GetComponent<SpriteRenderer>();
        headGearSpriteRenderer = gameObject.transform.Find("Headgear").GetComponent<SpriteRenderer>();
        outfitSpriteRenderer = gameObject.transform.Find("Outfit").GetComponent<SpriteRenderer>();
        animator = gameObject.transform.Find("Body").GetComponent<Animator>();
        SetFrogSprites();
        initFlyDestination();
        currentSadnessLevel = frogData.maximumSadnessLevel;


    }

    private void SetFrogSprites()
    {
        if (frogData.sadSprite != null)
        {
            bodySpriteRenderer.sprite = frogData.sadSprite;
        }

        if (frogData.headgearSprite != null)
        {
            headGearSpriteRenderer.sprite = frogData.headgearSprite;
        }

        if (frogData.outfitSprite != null)
        {
            outfitSpriteRenderer.sprite = frogData.outfitSprite;
        }

        if (frogData.kamykAnimation != null)
        {
            animator.runtimeAnimatorController = frogData.kamykAnimation;
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
        animator.runtimeAnimatorController = null;
        if (frogData.happySprite != null)
        {
            bodySpriteRenderer.sprite = frogData.happySprite;
        }
        bodySpriteRenderer.sortingOrder += 5;
        outfitSpriteRenderer.sortingOrder += 5;
        headGearSpriteRenderer.sortingOrder += 5;
    }

    private void FlyAway()
    {
        gameObject.transform.localScale += new Vector3(-0.75f, -0.75f, 0);
        var localTemporaryVectorOfFrogRotation = gameObject.transform.localScale;
        localTemporaryVectorOfFrogRotation.x = localTemporaryVectorOfFrogRotation.x > 0 ? localTemporaryVectorOfFrogRotation.x : 0;
        localTemporaryVectorOfFrogRotation.y = localTemporaryVectorOfFrogRotation.y > 0 ? localTemporaryVectorOfFrogRotation.y : 0;
        gameObject.transform.localScale = localTemporaryVectorOfFrogRotation;

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
