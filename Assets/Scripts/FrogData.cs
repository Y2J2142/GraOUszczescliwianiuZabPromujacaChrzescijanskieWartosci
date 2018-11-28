using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrogData", menuName = "Frog/NewFrog", order = 1)]
public class FrogData : ScriptableObject
{
    public Sprite sadSprite;
    public Sprite happySprite;
    public Sprite headgearSprite;
    public Sprite outfitSprite;
    public int maximumSadnessLevel = 10;

    [Tooltip("im wieksze tym czesciej sie pojawia")]
    public int rarity = 100;
}
