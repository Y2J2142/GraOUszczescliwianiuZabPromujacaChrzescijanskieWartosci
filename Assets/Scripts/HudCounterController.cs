using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudCounterController : MonoBehaviour
{

    public Text HudCounter { get; set; }

    void Start()
    {
        this.HudCounter = gameObject.GetComponent<Text>();
        PlayerResourcesScript.OnCurrentCoinsNumberChange += OnCurrentCoinsNumberChange;
    }

    void Destroy()
    {
        PlayerResourcesScript.OnCurrentCoinsNumberChange -= OnCurrentCoinsNumberChange;
    }

    void Update()
    {

    }

    void OnCurrentCoinsNumberChange(int coinsNumber)
    {
        this.HudCounter.text = coinsNumber.ToString();
    }
}
