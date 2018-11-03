using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Frog : MonoBehaviour
{

    [SerializeField]
    public Sprite sadSprite;

    [SerializeField]
    public Sprite happySprite;

    [SerializeField]
    public int currentSadnessLevel = 10;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sadSprite;
    }

    void Update()
    {
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

    private bool IsSad()
    {
        return currentSadnessLevel > 0;
    }

    private void MakeHappy()
    {
        spriteRenderer.sprite = happySprite;
    }
}
