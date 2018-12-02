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
    }

    void Update()
    {

    }

    internal void Increment()
    {
        this.HudCounter.text = (Convert.ToInt32(this.HudCounter.text) + 1).ToString();
    }
}
