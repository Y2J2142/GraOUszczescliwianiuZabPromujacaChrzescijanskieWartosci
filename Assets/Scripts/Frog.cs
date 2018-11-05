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

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.sprite = sadSprite;
    }

    void Update()
    {
    }

    public void FlyAway()
    {
        Vector2 frogSize = gameObject.GetComponent<Image>().rectTransform.sizeDelta;
        image.rectTransform.sizeDelta = new Vector2(frogSize.x - 4, frogSize.y - 4);
        gameObject.transform.Rotate(new Vector3(0, 0, 15));
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, behindUpperLeftCorner(), 15);
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

    private Vector3 behindUpperLeftCorner()
    {
        Vector3[] worldCorners = new Vector3[4];
        GameObject.Find("Canvas").GetComponent<RectTransform>().GetWorldCorners(worldCorners);
        Vector3 leftTopCorner = worldCorners[1];
        return new Vector3(leftTopCorner.x - 100, leftTopCorner.y + 150, leftTopCorner.z);
    }
}
