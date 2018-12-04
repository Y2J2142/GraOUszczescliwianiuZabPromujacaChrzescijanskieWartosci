using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData", menuName = "Item/NewItem", order = 2)]
public class ItemData : ScriptableObject
{
	public enum rarities {GOWNO, ZWYKLE, DOBRE, ZAJEBISTE}
	public int price = 100;
	public float modifier = 2;
	public string itemName;
	public Sprite sprite;
	public rarities rarity = rarities.GOWNO;
	public bool isBought = false;
}