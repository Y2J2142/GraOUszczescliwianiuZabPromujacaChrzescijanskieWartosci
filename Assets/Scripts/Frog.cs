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
        image.sprite = happySprite;
    }
}
