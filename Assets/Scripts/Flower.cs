using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower
{
    public GameObject gameObject { get; set; }
    public float Happines { get; set; }

    private int rotateRadius;

    public Flower(GameObject flower)
    {
        this.gameObject = flower;
        this.rotateRadius = UnityEngine.Random.value > .5f ? 10 : -10;
    }

    public void FlyToFrog(GameObject frogObject)
    {
        gameObject.transform.Rotate(new Vector3(0, 0, rotateRadius));
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, frogObject.transform.position, 15);
    }

    public bool hitsFrog(GameObject frogObject)
    {
        return gameObject.transform.position == frogObject.transform.position;
    }
}
