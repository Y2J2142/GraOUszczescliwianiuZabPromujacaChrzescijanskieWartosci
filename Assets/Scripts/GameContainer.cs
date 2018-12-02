using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameContainer", menuName = "Game/NewGame", order = 1)]
public class GameContainer : ScriptableObject
{
    public int HappyFrogCounter { get; set; }
}
