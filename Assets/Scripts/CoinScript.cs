using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private float xSpeed;
    private float ySpeed;
    private GameObject hudCounter;

    void Start()
    {
        hudCounter = GameObject.Find("HudCounter");
    }

    void Update()
    {
        FlyToCounter();
    }

    public void FlyToCounter()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, hudCounter.transform.position, 15);
    }

    public bool hitsCounter()
    {
        return gameObject.transform.position.x == hudCounter.transform.position.x;
    }
}
