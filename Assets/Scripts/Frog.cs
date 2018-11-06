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

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sadSprite;
        rectTransform = gameObject.GetComponent<RectTransform>();
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

    private bool IsSad()
    {
        return currentSadnessLevel > 0;
    }

    private void MakeHappy()
    {
        spriteRenderer.sprite = happySprite;
    }

    private void FlyAway()
    {
        Vector2 frogSize = rectTransform.sizeDelta;
        rectTransform.sizeDelta = new Vector2(frogSize.x - 4, frogSize.y - 4);
        gameObject.transform.Rotate(new Vector3(0, 0, 15));
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, behindUpperLeftCorner(), 15);
    }

    private Vector3 behindUpperLeftCorner()
    {
        Vector3[] worldCorners = new Vector3[4];
        GameObject.Find("Canvas").GetComponent<RectTransform>().GetWorldCorners(worldCorners);
        Vector3 leftTopCorner = worldCorners[1];
        return new Vector3(leftTopCorner.x - 100, leftTopCorner.y + 150, leftTopCorner.z);
    }
}
